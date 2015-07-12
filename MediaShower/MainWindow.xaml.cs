
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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

namespace MediaShower
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int scroll_pos = 0;
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        private void initTimer()
        {
            Timer.Tick += new EventHandler(Timer_Click);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
        }

        private void Timer_Click(object sender, EventArgs e)
        {
            ScrollViewer scroll = FindVisualChild<ScrollViewer>(Pic_list);
            scroll.ScrollToHorizontalOffset(scroll_pos++);
        }


        public MainWindow()
        {
            InitializeComponent();
            initList();
            initTimer();
        }

        private void initList()
        {
            GetAllDirList(new DirectoryInfo(System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)));
            for (int i = 0; i < files.Count; i++)
            {
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(files[i], UriKind.RelativeOrAbsolute));
                image.Tag = files[i];
                image.Margin = new Thickness(100, 100, 100, 100);
                image.MaxHeight = 100;
                image.MaxWidth = 100;
                image.MouseLeftButtonDown += ClickMethod;
                
                Pic_list.Items.Add(image);
                
            }
        }
        public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            if (obj != null)
            {
                int k = VisualTreeHelper.GetChildrenCount(obj);
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {

                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                    if (child != null && child is T)
                    {
                        return (T)child;
                    }
                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null) return childItem;
                }
            }
            return null;
        }  
        private void ClickMethod(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image)sender;
            String name = image.Tag.ToString();
          //  MediaEnded += new RoutedEventHandler(media_MediaEnded);
           // this.Source = new Uri(files[index], UriKind.RelativeOrAbsolute);
          //  this.Play();
        }
        private void GetAllDirList(DirectoryInfo directory)
        {
            foreach (string filter in filters)
            {
                foreach (FileInfo file in directory.GetFiles(filter))
                {
                    files.Add(file.FullName);
                }
            }
            foreach (DirectoryInfo subDirectory in directory.GetDirectories())
            {
                GetAllDirList(subDirectory);
            }
        }
        private ObservableCollection<string> files = new ObservableCollection<string>();
        private string[] filters = new string[] { "*.png" };
    }
}
