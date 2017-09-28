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

    public class GlassTilesEffect
        : ShaderEffectBase
    {
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(GlassTilesEffect), 0);
        public static readonly DependencyProperty TilesProperty = DependencyProperty.Register(nameof(Tiles), typeof(double), typeof(GlassTilesEffect), new UIPropertyMetadata(5d, PixelShaderConstantCallback(0)));
        public static readonly DependencyProperty BevelWidthProperty = DependencyProperty.Register(nameof(BevelWidth), typeof(double), typeof(GlassTilesEffect), new UIPropertyMetadata(1d, PixelShaderConstantCallback(1)));
        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register(nameof(Offset), typeof(double), typeof(GlassTilesEffect), new UIPropertyMetadata(1d, PixelShaderConstantCallback(3)));
        public static readonly DependencyProperty GroutColorProperty = DependencyProperty.Register(nameof(GroutColor), typeof(Color), typeof(GlassTilesEffect), new UIPropertyMetadata(Color.FromArgb(255, 0, 0, 0), PixelShaderConstantCallback(2)));


        public double Tiles
        {
            get => (double)GetValue(TilesProperty);
            set => SetValue(TilesProperty, value);
        }

        public double BevelWidth
        {
            get => (double)GetValue(BevelWidthProperty);
            set => SetValue(BevelWidthProperty, value);
        }

        public double Offset
        {
            get => (double)GetValue(OffsetProperty);
            set => SetValue(OffsetProperty, value);
        }

        public Color GroutColor
        {
            get => (Color)GetValue(GroutColorProperty);
            set => SetValue(GroutColorProperty, value);
        }

        public Brush Input
        {
            get => GetValue(InputProperty) as Brush;
            set => SetValue(InputProperty, value);
        }


        public GlassTilesEffect()
            : base("GlassTiles")
        {
        }

        protected override void UpdateShader()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(TilesProperty);
            UpdateShaderValue(BevelWidthProperty);
            UpdateShaderValue(OffsetProperty);
            UpdateShaderValue(GroutColorProperty);
        }
    }

    public class CubicLensDistortionEffect
        : ShaderEffectBase
    {
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(CubicLensDistortionEffect), 0);
        public static readonly DependencyProperty DistortionCoefficientProperty = DependencyProperty.Register(nameof(DistortionCoefficient), typeof(double), typeof(CubicLensDistortionEffect), new UIPropertyMetadata(-.15, PixelShaderConstantCallback(0)));
        public static readonly DependencyProperty CubicDistortionProperty = DependencyProperty.Register(nameof(CubicDistortion), typeof(double), typeof(CubicLensDistortionEffect), new UIPropertyMetadata(.5, PixelShaderConstantCallback(1)));
        public static readonly DependencyProperty ZoomProperty = DependencyProperty.Register(nameof(Zoom), typeof(double), typeof(CubicLensDistortionEffect), new UIPropertyMetadata(1d, PixelShaderConstantCallback(2)));


        public Brush Input
        {
            get => GetValue(InputProperty) as Brush;
            set => SetValue(InputProperty, value);
        }

        public double DistortionCoefficient
        {
            get => (double)GetValue(DistortionCoefficientProperty);
            set => SetValue(DistortionCoefficientProperty, value);
        }

        public double CubicDistortion
        {
            get => (double)GetValue(CubicDistortionProperty);
            set => SetValue(CubicDistortionProperty, value);
        }

        public double Zoom
        {
            get => (double)GetValue(ZoomProperty);
            set => SetValue(ZoomProperty, value);
        }


        public CubicLensDistortionEffect()
            : base("CubicLensDistortion")
        {
        }

        protected override void UpdateShader()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(DistortionCoefficientProperty);
            UpdateShaderValue(CubicDistortionProperty);
        }
    }
}
