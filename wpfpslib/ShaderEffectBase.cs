using System.Windows.Media.Effects;
using System;

namespace wpfpslib
{
    public abstract class ShaderEffectBase
        : ShaderEffect
    {
        private static readonly string asmname = typeof(ShaderEffectBase).Assembly.ToString().Split(',')[0];

        internal protected static Uri GetUri(string name) => new Uri($"pack://application:,,,/{asmname};component/ps-compiled/{name}.ps");


        protected abstract void UpdateShader();

        public ShaderEffectBase(string name)
        {
            PixelShader = new PixelShader
            {
                UriSource = GetUri(name)
            };

            UpdateShader();
        }
    }
}
