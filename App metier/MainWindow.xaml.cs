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

        List<RoundedControl> _btn = new List<RoundedControl>();


        public MainWindow()
        {
            InitializeComponent();

            _btn.Add(Missions_btn);
            _btn.Add(Sect_btn);
            _btn.Add(Formation_btn);
            _btn.Add(Evol_btn);
            _btn.Add(Profil_btn);


            this.SizeChanged += MainWindow_SizeChanged;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
         //   int widthBtn;
        }




        #region event from UI
        private void btnQuit_OnClick(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
            Application.Current.Shutdown();
        }

        private void btnMax_OnClick(object sender, EventArgs e) =>
            WindowState = (WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;

        private void btnMin_OnClick(object sender, EventArgs e) => WindowState = WindowState.Minimized;

        private void Header_OnDown(object sender, MouseEventArgs e)
        {
            this.DragMove();
        }

        #endregion

        private void btn_OnClick(object sender, EventArgs e)
        {
            if ((sender as RoundedControl) == null)
                return;

            RoundedControl control = (RoundedControl)sender;

            foreach (RoundedControl rctrl in _btn)
            {
                rctrl.IsActiv = rctrl.Name == control.Name;
            }
        }
    }
}
