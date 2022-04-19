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
using System.Windows.Shapes;

namespace WPFUI.Themes
{
    /// <summary>
    /// Interaction logic for RoundedMessageBox.xaml
    /// </summary>
    public partial class RoundedMessageBox : Window
    {
        

        private RoundedMessageBox()
        {
            InitializeComponent();
        }

        private void btnOK_OnClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Header_OnDown(object sender, MouseEventArgs e)
        {
            this.DragMove();
        }




        public static void Show(string msg)
        {
            RoundedMessageBox roundedMessageBox = new RoundedMessageBox();
            roundedMessageBox.lbMessage.Text = msg;
            roundedMessageBox.ShowDialog();
        }
    }
}
