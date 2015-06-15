using QuerySystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        int NowPage = 1, TotalPage = 1,TotalItem=0,Time=-1;
        String StudentName="张三";
        ObservableCollection<MyMessage> MessageList = new ObservableCollection<MyMessage>();
        public MainWindow()
        {
            InitializeComponent();
            LabClient.Login("a091116", "222222");

            initTimer();
            this.keyboard.Visibility = Visibility.Hidden;
            this.SearchResultView.Visibility = Visibility.Hidden;
        }
        private void initTimer()
        {
            Timer.Tick += new EventHandler(countdown);
            Timer.Interval = new TimeSpan(0, 0, 1);

        }
        public void countdown(object sender, EventArgs e)
        {
            if (Time > 0)
            {
                Time--;
                this.TimePass.Text = Time.ToString() + "秒后自动退出";
                return ;
            }
            else if (Time == 0)
            {
                Time--;
                this.TimePass.Text = Time.ToString() + "秒后自动退出";
                Timer.Stop();
                Log_out(null, null);
            }
        }
        private void TextBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
           // this.input.Text = "12345";
            this.keyboard.Visibility = Visibility.Visible;
        }

        private void search_click(object sender, RoutedEventArgs e)
        {
            if (this.input.Text.Equals(""))
                return;
            this.keyboard.Visibility = Visibility.Hidden;
            //input user id exist?
            //LabClient.Login("a091116", "222222");
            String a = this.input.Text;
            DataUtil.accountId=Int32.Parse(a);
            bool UserExist=true;
            if (UserExist == true)
            {
                Timer.Start();
                getMessage(-1);
                setGridView();
       
            }
            else {
                MessageBox.Show("该用户不存在");
            }
        }

        private void setGridView()
        {
            this.User_Name.Text = "你好! " + StudentName;
            Time = 60;
            this.TimePass.Text = Time.ToString() + "秒后自动退出";
            this.Second_Title.Text = "共" + TotalItem.ToString() + "个预约,当前第 " + NowPage.ToString() + " 页  共 " + TotalPage.ToString() + " 页";
            this.DataGridView.DataContext = MessageList;
            this.SearchResultView.Visibility = Visibility.Visible;
        }

        private void getMessage(int statu)
        {
            MessageList.Clear();
            List<MyMessage> Temp;
            if (statu == -1)
                Temp = DataUtil.goPriviousPage();
            else
                Temp = DataUtil.goNextPage();
            if (Temp == null)
            {
                MessageList.Add(new MyMessage());
                return;
            }
            for (int i = 0; i < Temp.Count; i++)
            {
                MessageList.Add(Temp[i]);
            }

            NowPage = DataUtil.curPageNum;
            TotalPage = DataUtil.totalPageNum;
            TotalItem = DataUtil.totalItemNum;
            Time = 60;
            
        }
        private void close_search_view(object sender, RoutedEventArgs e)
        {
            this.SearchResultView.Visibility = Visibility.Visible;
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

        private void Log_out(object sender, RoutedEventArgs e)
        {
            this.input.Text = "";
            this.InputSearchIDView.Visibility = Visibility.Visible;
    //        this.keyboard.Visibility = Visibility.Hidden;
            this.SearchResultView.Visibility = Visibility.Hidden;
        }

        private void Change_Page(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            if (bt.Tag.Equals("-1"))
            {
                if (NowPage <= 1)
                    return;
                else
                {
                    getMessage(-1);
                    setGridView();
                }
            }
            
            if (bt.Tag.Equals("1"))
            {
                if (NowPage >= TotalPage)
                    return;
                else
                {
                    getMessage(+1);
                    setGridView();
                }
            }
        }

    }
}
