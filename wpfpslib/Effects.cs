using System.Windows.Markup;
using System.Windows.Media;
using System.Windows;
using System.Reflection;
using System;

[assembly: XmlnsDefinition("https://unknown6656.com/wpfpslib", nameof(wpfpslib))]

namespace wpfpslib
{
    /// <summary>
    /// Represents an effect, which renderes any input into its corresponding normal map
    /// </summary>
    /// <inheritdoc/>
    public sealed class NormalMapEffect
        : PixelShaderEffectBase
    {
        /// <summary>
        /// Identifies the <see cref="Range"/> property
        /// </summary>
        public static readonly DependencyProperty RangeProperty = Register(nameof(Range), typeof(double), typeof(NormalMapEffect), 0d, 0);
        /// <summary>
        /// Identifies the <see cref="Input"/> property
        /// </summary>
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(NormalMapEffect), 0);


        /// <summary>
        /// The normal map edge detection range.
        /// <para/>
        /// Should be set to a value between [0..1]
        /// <para/>
        /// Should be set to `4 / Min(Width, Height)` for best results
        /// </summary>
        public double Range
        {
            get => (double)GetValue(RangeProperty);
            set => SetValue(RangeProperty, value);
        }

        /// <summary>
        /// The imagery input data
        /// </summary>
        public Brush Input
        {
            get => (Brush)GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }


        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <inheritdoc/>
        public NormalMapEffect()
            : base(typeof(NormalMapEffect))
        {
        }

