using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFUI.Themes;

namespace App_metier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {

        List<ControlBinding> _btn = new List<ControlBinding>();


        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();

            _btn.Add(new ControlBinding(Missions_btn, mission));
            _btn.Add(new ControlBinding(Sect_btn, sector));
            _btn.Add(new ControlBinding(Formation_btn, formation));
            _btn.Add(new ControlBinding(Evol_btn, cariere));
            _btn.Add(new ControlBinding(Profil_btn, profil));

            this.SizeChanged += MainWindow_SizeChanged;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double widthBtn = this.Width / 5;
            for (int i = 0; i < _btn.Count; i++)
            {
                _btn[i].RoundedBtn.Width = widthBtn;
                if(i!=0)
                {
                    Thickness thicknessLast = _btn[i - 1].RoundedBtn.Margin;
                    Thickness thicknessCurrent = _btn[i].RoundedBtn.Margin;
                    _btn[i].RoundedBtn.Margin = new Thickness(thicknessLast.Left+widthBtn, thicknessCurrent.Top, thicknessCurrent.Right, thicknessCurrent.Bottom);
                }
                else
                {
                    Thickness thicknessCurrent = _btn[i].RoundedBtn.Margin;
                    _btn[i].RoundedBtn.Margin = new Thickness(0, thicknessCurrent.Top, thicknessCurrent.Right, thicknessCurrent.Bottom);
                }
            }
        }


        private void btn_OnClick(object sender, EventArgs e)
        {
            if ((sender as RoundedControl) == null)
                return;

            RoundedControl control = (RoundedControl)sender;

            foreach (ControlBinding rctrl in _btn)
            {
                rctrl.IsActiv = rctrl.RoundedBtn.Name == control.Name;
            }
        }




        #region utility
        private void btnQuit_OnClick(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
            Application.Current.Shutdown();
        }

        private void btnMax_OnClick(object sender, EventArgs e) =>   WindowState = (WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;

        private void btnMin_OnClick(object sender, EventArgs e) => WindowState = WindowState.Minimized;

        private void Header_OnDown(object sender, MouseEventArgs e)
        {
            this.DragMove();
        }

        #endregion


    }


    class ControlBinding
    {
        private RoundedControl btn;
        private Control control;




        public bool IsActiv { get => btn.IsActiv;
            set
            {
                btn.IsActiv = value;
                if (control == null)
                    return;
                control.Visibility = (value)?Visibility.Visible : Visibility.Hidden;
            }
        }


        public RoundedControl RoundedBtn => btn;


        public ControlBinding(RoundedControl btn, Control control)
        {
            this.btn = btn;
            this.control = control;
        }
    }
}
