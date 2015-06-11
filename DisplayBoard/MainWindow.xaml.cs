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
using System.Windows.Media.Animation;
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
            this.button1.Click+= new RoutedEventHandler(button_click);
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

        private void ListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
        private void button_click(object sender,RoutedEventArgs e)
        {
            MessageBox.Show("222233333");
            DoubleAnimation []dax = new DoubleAnimation[5];
            DoubleAnimation []day = new DoubleAnimation[5];
            //指定起点  
            dax[0] = new DoubleAnimation();
            day[0]=new DoubleAnimation();
            dax[0].From = 300;
            day[0].From = 0;

            //指定终点  
            dax[0].To = 0;
            day[0].To = 0;
            
            //指定时长  
            Duration duration = new Duration(TimeSpan.FromMilliseconds(3000));
            dax[0].Duration = duration;
            day[0].Duration = duration;

            //动画主体是TranslatTransform变形，而非Button  
            this.tt3_copy.BeginAnimation(TranslateTransform.XProperty, dax[0]);
            this.tt3_copy.BeginAnimation(TranslateTransform.YProperty, day[0]);  
        
        }

        

      
    }
}
