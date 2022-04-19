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

namespace App_metier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

        private void Missions_btn_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Sect_btn_Loaded(object sender, RoutedEventArgs e)
        {
            var Secteur = new Secteurs();
            this.MainContent.Content = Secteur;
        }

        private void Formation_btn_Loaded(object sender, RoutedEventArgs e)
        {
            var Format = new 
        }

        private void Evol_btn_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Profil_btn_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
