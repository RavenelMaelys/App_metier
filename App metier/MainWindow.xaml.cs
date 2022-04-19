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

        private void Missions_btn_Click(object sender, RoutedEventArgs e)
        {
            /*Apréhender les projets
             * Concevoir un plan de developpement
             * le besoin du client
             * Developpement
             * Suivi du developememnt
             * *evolution du besoin
             * *gerer le developement (temps, imprévus)
             * restitution du logiciel
             * Support logiciel*/
        }

        private void Profil_btn_Click(object sender, RoutedEventArgs e)
        {
            /*Maitrise de logiciels (Environnement de developement)
             * Maitrise des languages (C#, C++, java, Python)
             *Maitrise de l'anglais
             *Communication skills
             *Managment skills
             */
        }

        private void Carri_btn_Click(object sender, RoutedEventArgs e)
        {
            /*Salaire : 30k-35 k = junior, 40k = millieu, Sr= 49k
             * Evolution possible
             */
        }

        private void Sect_btn_Click(object sender, RoutedEventArgs e)
        {
            var Secteurs = new Secteurs();
            this.MainContent.Content = Secteurs;
            /*
             * 
             * 
             * 
             */
        }
    }
}