        /// <summary>
        /// Updates all pixel shader properties
        /// </summary>
        /// <inheritdoc/>
        protected internal override void UpdateShader()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(RangeProperty);
        }
    }

    public sealed class GlassTilesEffect
        : PixelShaderEffectBase
    {
        /// <summary>
        /// Identifies the <see cref="Input"/> property
        /// </summary>
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(GlassTilesEffect), 0);
        /// <summary>
        /// Identifies the <see cref="Tiles"/> property
        /// </summary>
        public static readonly DependencyProperty TilesProperty = Register(nameof(Tiles), typeof(double), typeof(GlassTilesEffect), 5d, 0);
        /// <summary>
        /// Identifies the <see cref="BevelWidth"/> property
        /// </summary>
        public static readonly DependencyProperty BevelWidthProperty = Register(nameof(BevelWidth), typeof(double), typeof(GlassTilesEffect), 1d, 1);
        /// <summary>
        /// Identifies the <see cref="Offset"/> property
        /// </summary>
        public static readonly DependencyProperty OffsetProperty = Register(nameof(Offset), typeof(double), typeof(GlassTilesEffect), 1d, 3);
        /// <summary>
        /// Identifies the <see cref="BackgroundColor"/> property
        /// </summary>
        public static readonly DependencyProperty BackgroundColorProperty = Register(nameof(BackgroundColor), typeof(Color), typeof(GlassTilesEffect), Color.FromArgb(255, 0, 0, 0), 2);


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

        public Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        /// <summary>
        /// The imagery input data
        /// </summary>
        public Brush Input
        {
            get => GetValue(InputProperty) as Brush;
            set => SetValue(InputProperty, value);
        }


        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <inheritdoc/>
        public GlassTilesEffect()
            : base(typeof(NormalMapEffect))
        {
        }

        /// <summary>
        /// Updates all pixel shader properties
        /// </summary>
        /// <inheritdoc/>
        protected internal override void UpdateShader()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(TilesProperty);
            UpdateShaderValue(BevelWidthProperty);
            UpdateShaderValue(OffsetProperty);
            UpdateShaderValue(BackgroundColorProperty);
        }
    }

    public sealed class CubicLensDistortionEffect
        : PixelShaderEffectBase
    {
        /// <summary>
        /// Identifies the <see cref="Input"/> property
        /// </summary>
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(CubicLensDistortionEffect), 0);
        /// <summary>
        /// Identifies the <see cref="DistortionCoefficient"/> property
        /// </summary>
        public static readonly DependencyProperty DistortionCoefficientProperty = DependencyProperty.Register(nameof(DistortionCoefficient), typeof(double), typeof(CubicLensDistortionEffect), new UIPropertyMetadata(-.15, PixelShaderConstantCallback(0)));
        /// <summary>
        /// Identifies the <see cref="CubicDistortion"/> property
        /// </summary>
        public static readonly DependencyProperty CubicDistortionProperty = DependencyProperty.Register(nameof(CubicDistortion), typeof(double), typeof(CubicLensDistortionEffect), new UIPropertyMetadata(.5, PixelShaderConstantCallback(1)));
        /// <summary>
        /// Identifies the <see cref="Zoom"/> property
        /// </summary>
        public static readonly DependencyProperty ZoomProperty = DependencyProperty.Register(nameof(Zoom), typeof(double), typeof(CubicLensDistortionEffect), new UIPropertyMetadata(1d, PixelShaderConstantCallback(2)));


        /// <summary>
        /// The imagery input data
        /// </summary>
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


        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <inheritdoc/>
        public CubicLensDistortionEffect()
            : base(typeof(CubicLensDistortionEffect))
        {
        }

        /// <summary>
        /// Updates all pixel shader properties
        /// </summary>
        /// <inheritdoc/>
        protected internal override void UpdateShader()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(DistortionCoefficientProperty);
            UpdateShaderValue(CubicDistortionProperty);
        }
    }

    /// <summary>
    /// Represents a cubic chromatic abberation effect (also known as cubic RGB-split)
    /// </summary>
    /// <inheritdoc/>
    public sealed class CubicChromaticAbberationEffect
        : PixelShaderEffectBase
    {
        /// <summary>
        /// Identifies the <see cref="Input"/> property
        /// </summary>
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(CubicChromaticAbberationEffect), 0);
        /// <summary>
        /// Identifies the <see cref="Amount"/> property
        /// </summary>
        public static readonly DependencyProperty AmountProperty = DependencyProperty.Register(nameof(Amount), typeof(double), typeof(CubicChromaticAbberationEffect), new UIPropertyMetadata(0d, PixelShaderConstantCallback(0)));


        /// <summary>
        /// The imagery input data
        /// </summary>
        public Brush Input
        {
            get => GetValue(InputProperty) as Brush;
            set => SetValue(InputProperty, value);
        }

        /// <summary>
        /// Abberation amount.
        /// <para/>
        /// Should be set to a value between [0..∞)
        /// </summary>
        public double Amount
        {
            get => (double)GetValue(AmountProperty);
            set => SetValue(AmountProperty, value);
        }


        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <inheritdoc/>
        public CubicChromaticAbberationEffect()
            : base(typeof(CubicChromaticAbberationEffect))
        {
        }

        /// <summary>
        /// Updates all pixel shader properties
        /// </summary>
        /// <inheritdoc/>
        protected internal override void UpdateShader()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(AmountProperty);
        }
    }

    /// <summary>
    /// Represents a linear chromatic abberation effect (also known as linear RGB-split)
    /// </summary>
    /// <inheritdoc/>
    public sealed class LinearChromaticAbberationEffect
        : PixelShaderEffectBase
    {
        /// <summary>
        /// Identifies the <see cref="Input"/> property
        /// </summary>
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(LinearChromaticAbberationEffect), 0);
        /// <summary>
        /// Identifies the <see cref="Amount"/> property
        /// </summary>
        public static readonly DependencyProperty AmountProperty = DependencyProperty.Register(nameof(Amount), typeof(double), typeof(LinearChromaticAbberationEffect), new UIPropertyMetadata(0d, PixelShaderConstantCallback(0)));
        /// <summary>
        /// Identifies the <see cref="Angle"/> property
        /// </summary>
        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(nameof(Angle), typeof(double), typeof(LinearChromaticAbberationEffect), new UIPropertyMetadata(0d, PixelShaderConstantCallback(1)));


        /// <summary>
        /// The imagery input data
        /// </summary>
        public Brush Input
        {
            get => GetValue(InputProperty) as Brush;
            set => SetValue(InputProperty, value);
        }

        /// <summary>
        /// Abberation amount.
        /// <para/>
        /// Should be set to a value between [0..1]
        /// </summary>
        public double Amount
        {
            get => (double)GetValue(AmountProperty);
            set => SetValue(AmountProperty, value);
        }

        /// <summary>
        /// Abberation angle.
        /// <para/>
        /// Should be set to a value between [0..1] which will be translated to a mathematical angle between [0..2π]
        /// </summary>
        public double Angle
        {
            get => (double)GetValue(AngleProperty);
            set => SetValue(AngleProperty, value);
        }


        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <inheritdoc/>
        public LinearChromaticAbberationEffect()
            : base(typeof(LinearChromaticAbberationEffect))
        {
        }

        /// <summary>
        /// Updates all pixel shader properties
        /// </summary>
        protected internal override void UpdateShader()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(AmountProperty);
            UpdateShaderValue(AngleProperty);
        }
    }

    public sealed class RoundedGlassTilesEffect
        : PixelShaderEffectBase
    {
        /// <summary>
        /// Identifies the <see cref="Input"/> property
        /// </summary>
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(RoundedGlassTilesEffect), 0);
        /// <summary>
        /// Identifies the <see cref="Density"/> property
        /// </summary>
        public static readonly DependencyProperty DensityProperty = DependencyProperty.Register(nameof(Density), typeof(double), typeof(RoundedGlassTilesEffect), new UIPropertyMetadata(0.04d, PixelShaderConstantCallback(0)));


        /// <summary>
        /// The imagery input data
        /// </summary>
        public Brush Input
        {
            get => GetValue(InputProperty) as Brush;
            set => SetValue(InputProperty, value);
        }

        public double Density
        {
            get => (double)GetValue(DensityProperty);
            set => SetValue(DensityProperty, value);
        }


        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <inheritdoc/>
        public RoundedGlassTilesEffect()
            : base(typeof(RoundedGlassTilesEffect))
        {
        }

        /// <summary>
        /// Updates all pixel shader properties
        /// </summary>
        /// <inheritdoc/>
        protected internal override void UpdateShader()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(DensityProperty);
        }
    }

    /// <summary>
    /// Represents an hexagonal pixelation effect
    /// </summary>
    /// <inheritdoc/>
    public sealed class HexagonalPixelationEffect
        : PixelShaderEffectBase
    {
        /// <summary>
        /// Identifies the <see cref="Input"/> property
        /// </summary>
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(HexagonalPixelationEffect), 0);
        /// <summary>
        /// Identifies the <see cref="Amount"/> property
        /// </summary>
        public static readonly DependencyProperty AmountProperty = DependencyProperty.Register(nameof(Amount), typeof(double), typeof(HexagonalPixelationEffect), new UIPropertyMetadata(0d, PixelShaderConstantCallback(0)));


        /// <summary>
        /// The imagery input data
        /// </summary>
        public Brush Input
        {
            get => GetValue(InputProperty) as Brush;
            set => SetValue(InputProperty, value);
        }

        /// <summary>
        /// The pixelation amount
        /// <para/>
        /// The value should be between (0..1]
        /// </summary>
        public double Amount
        {
            get => (double)GetValue(AmountProperty);
            set => SetValue(AmountProperty, value);
        }


        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <inheritdoc/>
        public HexagonalPixelationEffect()
            : base(typeof(HexagonalPixelationEffect))
        {
        }

        /// <summary>
        /// Updates all pixel shader properties
        /// </summary>
        /// <inheritdoc/>
        protected internal override void UpdateShader()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(AmountProperty);
        }
    }

    /// <summary>
    /// Represents a rippling effect
    /// </summary>
    /// <inheritdoc/>
    public class RippleEffect
        : PixelShaderEffectBase
    {
        /// <summary>
        /// Identifies the <see cref="Input"/> property
        /// </summary>
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(RippleEffect), 0);
        /// <summary>
        /// Identifies the <see cref="Center"/> property
        /// </summary>
        public static readonly DependencyProperty CenterProperty = DependencyProperty.Register(nameof(Center), typeof(Point), typeof(RippleEffect), new UIPropertyMetadata(new Point(.5, .5), PixelShaderConstantCallback(0)));
        /// <summary>
        /// Identifies the <see cref="Amplitude"/> property
        /// </summary>
        public static readonly DependencyProperty AmplitudeProperty = DependencyProperty.Register(nameof(Amplitude), typeof(double), typeof(RippleEffect), new UIPropertyMetadata(.1, PixelShaderConstantCallback(1)));
        /// <summary>
        /// Identifies the <see cref="Frequency"/> property
        /// </summary>
        public static readonly DependencyProperty FrequencyProperty = DependencyProperty.Register(nameof(Frequency), typeof(double), typeof(RippleEffect), new UIPropertyMetadata(70d, PixelShaderConstantCallback(2)));
        /// <summary>
        /// Identifies the <see cref="Phase"/> property
        /// </summary>
        public static readonly DependencyProperty PhaseProperty = DependencyProperty.Register(nameof(Phase), typeof(double), typeof(RippleEffect), new UIPropertyMetadata(.0, PixelShaderConstantCallback(3)));
        /// <summary>
        /// Identifies the <see cref="AspectRatio"/> property
        /// </summary>
        public static readonly DependencyProperty AspectRatioProperty = DependencyProperty.Register(nameof(AspectRatio), typeof(double), typeof(RippleEffect), new UIPropertyMetadata(1.5, PixelShaderConstantCallback(4)));


        /// <summary>
        /// The imagery input data
        /// </summary>
        public Brush Input
        {
            get => GetValue(InputProperty) as Brush;
            set => SetValue(InputProperty, value);
        }

        /// <summary>
        /// The ripple effect's center point.
        /// <para/>
        /// Should be set to a value between [(0,0)..(1,1)]
        /// </summary>
        public Point Center
        {
            get => (Point)GetValue(CenterProperty);
            set => SetValue(CenterProperty, value);
        }

        /// <summary>
        /// The rippling effect's wave amplitude.
        /// <para/>
        /// Should be set to a value between [0..1]
        /// </summary>
        public double Amplitude
        {
            get => (double)GetValue(AmplitudeProperty);
            set => SetValue(AmplitudeProperty, value);
        }

        /// <summary>
        /// The rippling effect's wave frequency.
        /// <para/>
        /// Should be set to a value between [0..300]
        /// </summary>
        public double Frequency
        {
            get => (double)GetValue(FrequencyProperty);
            set => SetValue(FrequencyProperty, value);
        }

        /// <summary>
        /// The rippling effect's phase.
        /// <para/>
        /// Should be set to a value between [-π..+π]
        /// </summary>
        public double Phase
        {
            get => (double)GetValue(PhaseProperty);
            set => SetValue(PhaseProperty, value);
        }

        /// <summary>
        /// The rippling effect's aspect ratio.
        /// <para/>
        /// Should be set to `Width / Height` 
        /// </summary>
        public double AspectRatio
        {
            get => (double)GetValue(AspectRatioProperty);
            set => SetValue(AspectRatioProperty, value);
        }


        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <inheritdoc/>
        public RippleEffect()
            : base(typeof(RippleEffect))
        {
        }

        /// <summary>
        /// Updates all pixel shader properties
        /// </summary>
        /// <inheritdoc/>
        protected internal override void UpdateShader()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(CenterProperty);
            UpdateShaderValue(AmplitudeProperty);
            UpdateShaderValue(FrequencyProperty);
            UpdateShaderValue(PhaseProperty);
            UpdateShaderValue(AspectRatioProperty);
        }
    }

    /// <summary>
    /// Represents a pixelation effect
    /// </summary>
    /// <inheritdoc/>
    public class PixelationEffect
        : PixelShaderEffectBase
    {
        /// <summary>
        /// Identifies the <see cref="Input"/> property
        /// </summary>
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(PixelationEffect), 0);
        /// <summary>
        /// Identifies the <see cref="Count"/> property
        /// </summary>
        public static readonly DependencyProperty CountProperty = DependencyProperty.Register(nameof(Count), typeof(Point), typeof(PixelationEffect), new UIPropertyMetadata(new Point(0, 0), PixelShaderConstantCallback(0)));


        /// <summary>
        /// The imagery input data
        /// </summary>
        public Brush Input
        {
            get => GetValue(InputProperty) as Brush;
            set => SetValue(InputProperty, value);
        }

        /// <summary>
        /// The horizontal and vertical pixel count
        /// </summary>
        public Point Count
        {
            get => (Point)GetValue(CountProperty);
            set => SetValue(CountProperty, value);
        }


        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <inheritdoc/>
        public PixelationEffect()
            : base(typeof(PixelationEffect))
        {
        }

        /// <summary>
        /// Updates all pixel shader properties
        /// </summary>
        /// <inheritdoc/>
        protected internal override void UpdateShader()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(CountProperty);
        }
    }

    /// <summary>
    /// Represents an offset/diagonalized pixelation effect
    /// </summary>
    /// <inheritdoc/>
    public class DiagonalPixelationEffect
        : PixelShaderEffectBase
    {
        /// <summary>
        /// Identifies the <see cref="Input"/> property
        /// </summary>
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(DiagonalPixelationEffect), 0);
        /// <summary>
        /// Identifies the <see cref="Count"/> property
        /// </summary>
        public static readonly DependencyProperty CountProperty = DependencyProperty.Register(nameof(Count), typeof(Point), typeof(DiagonalPixelationEffect), new UIPropertyMetadata(new Point(0, 0), PixelShaderConstantCallback(0)));


        /// <summary>
        /// The imagery input data
        /// </summary>
        public Brush Input
        {
            get => GetValue(InputProperty) as Brush;
            set => SetValue(InputProperty, value);
        }

        /// <summary>
        /// The horizontal and vertical pixel count
        /// </summary>
        public Point Count
        {
            get => (Point)GetValue(CountProperty);
            set => SetValue(CountProperty, value);
        }


        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <inheritdoc/>
        public DiagonalPixelationEffect()
            : base(typeof(DiagonalPixelationEffect))
        {
        }

        /// <summary>
        /// Updates all pixel shader properties
        /// </summary>
        /// <inheritdoc/>
        protected internal override void UpdateShader()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(CountProperty);
        }
    }

    /// <summary>
    /// Represents an effect creating a gravitational lensing around a black hole
    /// </summary>
    /// <inheritdoc/>
    public sealed class BlackHoleEffect
            : PixelShaderEffectBase
    {
        /// <summary>
        /// Identifies the <see cref="Position"/> property
        /// </summary>
        public static readonly DependencyProperty PositionProperty = Register(nameof(Position), typeof(Point), typeof(BlackHoleEffect), new Point(.5, .5), 0);
        /// <summary>
        /// Identifies the <see cref="Aspectratio"/> property
        /// </summary>
        public static readonly DependencyProperty AspectratioProperty = Register(nameof(Aspectratio), typeof(double), typeof(BlackHoleEffect), default(double), 1);
        /// <summary>
        /// Identifies the <see cref="Radius"/> property
        /// </summary>
        public static readonly DependencyProperty RadiusProperty = Register(nameof(Radius), typeof(double), typeof(BlackHoleEffect), default(double), 2);
        /// <summary>
        /// Identifies the <see cref="Dist"/> property
        /// </summary>
        public static readonly DependencyProperty DistProperty = Register(nameof(Dist), typeof(double), typeof(BlackHoleEffect), default(double), 3);
        /// <summary>
        /// Identifies the <see cref="Size"/> property
        /// </summary>
        public static readonly DependencyProperty SizeProperty = Register(nameof(Size), typeof(double), typeof(BlackHoleEffect), default(double), 4);
        /// <summary>
        /// Identifies the <see cref="Input"/> property
        /// </summary>
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(BlackHoleEffect), 0);


        /// <summary>
        /// The black hole's center position
        /// <para/>
        /// Should be set to a value between [(0,0)..(1,1)]
        /// </summary>
        public Point Position
        {
            get => (Point)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        /// <summary>
        /// The aspect ratio.
        /// <para/>
        /// Should be set to the value 'Width / Height'
        /// </summary>
        public double Aspectratio
        {
            get => (double)GetValue(AspectratioProperty);
            set => SetValue(AspectratioProperty, value);
        }

        /// <summary>
        /// The graviational lensing effect radius.
        /// <para/>
        /// Should be set to a value between [0..1]
        /// </summary>
        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        /// <summary>
        /// The black hole's distance to the 'camera'.
        /// <para/>
        /// Should be set to a value of (0..1]
        /// </summary>
        public double Dist
        {
            get => (double)GetValue(DistProperty);
            set => SetValue(DistProperty, value);
        }

        /// <summary>
        /// The black hole's size compared to its graviational lensing radius.
        /// <para/>
        /// Should be set to a value between [(0,0)...(1,1)]
        /// </summary>
        public double Size
        {
            get => (double)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        /// <summary>
        /// The imagery input data
        /// </summary>
        public Brush Input
        {
            get => (Brush)GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }


        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <inheritdoc/>
        public BlackHoleEffect()
            : base(typeof(BlackHoleEffect))
        {
        }

        /// <summary>
        /// Updates all pixel shader properties
        /// </summary>
        /// <inheritdoc/>
        protected internal override void UpdateShader()
        {
            UpdateShaderValue(PositionProperty);
            UpdateShaderValue(AspectratioProperty);
            UpdateShaderValue(RadiusProperty);
            UpdateShaderValue(DistProperty);
            UpdateShaderValue(SizeProperty);
            UpdateShaderValue(InputProperty);
        }
    }

    /// <summary>
    /// Represents a color inversion effect
    /// </summary>
    /// <inheritdoc/>
    public sealed class InvertEffect
        : PixelShaderEffectBase
    {
        /// <summary>
        /// Identifies the <see cref="Amount"/> property
        /// </summary>
        public static readonly DependencyProperty AmountProperty = Register(nameof(Amount), typeof(double), typeof(InvertEffect), 1d, 0);
        /// <summary>
        /// Identifies the <see cref="Input"/> property
        /// </summary>
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(InvertEffect), 0);


        /// <summary>
        /// The inversion amount.
        /// <para/>
        /// Should be set to a value between [0..1]
        /// </summary>
        public double Amount
        {
            get => (double)GetValue(AmountProperty);
            set => SetValue(AmountProperty, value);
        }

        /// <summary>
        /// The imagery input data
        /// </summary>
        public Brush Input
        {
            get => (Brush)GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }


        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <inheritdoc/>
        public InvertEffect()
            : base(typeof(InvertEffect))
        {
        }

        /// <summary>
        /// Updates all pixel shader properties
        /// </summary>
        /// <inheritdoc/>
        protected internal override void UpdateShader()
        {
            UpdateShaderValue(AmountProperty);
            UpdateShaderValue(InputProperty);
        }
    }

    /// <summary>
    /// Represents a telescopic blurring effect
    /// </summary>
    /// <inheritdoc/>
    public sealed class TelescopicBlurEffect
        : PixelShaderEffectBase
    {
        /// <summary>
        /// Identifies the <see cref="Center"/> property
        /// </summary>
        public static readonly DependencyProperty CenterProperty = Register(nameof(Center), typeof(Point), typeof(TelescopicBlurEffect), new Point(.5, .5), 0);
        /// <summary>
        /// Identifies the <see cref="Amount"/> property
        /// </summary>
        public static readonly DependencyProperty AmountProperty = Register(nameof(Amount), typeof(double), typeof(TelescopicBlurEffect), default(double), 1);
        /// <summary>
        /// Identifies the <see cref="Radius"/> property
        /// </summary>
        public static readonly DependencyProperty RadiusProperty = Register(nameof(Radius), typeof(double), typeof(TelescopicBlurEffect), default(double), 2);
        /// <summary>
        /// Identifies the <see cref="Input"/> property
        /// </summary>
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(TelescopicBlurEffect), 0);


        /// <summary>
        /// The blur's center point
        /// <para/>
        /// Should be set to a value between [(0,0)..(1,1)]
        /// </summary>
        public Point Center
        {
            get => (Point)GetValue(CenterProperty);
            set => SetValue(CenterProperty, value);
        }

        /// <summary>
        /// The telescopic blur amount.
        /// <para/>
        /// Should be set to a value between [0..1]
        /// </summary>
        public double Amount
        {
            get => (double)GetValue(AmountProperty);
            set => SetValue(AmountProperty, value);
        }

        /// <summary>
        /// The blur's inner falloff radius.
        /// <para/>
        /// Should be set to a value between [0..1]
        /// </summary>
        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        /// <summary>
        /// The imagery input data
        /// </summary>
        public Brush Input
        {
            get => (Brush)GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }


        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <inheritdoc/>
        public TelescopicBlurEffect()
            : base(typeof(TelescopicBlurEffect))
        {
        }

        /// <summary>
        /// Updates all pixel shader properties
        /// </summary>
        /// <inheritdoc/>
        protected internal override void UpdateShader()
        {
            UpdateShaderValue(CenterProperty);
            UpdateShaderValue(AmountProperty);
            UpdateShaderValue(RadiusProperty);
            UpdateShaderValue(InputProperty);
        }
    }

    /// <summary>
    /// Represents a spastic horizontal twitching effect
    /// </summary>
    /// <inheritdoc/>
    public sealed class SpasticChromaticAbberationEffect
        : PixelShaderEffectBase
    {
        /// <summary>
        /// Identifies the <see cref="Phase"/> property
        /// </summary>
        public static readonly DependencyProperty PhaseProperty = Register(nameof(Phase), typeof(double), typeof(SpasticChromaticAbberationEffect), default(double), 0);
        /// <summary>
        /// Identifies the <see cref="Amount"/> property
        /// </summary>
        public static readonly DependencyProperty AmountProperty = Register(nameof(Amount), typeof(double), typeof(SpasticChromaticAbberationEffect), default(double), 1);
        /// <summary>
        /// Identifies the <see cref="Input"/> property
        /// </summary>
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(SpasticChromaticAbberationEffect), 0);


        /// <summary>
        /// The twitching phase shift.
        /// <para/>
        /// Should be set to a value between [0..1] which will be mapped to a value between [0..2π]
        /// </summary>
        public double Phase
        {
            get => (double)GetValue(PhaseProperty);
            set => SetValue(PhaseProperty, value);
        }

        /// <summary>
        /// The twitching amount.
        /// <para/>
        /// Should be set to a value between [0..1]
        /// </summary>
        public double Amount
        {
            get => (double)GetValue(AmountProperty);
            set => SetValue(AmountProperty, value);
        }

        /// <summary>
        /// The imagery input data
        /// </summary>
        public Brush Input
        {
            get => (Brush)GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }


        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <inheritdoc/>
        public SpasticChromaticAbberationEffect()
            : base(typeof(SpasticChromaticAbberationEffect))
        {
        }

        /// <summary>
        /// Updates all pixel shader properties
        /// </summary>
        /// <inheritdoc/>
        protected internal override void UpdateShader()
        {
            UpdateShaderValue(PhaseProperty);
            UpdateShaderValue(AmountProperty);
            UpdateShaderValue(InputProperty);
        }
    }

    public sealed class BlurBehindEffect
        : PixelShaderEffectBase
    {
        /// <summary>
        /// Identifies the <see cref="Input"/> property
        /// </summary>
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(BlurBehindEffect), 0);

        public static readonly DependencyProperty UpLeftCornerProperty = Register(nameof(UpLeftCorner), typeof(Point), typeof(BlurBehindEffect), default(Point), 0);

        public static readonly DependencyProperty LowRightCornerProperty = Register(nameof(LowRightCorner), typeof(Point), typeof(BlurBehindEffect), default(Point), 1);

        public static readonly DependencyProperty SpacingProperty = Register(nameof(Spacing), typeof(Size), typeof(BlurBehindEffect), default(Size), 2);

        /// <summary>
        /// Identifies the <see cref="Radius"/> property
        /// </summary>
        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(nameof(Radius), typeof(double), typeof(BlurBehindEffect), new PropertyMetadata(0d));

        public static readonly DependencyProperty FrameworkElementProperty = DependencyProperty.Register(nameof(FrameworkElement), typeof(FrameworkElement), typeof(BlurBehindEffect), new PropertyMetadata(null, OnFrameworkElementPropertyChanged));

        private static readonly PropertyInfo? _inheritance_ctx = typeof(BlurBehindEffect).GetProperty("InheritanceContext", BindingFlags.Instance | BindingFlags.NonPublic);


        /// <summary>
        /// The imagery input data
        /// </summary>
        public Brush Input
        {
            get => (Brush)GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }

        /// <summary>
        /// The blur radius.
        /// </summary>
        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set
            {
                SetValue(RadiusProperty, value);
                FrameworkElement = FrameworkElement; // trigger update
            }
        }

        public Size Spacing
        {
            get => (Size)GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }

        public Point UpLeftCorner
        {
            get => (Point)GetValue(UpLeftCornerProperty);
            set => SetValue(UpLeftCornerProperty, value);
        }

        public Point LowRightCorner
        {
            get => (Point)GetValue(LowRightCornerProperty);
            set => SetValue(LowRightCornerProperty, value);
        }

        public FrameworkElement? FrameworkElement
        {
            get => GetValue(FrameworkElementProperty) as FrameworkElement;
            set => SetValue(FrameworkElementProperty, value);
        }

        public FrameworkElement? GetInheritanceContext => _inheritance_ctx?.GetValue(this, null) as FrameworkElement;


        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <inheritdoc/>
        public BlurBehindEffect()
            : base(typeof(BlurBehindEffect))
        {
        }

        /// <summary>
        /// Updates all pixel shader properties
        /// </summary>
        /// <inheritdoc/>
        protected internal override void UpdateShader()
        {
            UpdateShaderValue(RadiusProperty);
            UpdateShaderValue(UpLeftCornerProperty);
            UpdateShaderValue(LowRightCornerProperty);
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(SpacingProperty);
        }

        private void UpdateEffect(object? sender, EventArgs args)
        {
            if (GetInheritanceContext is FrameworkElement under && FrameworkElement is FrameworkElement over)
            {
                Point u_origin = under.PointToScreen(new Point(0, 0));
                Point o_origin = over.PointToScreen(new Point(0, 0));
                Rect u_rect = new Rect(u_origin.X, u_origin.Y, under.ActualWidth, under.ActualHeight);
                Rect o_rect = new Rect(o_origin.X, o_origin.Y, over.ActualWidth, over.ActualHeight);
                Rect intersect = Rect.Intersect(o_rect, u_rect);

                if (intersect.IsEmpty)
                {
                    UpLeftCorner = new Point(0, 0);
                    LowRightCorner = new Point(0, 0);
                }
                else
                {
                    Point i_origin = under.PointFromScreen(new Point(intersect.X, intersect.Y));

                    UpLeftCorner = new Point(i_origin.X / under.ActualWidth, i_origin.Y / under.ActualHeight);
                    LowRightCorner = new Point(UpLeftCorner.X + (intersect.Width / under.ActualWidth), UpLeftCorner.Y + (intersect.Height / under.ActualHeight));
                }

                UpdateShader();
            }
        }

        private void SizeChanged(object sender, SizeChangedEventArgs e) => Spacing = new Size(Radius / e.NewSize.Width, Radius / e.NewSize.Height);

        private static void OnFrameworkElementPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            BlurBehindEffect fx = (BlurBehindEffect)d;

            if (args.OldValue is FrameworkElement old)
            {
                old.LayoutUpdated -= fx.UpdateEffect;
                old.SizeChanged -= fx.SizeChanged;
            }

            if (args.NewValue is FrameworkElement @new)
            {
                @new.LayoutUpdated += fx.UpdateEffect;
                @new.SizeChanged += fx.SizeChanged;
            }
        }
    }
}
