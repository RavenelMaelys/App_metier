using AyoControlLibrary;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for RoundedTexteBox.xaml
    /// </summary>
    public partial class RoundedTexteBox : UserControl , IClickable, IActivable
    {
        public event TextChangedEventHandler TextChanged { add => textBox.TextChanged += value; remove => textBox.TextChanged -= value; }
        public event EventHandler OnClick;
        public event EventHandler OnActivStateChanged;

        public static readonly DependencyProperty TextDepency = DependencyProperty.RegisterAttached(nameof(Text), typeof(string), typeof(RoundedTexteBox), new PropertyMetadata("Ayo Text"/*, new PropertyChangedCallback(UpdateControl)*/));
        //public static readonly DependencyProperty TextDepency = TextBox.TextProperty;


        private bool _mouseDownTb = false;
        private bool _isAutoCheck = false;



        // accessor

        public bool IsActiv { get => Back.IsActiv; set { Back.IsActiv = value; InvalidateVisual(); Back.InvalidateVisual(); } }
        public bool IsAutoCheck { get => _isAutoCheck; set => _isAutoCheck = value; }


        public int CornerRadius { get => Back.CornerRadius; set { Back.CornerRadius = value; InvalidateVisual(); } }
        public int BorderSize { get => Back.BorderSize; set {Back.BorderSize = value; InvalidateVisual(); } }
           
        public double SizeText { get => textBox.FontSize; set { textBox.FontSize = value; InvalidateVisual(); } }
        
        public string Text { get => (string)GetValue(TextDepency); set => SetValue(TextDepency,value); }


        public ERoundedType RoundedType { get => Back.RoundedType; set { Back.RoundedType = value; InvalidateVisual(); } }
        public ERoundedFlag RoundedFlag { get => Back.RoundedFlag; set { Back.RoundedFlag = value; InvalidateVisual(); } }
        public TextAlignment TextAlignment { get => textBox.TextAlignment; set { textBox.TextAlignment = value; InvalidateVisual(); } }


        public Color? ColorBackEnable { get => Back.ColorBackEnable; set { Back.ColorBackEnable = value; InvalidateVisual(); } }
        public Color? ColorBackDisable { get => Back.ColorBackDisable; set { Back.ColorBackDisable = value; InvalidateVisual(); } }
        public Color? ColorBackOver { get => Back.ColorBackOver; set { Back.ColorBackOver = value; InvalidateVisual(); } }
        public Color? ColorBackDown { get => Back.ColorBackDown; set { Back.ColorBackDown = value; InvalidateVisual(); } }
        public Color? ColorBackActiv { get => Back.ColorBackActiv; set { Back.ColorBackActiv = value; InvalidateVisual(); } }
        public Color? ColorBorderEnable { get => Back.ColorBorderEnable; set { Back.ColorBorderEnable = value; InvalidateVisual(); } }
        public Color? ColorBorderDisable { get => Back.ColorBorderEnable; set { Back.ColorBorderEnable = value; InvalidateVisual(); } }
        public Color? ColorBorderOver { get => Back.ColorBorderEnable; set { Back.ColorBorderEnable = value; InvalidateVisual(); } }
        public Color? ColorBorderDown { get => Back.ColorBorderDown; set { Back.ColorBorderDown = value; InvalidateVisual(); } }
        public Color? ColorBorderActiv { get => Back.ColorBorderActiv; set { Back.ColorBorderActiv = value; InvalidateVisual(); } }


        public RoundedTexteBox()
        {
            InitializeComponent();
            textBox.DataContext = this;
            textBox.Foreground = new LinearGradientBrush(AyoToolsUtility.AyoYellow, AyoToolsUtility.AyoLightGray, new Point(0.5, 0), new Point(0.5, 0.8));
            textBox.FontFamily = AyoToolsUtility.AyoFontFamily.FontFamily;

            //textBox.MouseLeftButtonDown -= TextBox_MouseLeftButtonDown;
            //textBox.MouseLeftButtonDown += TextBox_MouseLeftButtonDown;

            //textBox.MouseLeftButtonUp -= TextBox_MouseLeftButtonUp;
            //textBox.MouseLeftButtonUp += TextBox_MouseLeftButtonUp;

            textBox.GotFocus -= TextBox_GotFocus;
            textBox.GotFocus += TextBox_GotFocus;

            //textBox.MouseDown -= TextBox_MouseDown;
            //textBox.MouseDown += TextBox_MouseDown;

            //textBox.MouseUp -= TextBox_MouseUp;
            //textBox.MouseUp += TextBox_MouseUp;

            //textBox.MouseLeave -= TextBox_MouseLeave;
            //textBox.MouseLeave += TextBox_MouseLeave;

            Back.OnClick -= Back_OnClick;
            Back.OnClick += Back_OnClick;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            OnClick?.Invoke(this, EventArgs.Empty);
            if (_isAutoCheck)
            {
                IsActiv = !IsActiv;
                OnActivStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void TextBox_MouseLeave(object sender, MouseEventArgs e) => _mouseDownTb = false;

        private void TextBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_mouseDownTb)
                OnClick?.Invoke(this, EventArgs.Empty);

            _mouseDownTb = false;
        }

        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e) => _mouseDownTb = true;

        private void Back_OnClick(object sender, EventArgs e) => OnClick?.Invoke(this, EventArgs.Empty);

        private void TextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _mouseDownTb = true;
        }

        private void TextBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_mouseDownTb)
            {
                OnClick?.Invoke(this, EventArgs.Empty);
                if (_isAutoCheck)
                {
                    IsActiv = !IsActiv;
                    OnActivStateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
            _mouseDownTb = false;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            //InvalidateVisual();
            //Back?.InvalidateVisual();
            //textBox?.InvalidateVisual();
        }

       // private static void UpdateControl(DependencyObject sender, DependencyPropertyChangedEventArgs e)
       //{
       //     if (sender is RoundedTexteBox)
       //     {
       //         (sender as RoundedTexteBox).InvalidateVisual();
       //         (sender as RoundedTexteBox).textBox.Text = = (string)e.NewValue;
       //     }
       // }
    }
}
