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

namespace QuerySystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.keyboard.Visibility = Visibility.Hidden;
        }

        private void TextBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            this.input.Text = "12345";
            this.keyboard.Visibility = Visibility.Visible;
        }

        private void search_click(object sender, RoutedEventArgs e)
        {
            this.keyboard.Visibility = Visibility.Hidden;
        }
        //input keyboard listener
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button temp = sender as Button ;
            if (temp.Tag.Equals("delete"))
            {
                if(this.input.Text.Length>0)
                    this.input.Text=this.input.Text.Remove(this.input.Text.Length - 1);
                return;
            }
            if( temp.Tag.Equals("enter"))
            {
               
                search_click(null,null);
                return;
            }
            if(input.Text.Length<14)
                this.input.Text = this.input.Text + temp.Tag;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

    }
}
