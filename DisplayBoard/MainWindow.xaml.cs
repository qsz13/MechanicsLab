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
using System.Runtime.InteropServices;
using DisplayBoard.Model;


namespace DisplayBoard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        int elpase = 3;
        int Logintime = 0;
        string temp = "";
        DoubleAnimation dax = new DoubleAnimation();
        DoubleAnimation day = new DoubleAnimation();
        MyMessage ongoing = new MyMessage();
        MyMessage upcoming = new MyMessage();
        MyMessage tomorrow = new MyMessage();
        public MainWindow()
        {
            InitializeComponent();
            initTimer();
            initClock();
            initDate();
            initMedia();
            try
            {
                LabClient.Login("a091116", "222222");
            }
            catch {
                Console.WriteLine("login fail");
            }
            initAnimation();
        }

        private void initMedia()
        {
            media.MediaEnded += new RoutedEventHandler(media_MediaEnded);
            media.Loaded += new RoutedEventHandler(media_Loaded);
            media.Unloaded += new RoutedEventHandler(media_Unloaded);  
        }

        private void media_Unloaded(object sender, RoutedEventArgs e)
        {
            (sender as MediaElement).Stop();  
        }

        private void media_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as MediaElement).Play();  
        }

        private void media_MediaEnded(object sender, RoutedEventArgs e)
        {
            (sender as MediaElement).Stop();
            (sender as MediaElement).Play();  
        }
        private void initAnimation()
        {
            //指定起点  
            dax= new DoubleAnimation();
            day = new DoubleAnimation();
            dax.From = 400;
            day.From = 0;
            //指定终点  
            dax.To = 0;
            day.To = 0;
            //指定时长  
            Duration duration = new Duration(TimeSpan.FromMilliseconds(3000));
            dax.Duration = duration;
            day.Duration = duration;
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
            if(d.Minute<10)
                clock.Text = d.Hour + " : 0" + d.Minute;
            else
                clock.Text = d.Hour + " : " + d.Minute;
            
        }

        private void initDate()
        {
            DateTime d = DateTime.Now;
            int[] arr = { d.Year, d.Month, d.Day };
            String m="";
            switch (d.DayOfWeek)
            { 
                case DayOfWeek.Monday:
                    m = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    m = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    m = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    m = "星期四";
                    break;
                case DayOfWeek.Friday:
                    m = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    m = "星期六";
                    break;
                case DayOfWeek.Sunday:
                    m = "星期日";
                    break;
            }
            date_cn.Text = string.Join(".", arr) + " " + m+ " 今日实验 ："+DataUtil.todayExpSum.ToString();
            tomo_test.Text = "明日实验 ：" + DataUtil.tomorrowExpSum.ToString();
        }
        private void change_content(Grid g,MyMessage m)
        {
            m.getmm();
            for(int i=0;i<5;i++){
                Viewbox vb = g.Children[i] as Viewbox;
                TextBlock tb = vb.Child as TextBlock;
                tb.Text = m.mm[i];
            }
        }
        //private class MyMessage 
        //{
        //    public MyMessage(String s){
        //        m_statu = m_statu + s;
        //    }
        //    public String m_lab = "1号实验室";
        //    public String m_statu = "进行:";
        //    public String m_content = "未知名实验";
        //    public String m_class = "神秘地带";
        //    public String m_people = "黑衣人";
        //    public String[] mm=new String[8];
        //    public void getmm()
        //    {
        //        mm[0] = m_lab;
        //        mm[1] = m_statu;
        //        mm[2] = m_content;
        //        mm[3] = m_class;
        //        mm[4] = m_people;
        //    }
        //}
        private void Timer_Click(object sender, EventArgs e)
        {
            initDate();
            initClock();
            elpase++;

            if (elpase > 4)
            {
                change_content(this.ongoingview, ongoing);
                change_content(this.upcomingview, upcoming);
                change_content(this.tomorrowview, tomorrow);
                temp = clock.Text+DateTime.Now.Second;
                elpase = 0;
                Start_animation();
            }
            Logintime++;
            if (Logintime > 3000)
            {
                LabClient.Login("a091116", "222222");
                Logintime = 0;
            }
        }

        private void Start_animation()
        {
            //input your message
            MyMessage temp;
            temp = DataUtil.getNextOngoingMyMsg();
            if (!temp.Equals(ongoing))
            {
                change_content(this.ongoingAnimaView, temp);
                this.tt1_copy.BeginAnimation(TranslateTransform.XProperty, dax);
                this.tt1_copy.BeginAnimation(TranslateTransform.YProperty, day);
                ongoing = temp;

            }
            temp = DataUtil.getNextUpcomingMyMsg();
           
            if (!temp.Equals(upcoming))
            {
                change_content(this.upcomingAnimaView, temp);
                this.tt3_copy.BeginAnimation(TranslateTransform.XProperty, dax);
                this.tt3_copy.BeginAnimation(TranslateTransform.YProperty, day);
                
                upcoming = temp;
            }
            temp = DataUtil.getNextTomorrowMyMsg();
            if (!temp.Equals(tomorrow))
            {
                change_content(this.tomorrowAnimaView, temp);
                this.tt4_copy.BeginAnimation(TranslateTransform.XProperty, dax);
                this.tt4_copy.BeginAnimation(TranslateTransform.YProperty, day);
                tomorrow = temp;
            }
        }

        

      
    }
}
