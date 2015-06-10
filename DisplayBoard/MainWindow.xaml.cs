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

namespace DisplayBoard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
 
        public MainWindow()
        {
            InitializeComponent();
            initTimer();
            initClock();
            initDate();
        }

        private void initTimer()
        {
            Timer.Tick += new EventHandler(Timer_Click);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
        }


        public void initClock()
        {
            DateTime d = DateTime.Now;
            clock.Text = d.Hour + " : " + d.Minute;
            
        }

        private void initDate()
        {
            DateTime d = DateTime.Now;
            int[] arr = { d.Year, d.Month, d.Day };

            date_cn.Text = string.Join(".", arr);
        }

        private void Timer_Click(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;
            clock.Text = d.Hour + " : " + d.Minute;

        }

        

      
    }
}
