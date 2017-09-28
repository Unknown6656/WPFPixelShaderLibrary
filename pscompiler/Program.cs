using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System;

namespace pscompiler
{
    using spf = Environment.SpecialFolder;


    public static class Program
    {
        public static int Main(string[] argv)
        {
            if (!argv.Any())
            {
                Console.WriteLine(@"Usage:
    pscompiler <src-dir> <dst-dir>");

                return 0;
            }

            string archstr = IntPtr.Size == 8 ? "x64" : "x86";

            Match m = null;
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
                            let arch = file.Directory.Name
                            let verstr = file.Directory.FullName.match(@"([0-9]+\.[0-9]+[\.0-9]*)", out m) ? m.ToString() : "0.0.0.0"
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

                using (Process p = new Process())
                {
                    p.StartInfo = psi;
                    p.Start();

                    Console.Out.WriteLine(p.StandardOutput.ReadToEnd());
                    Console.Error.WriteLine(p.StandardError.ReadToEnd());

                    p.WaitForExit();

                    if ((ret == 0) && (p.ExitCode != 0))
                        ret = p.ExitCode;
                }

                Console.WriteLine($"{fx.FullName} --> {ps}");
            }
            
            return ret;
        }

        private static bool match(this string str, string pat, out Match m, RegexOptions opt = RegexOptions.Compiled | RegexOptions.IgnoreCase) =>
            (m = Regex.Match(str, pat, opt)).Success;
    }
}
