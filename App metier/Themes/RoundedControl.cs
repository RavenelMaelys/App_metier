using AyoControlLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFUI.Themes
{
    /// <summary>
    /// Interaction logic for RoundedControl.xaml
    /// </summary>
    public partial class RoundedControl : UserControl, IClickable, IActivable
    {
        private const double ALPHA_OVER = 0.35d;

        private static readonly Color DEFAULT_BACK = AyoToolsUtility.AyoDarkGray;
        private static readonly Color DEFAULT_BORDER = AyoToolsUtility.AyoLightGray;


        public static readonly DependencyProperty TextDepency = DependencyProperty.RegisterAttached(nameof(Text), typeof(string), typeof(RoundedControl),new PropertyMetadata("Ayo Text",new PropertyChangedCallback(UpdateControl)));
        public static readonly DependencyProperty CommandDepency = DependencyProperty.RegisterAttached(nameof(Command), typeof(ICommand), typeof(RoundedControl));
        public static readonly DependencyProperty CommandParameterDepency = DependencyProperty.RegisterAttached(nameof(CommandParamater), typeof(object), typeof(RoundedControl));
        public static readonly DependencyProperty IsActivDepency = DependencyProperty.RegisterAttached(nameof(IsActiv), typeof(bool), typeof(RoundedControl), new PropertyMetadata(false, new PropertyChangedCallback(UpdateControl)));
        public static readonly DependencyProperty ProgressPourcentDepency = DependencyProperty.RegisterAttached(nameof(ProgressPourcent), typeof(double), typeof(RoundedControl));
        public static readonly DependencyProperty ImageDepency = DependencyProperty.RegisterAttached(nameof(Image), typeof(ImageSource), typeof(RoundedControl));



        //public static readonly DependencyProperty OverCommand = DependencyProperty.RegisterAttached(nameof(Text), typeof(string), typeof(RoundedControl),new PropertyMetadata("Ayo Text"));
        //public static readonly DependencyProperty DownCommand = DependencyProperty.RegisterAttached(nameof(Text), typeof(string), typeof(RoundedControl),new PropertyMetadata("Ayo Text"));
        //public static readonly DependencyProperty UpComma = DependencyProperty.RegisterAttached(nameof(Text), typeof(string), typeof(RoundedControl),new PropertyMetadata("Ayo Text"));

        // event
        public event EventHandler OnClick;
        public event EventHandler OnActivStateChanged;
        public event MouseEventHandler OnOver;
        public event MouseEventHandler OnLeave;
        public event MouseEventHandler OnDown;
        public event MouseEventHandler OnMove;
        public event MouseEventHandler OnUp;


        // private
        //protected bool _isActivate = false;
        protected bool _hasProgessBar = false;
        protected bool _isClickable = false;
        protected bool _isMouseIn = false;
        protected bool _isMouseDown = false;
        protected bool _isAutoCheck = false;
        protected bool _isAllowOver = true;
        protected bool _isOverOnPicture = true;

        protected int _radius = 10;
        protected int _borderSize = 10;

        protected float _zoomImage = 1;
        protected double _sizeText = 9d;
        protected double _activAccentuation = 1;

        protected ERoundedType _roundedType = ERoundedType.All;
        protected ERoundedFlag _roundedFlags = ERoundedFlag.None;
        protected EImageLayout _imageLayout = EImageLayout.Stretch;

        protected Color? _clrBackEnable = AyoToolsUtility.AyoDarkGray;
        protected Color? _clrBackDisable = AyoToolsUtility.AyoMiddleGray;
        protected Color? _clrBackOver = AyoToolsUtility.AyoGray;
        protected Color? _clrBackDown = AyoToolsUtility.AyoDarkGray;
        protected Color? _clrBackActiv = AyoToolsUtility.AyoDarkGray;
        protected Color? _clrBorderEnable = AyoToolsUtility.AyoLightGray;
        protected Color? _clrBorderDisable = AyoToolsUtility.AyoLightGray;
        protected Color? _clrBorderOver = null;
        protected Color? _clrBorderDown = AyoToolsUtility.AyoYellow;
        protected Color? _clrBorderActiv = AyoToolsUtility.AyoYellow;
        protected Color _clrProgress = AyoToolsUtility.AyoLimeGreen;

        protected Typeface _fontFamilly = AyoToolsUtility.AyoFontFamily;


        // accessor
        //public bool IsActiv { get => _isActivate; set { bool trigger = _isActivate != value; _isActivate = value; OnActivStateChanged?.Invoke(this, EventArgs.Empty); InvalidateVisual(); } }
        public bool IsActiv { get => (bool)GetValue(IsActivDepency); set { bool trigger = IsActiv != value; SetValue(IsActivDepency,value); OnActivStateChanged?.Invoke(this, EventArgs.Empty); InvalidateVisual(); } }
        public bool IsClickalble { get => _isClickable; set { _isClickable = value; InvalidateVisual(); } }
        public bool IsAutoCheck { get => _isAutoCheck; set { _isAutoCheck = value; InvalidateVisual(); } }
        public bool IsAllowOver { get => _isAllowOver; set { _isAllowOver = value; InvalidateArrange(); } }
        public bool IsOverOnPicture { get => _isOverOnPicture; set { _isOverOnPicture = value; InvalidateVisual(); } }
        public bool HasProgressBar { get => _hasProgessBar; set { _hasProgessBar = value; InvalidateVisual(); } }

        public float ZoomImage { get => _zoomImage; set { _zoomImage = value; InvalidateVisual(); } }

        public double ProgressPourcent { get => (double)GetValue(ProgressPourcentDepency); set { SetValue(ProgressPourcentDepency, value.Clamp(0d,1d)); InvalidateVisual(); } }
        public double SizeText { get => _sizeText; set { _sizeText = value; InvalidateVisual(); } }
        public double ActivAccentuation { get => _activAccentuation; set { _activAccentuation = value; InvalidateVisual(); } }

        public int CornerRadius { get => _radius; set { _radius = value; InvalidateVisual(); } }
        public int BorderSize { get => _borderSize; set { _borderSize = value; InvalidateVisual(); } }

        public string Text { get => (string)GetValue(TextDepency); set { SetValue(TextDepency, value); InvalidateVisual(); } }

        public object CommandParamater { get => GetValue(CommandParameterDepency); set => SetValue(CommandParameterDepency,value);  }

        public ERoundedType RoundedType { get => _roundedType; set { _roundedType = value; InvalidateVisual(); } }
        public ERoundedFlag RoundedFlag { get => _roundedFlags; set { _roundedFlags = value; InvalidateVisual(); } }
        public EImageLayout ImageLayout { get => _imageLayout; set { _imageLayout = value; InvalidateVisual(); } }

        public ICommand Command { get => (ICommand)GetValue(CommandDepency); set => SetValue(CommandDepency,value); }
        public Color? ColorBackEnable { get => _clrBackEnable; set { _clrBackEnable = value; InvalidateVisual(); } }
        public Color? ColorBackDisable { get => _clrBackEnable; set { _clrBackEnable = value; InvalidateVisual(); } }
        public Color? ColorBackOver { get => _clrBackOver; set { _clrBackOver = value; InvalidateVisual(); } }
        public Color? ColorBackDown { get => _clrBackDown; set { _clrBackDown = value; InvalidateVisual(); } }
        public Color? ColorBackActiv { get => _clrBackActiv; set { _clrBackActiv = value; InvalidateVisual(); } }
        public Color? ColorBorderEnable { get => _clrBorderEnable; set { _clrBorderEnable = value; InvalidateVisual(); } }
        public Color? ColorBorderDisable { get => _clrBorderDisable; set { _clrBorderDisable = value; InvalidateVisual(); } }
        public Color? ColorBorderOver { get => _clrBorderOver; set { _clrBorderOver = value; InvalidateVisual(); } }
        public Color? ColorBorderDown { get => _clrBorderDown; set { _clrBorderDown = value; InvalidateVisual(); } }
        public Color? ColorBorderActiv { get => _clrBorderActiv; set { _clrBorderActiv = value; InvalidateVisual(); } }
        public Color ColorProgress { get => _clrProgress; set { _clrProgress = value; InvalidateVisual(); } }



        public ImageSource Image { get => (ImageSource)GetValue(ImageDepency); set { SetValue(ImageDepency, value); InvalidateVisual(); } }
        public Typeface Font { get => _fontFamilly; set { _fontFamilly = value; InvalidateVisual(); } }

        // constructor
        public RoundedControl()
        {
            InitializeComponent();
            //Clip = AyoControlHelpers.GenerateBorder(_roundedType, _radius, new Rect(0,0,Width,Height), _roundedFlags);
        }

        private static void UpdateControl(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is RoundedControl)
            {
                (sender as RoundedControl).InvalidateVisual();
            }
        }

        

        protected override void OnRender(DrawingContext drawingContext)
        {
            Brush bFore = new SolidColorBrush(CheckBorderColor());
            Brush bText = new LinearGradientBrush(AyoToolsUtility.AyoYellow, AyoToolsUtility.AyoLightGray, new Point(0.5, 0), new Point(0.5, 0.8));
            Brush bBack = new SolidColorBrush(CheckBackColor());

            Pen pFore = new Pen(bFore, 0);
            Pen pBack = new Pen(bBack, 0);


            Rect fullRect = new Rect(0, 0, ActualWidth, ActualHeight);
            float ratioX = 1 - (float)((IsActiv ? _activAccentuation : 1) * _borderSize) / (float)(RenderSize.Width);
            float ratioY = 1 - (float)((IsActiv ? _activAccentuation : 1) * _borderSize) / (float)(RenderSize.Height);
            Rect canRect = fullRect.ReZoom(ratioX, ESideResize.X);
            canRect = canRect.ReZoom(ratioY, ESideResize.Y);

            PathGeometry geometryBorder = AyoControlHelpers.GenerateBorder(_roundedType, _radius, fullRect, _roundedFlags);
            PathGeometry geometryCanvas = AyoControlHelpers.GenerateBorder(_roundedType, _radius, canRect, _roundedFlags);

            if (_borderSize > 0)
            {
                drawingContext.DrawGeometry(bFore, pFore, geometryBorder);
                if (_hasProgessBar)
                {
                    LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
                    linearGradientBrush.MappingMode = BrushMappingMode.RelativeToBoundingBox;
                    linearGradientBrush.StartPoint = new Point(0, 0);
                    linearGradientBrush.EndPoint = new Point(1, 0);
                    linearGradientBrush.GradientStops.Add(new GradientStop(_clrProgress, 0));
                    linearGradientBrush.GradientStops.Add(new GradientStop(_clrProgress,1* ((double)GetValue(ProgressPourcentDepency))));
                    linearGradientBrush.GradientStops.Add(new GradientStop(CheckBorderColor(),1.05*((double)GetValue(ProgressPourcentDepency)))); 
                    linearGradientBrush.GradientStops.Add(new GradientStop(CheckBorderColor(),1)); 
                    drawingContext.DrawGeometry(linearGradientBrush, pFore, geometryBorder);
                }
            }

            drawingContext.DrawGeometry(bBack, pBack, geometryCanvas);

            this.Canvas.Clip = geometryCanvas;

            ImageSource img = Image;
            if (img != null)
            {
                Rect rectImg = canRect;
                switch (_imageLayout)
                {
                    case EImageLayout.BestFit:
                        rectImg = canRect.GetBiggestRectWithoutDeformIn(new Rect(canRect.X, canRect.Y, img.Width, img.Height));
                        break;

                    case EImageLayout.Stretch:
                        rectImg = canRect;
                        break;

                    default:
                    case EImageLayout.None:
                        rectImg = new Rect(img.Width / 2, img.Height / 2, img.Width, img.Height);
                        break;
                }

                rectImg = rectImg.ReZoom(_zoomImage);
                drawingContext.DrawImage(img, rectImg);
            }

            //FormattedText formattedText = new FormattedText((_text == null) ? "" : _text, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, _fontFamilly, _sizeText, bText, 1);
            var temp = (string)GetValue(TextDepency);
            FormattedText formattedText = new FormattedText((temp == null) ? "" : temp, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, _fontFamilly, _sizeText, bText, 1);
            formattedText.TextAlignment = TextAlignment.Center;
            
            //formattedText.MaxTextWidth = Width - _borderSize;
            drawingContext.PushClip(geometryCanvas);
            if (formattedText.Width < canRect.Width)
                drawingContext.DrawText(formattedText, new Point(fullRect.Width / 2, fullRect.Height / 2 - formattedText.Height / 2));
            else
                drawingContext.DrawText(formattedText, new Point(fullRect.Width -formattedText.Width/2-_borderSize, fullRect.Height / 2 - formattedText.Height / 2));
            drawingContext.PushClip(geometryCanvas);

            if (!_isClickable || !_isAllowOver || !_isOverOnPicture)
                return;

            if (_isMouseIn && !_isMouseDown)
            {
                bBack.Opacity = ALPHA_OVER;
                drawingContext.DrawGeometry(bBack, pBack, geometryCanvas);
                drawingContext.DrawText(formattedText, new Point(fullRect.Width / 2, fullRect.Height / 2 - formattedText.Height / 2));
            }

        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            //if (sizeInfo.HeightChanged || sizeInfo.WidthChanged)
            //    Clip = AyoControlHelpers.GenerateBorder(_roundedType, _radius, new Rect(0, 0, Width, Height), _roundedFlags);

        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            InvalidateVisual();
        }


        #region override event
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            if (!_isClickable)
                return;

            _isMouseIn = true;
            InvalidateVisual();
            OnOver?.Invoke(this, e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (!_isClickable)
                return;

            _isMouseIn = false;
            _isMouseDown = false;
            InvalidateVisual();
            OnLeave?.Invoke(this, e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (!_isClickable)
                return;

            _isMouseDown = true;
            InvalidateVisual();
            OnDown?.Invoke(this, e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (!_isClickable)
                return;

            if (_isMouseDown)
            {
                if (_isAutoCheck)
                {
                    IsActiv = !IsActiv;
                    OnActivStateChanged?.Invoke(this, EventArgs.Empty);
                }
                FireOnClick();

            }

            _isMouseDown = false;
            InvalidateVisual();
            OnUp?.Invoke(this, e);
        }

        #endregion

        #region utility

        private Color CheckBackColor()
        {
            if (!IsEnabled)
                return _clrBackDisable.HasValue ? _clrBackDisable.Value : DEFAULT_BACK;
            if (_isMouseIn)
            {
                return _clrBackOver.HasValue ? _clrBackOver.Value : DEFAULT_BACK;
            }
            else if (IsActiv && !_isMouseIn)
            {
                return _clrBackActiv.HasValue ? _clrBackActiv.Value : DEFAULT_BACK;
            }
            else if (_isMouseDown)
            {
                return _clrBackDown.HasValue ? _clrBackDown.Value : DEFAULT_BACK;
            }
            else if (this.IsEnabled)
            {
                return _clrBackEnable.HasValue ? _clrBackEnable.Value : DEFAULT_BACK;
            }
            else
            {
                return _clrBackDisable.HasValue ? _clrBackDisable.Value : DEFAULT_BACK;
            }
        }

        private Color CheckBorderColor()
        {
            if (IsActiv)
            {
                return _clrBorderActiv.HasValue ? _clrBorderActiv.Value : DEFAULT_BORDER;
            }
            else if (_isMouseDown)
            {
                return _clrBorderDown.HasValue ? _clrBorderDown.Value : DEFAULT_BORDER;
            }
            else if (_isMouseIn)
            {
                return _clrBorderOver.HasValue ? _clrBorderOver.Value : DEFAULT_BORDER;
            }
            else if (this.IsEnabled)
            {
                return _clrBorderEnable.HasValue ? _clrBorderEnable.Value : DEFAULT_BORDER;
            }
            else
            {
                return _clrBorderDisable.HasValue ? _clrBorderDisable.Value : DEFAULT_BORDER;
            }
        }

        protected void FireOnClick()
        {
            Command?.Execute((CommandParamater == null) ? this.Tag : CommandParamater);
            OnClick?.Invoke(this, EventArgs.Empty);
        }

        #endregion

    }
}
