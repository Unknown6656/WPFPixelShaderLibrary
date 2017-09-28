using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Windows.Navigation;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows;
using System.Linq;
using System.Text;
using System;

namespace wpfpslib
{
    public sealed class NormalMapEffect
        : ShaderEffectBase
    {
        public static readonly DependencyProperty RangeProperty = DependencyProperty.Register(nameof(Range), typeof(double), typeof(NormalMapEffect), new UIPropertyMetadata(0d, PixelShaderConstantCallback(0)));
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(NormalMapEffect), 0);


        public double Range
        {
            get => ((double)(GetValue(RangeProperty)));
            set => SetValue(RangeProperty, value);
        }

        public Brush Input
        {
            get => ((Brush)(GetValue(InputProperty)));
            set => SetValue(InputProperty, value);
        }


        public NormalMapEffect()
            : base("NormalMap")
        {
        }

        protected override void UpdateShader()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(RangeProperty);
        }
    }
}
