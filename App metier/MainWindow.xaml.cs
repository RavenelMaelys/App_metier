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
        private void Missions_btn_Click(object sender, RoutedEventArgs e)
        {
            //var Missions = new Missions();
            //this.MainContent.Content = Missions;
        }

        private void Profil_btn_Click(object sender, RoutedEventArgs e)
        {
            //var Profile = new Profile();
            //this.MainContent.Content = Profile;
            


            /*Maitrise de logiciels (Environnement de developement)
             * Maitrise des languages (C#, C++, java, Python)
             *Maitrise de l'anglais
             *Communication skills
             *Managment skills
             */
        }




        private void Carri_btn_Click(object sender, RoutedEventArgs e)
        {
            //var Carriere = new Carrière();
            //this.MainContent.Content = Carriere;


            /*Salaire : 30k-35 k = junior, 40k = millieu, Sr= 49k
             * Evolution possible
             */
        }

        private void Sect_btn_Click(object sender, RoutedEventArgs e)
        {
            //var Secteurs = new Secteurs();
            //this.MainContent.Content = Secteurs;
        }


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
}
