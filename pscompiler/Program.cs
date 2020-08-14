using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System;

namespace pscompiler
{
    using spf = Environment.SpecialFolder;


    public static class Program
    {
        public static int Main(string[] argv)
        {
            Console.WriteLine("Copyright (c) Unknown6656, 2017. All rights reserved.");
            
            if (!argv.Any())
            {
                Console.WriteLine(@"Usage:
    pscompiler <src-dir> <dst-dir>

Compiles any *.fx file in `src-dir` and writes the generated *.ps file into the directory `dst-dir`.");

                return 0;
            }

            string archstr = IntPtr.Size == 8 ? "x64" : "x86";

            FileInfo fxc = (from f in new[]
                            {
                                spf.Programs,
                                spf.ProgramFiles,
                                spf.ProgramFilesX86,
                                spf.CommonPrograms,
                                spf.CommonProgramFiles,
                                spf.CommonProgramFilesX86,
                            }
                            let path = Environment.GetFolderPath(f)
                            let dir = new DirectoryInfo($@"{path}\Windows Kits")
                            where dir.Exists
                            from file in dir.GetFiles("fxc.exe", SearchOption.AllDirectories)
                            where file?.Directory?.Name != null
                            let arch = file.Directory.Name
                            let verstr = file.Directory.FullName.match(@"([0-9]+\.[0-9]+[\.0-9]*)", out Match m) ? m.ToString() : "0.0.0.0"
                            orderby new Version(verstr) descending
                            orderby arch.ToLower().Contains(archstr) ? 1 : -1 descending
                            select file).FirstOrDefault();

            foreach (var check in new(Func<bool> f, string msg)[]
            {
                (() => fxc is null, "The fxc compiler could not be found on this machine. Please install the latest Windows SDK version."),
                (() => argv.Length < 1, "A source directory must be given."),
                (() => argv.Length < 2, "A target directory must be given."),
                (() => !Directory.Exists(argv[0]), "The source directory must exist."),
                (() => !Directory.Exists(argv[0]), "The target directory must exist."),
            })
                if (check.f())
                {
                    Console.Error.WriteLine(check.msg);

                    return -1;
                }

            DirectoryInfo dst = new DirectoryInfo(argv[1]);
            int ret = 0;

            foreach (FileInfo fx in new DirectoryInfo(argv[0]).GetFiles("*.fx"))
            {
                string ps = $@"{dst.FullName}\{fx.Name.Replace(fx.Extension, "")}.ps";
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = fxc.FullName,
                    Arguments = $"/T ps_3_0 /E main /Fo \"{ps}\" \"{fx.FullName}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                };
                bool failed;

                using (Process p = new Process())
                {
                    p.StartInfo = psi;
                    p.Start();

                    string cout = p.StandardOutput.ReadToEnd();
                    string cerr = p.StandardError.ReadToEnd();

                    p.WaitForExit();
                    failed = p.ExitCode != 0;

                    if (failed)
                    {
                        Console.Out.WriteLine(cout);
                        Console.Error.WriteLine(cerr);
                        
                        if (ret == 0)
                            ret = p.ExitCode;
                    }
                }

                Console.WriteLine($"[{(failed ? "ERR." : " OK ")}] {fx.FullName} --> {ps}");
            }
            
            return ret;
        }

        private static bool match(this string str, string pat, out Match m, RegexOptions opt = RegexOptions.Compiled | RegexOptions.IgnoreCase) => (m = Regex.Match(str, pat, opt)).Success;
    }
}
