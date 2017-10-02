using System.Windows.Media.Effects;
using System.Diagnostics;
using System.Linq;
using System;
using System.Resources;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;

namespace wpfpslib
{
    /// <summary>
    /// Represents the base class for all pixel shader effects. Cannot be inherited from external classes.
    /// </summary>
    public abstract class PixelShaderEffectBase
        : ShaderEffect
    {
        private static readonly string asmname = typeof(PixelShaderEffectBase).Assembly.ToString().Split(',')[0];


        /// <summary>
        /// A list of all available pixel shader effects' names
        /// </summary>
        public static string[] AvailableEffects { get; }


        /// <summary>
        /// Static constructor
        /// </summary>
        static PixelShaderEffectBase()
        {
            Assembly asm = typeof(PixelShaderEffectBase).Assembly;
            string resname = asm.GetManifestResourceNames().FirstOrDefault(s => s.Contains("g.resources")) ?? $"{asm.GetName().Name}.g.resources";
            Match m = null;

            using (Stream str = asm.GetManifestResourceStream(resname))
            using (ResourceReader rd = new ResourceReader(str))
                AvailableEffects = (from e in rd.Cast<DictionaryEntry>()
                                    let name = e.Key as string
                                    where name.match(@"(.*(\\|\/))?(?<name>.+)\.ps", out m)
                                    let res = m?.Groups["name"]?.ToString()
                                    where res != null
                                    select res).ToArray();
        }

        /// <summary>
        /// Creates a new pixel shader with the caller's name used as pixel shader input file
        /// </summary>
        internal PixelShaderEffectBase()
            : this(TrimEnd(new StackTrace().GetFrame(1).GetMethod().DeclaringType.Name, "Effect"))
        {
        }

        /// <summary>
        /// Creates a new pixel shader with the given name used as pixel shader input file (without file extension or directory)
        /// </summary>
        /// <param name="name">Compiled pixel shader file name</param>
        internal PixelShaderEffectBase(string name)
            : this(new PixelShader
            {
                UriSource = GetUri(name)
            })
        {
        }

        /// <summary>
        /// Creates a new pixel shader effect based on the given shader
        /// </summary>
        /// <param name="shader">Pixel shader</param>
        internal PixelShaderEffectBase(PixelShader shader)
        {
            PixelShader = shader;

            UpdateShader();
        }

        /// <summary>
        /// Updates all pixel shader properties
        /// </summary>
        internal protected abstract void UpdateShader();

        /// <summary>
        /// Tries to get the pixel shader associated with the given name and returns whether the fetch was successfull
        /// </summary>
        /// <param name="name">Pixel shader name</param>
        /// <param name="shader">The pixel shader (null if not found)</param>
        /// <returns>Result, whether the fetching operation was successfull</returns>
        public static bool TryGetShaderByName(string name, out PixelShaderEffectBase shader)
        {
            shader = null;
            name = name?.ToLower();

            if (AvailableEffects.Contains(name))
                shader = (from t in typeof(PixelShaderEffectBase).Assembly.GetExportedTypes()
                          where typeof(PixelShaderEffectBase).IsAssignableFrom(t)
                          where t.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)
                          select Activator.CreateInstance(t) as PixelShaderEffectBase).FirstOrDefault();

            return shader != null;
        }

        private static Uri GetUri(string name) => new Uri($"pack://application:,,,/{asmname};component/ps-compiled/{name}.ps");

        private static string TrimEnd(string input, string suffix, StringComparison cmp = StringComparison.InvariantCulture) =>
            (suffix != null) && (input?.EndsWith(suffix, cmp) ?? false) ? input.Substring(0, input.Length - suffix.Length) : input;

        /// <summary>
        /// Registers a new Dependency Property bound to a given .NET Property and a HLSL Pixel Shader register
        /// </summary>
        /// <param name="name">.NET Property name</param>
        /// <param name="type">.NET Property type</param>
        /// <param name="parent">.NET Parent type</param>
        /// <param name="default">Default value</param>
        /// <param name="slot">HLSL register</param>
        /// <returns>>Generated Depenendy Property</returns>
        protected static DependencyProperty Register(string name, Type type, Type parent, dynamic @default, int slot) =>
            DependencyProperty.Register(name, type, parent, new UIPropertyMetadata(@default, PixelShaderConstantCallback(slot)));
    }

    internal static class Extensions
    {
        internal static bool match(this string str, string pat, out Match m, RegexOptions opt = RegexOptions.Compiled | RegexOptions.IgnoreCase) =>
            (m = Regex.Match(str, pat, opt)).Success;
    }
}
