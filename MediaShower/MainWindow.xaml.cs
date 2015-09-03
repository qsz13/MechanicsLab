
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
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        private void initTimer()
        {
            Timer.Tick += new EventHandler(Timer_Click);
            Timer.Interval = TimeSpan.FromMilliseconds(50);
            Timer.Start();
        }

        private void Timer_Click(object sender, EventArgs e)
        {
            if (media.NaturalDuration.HasTimeSpan)
            {
                time_slider.Value = media.Position.TotalMilliseconds;
            }
            time_slider.ToolTip = media.Position.ToString().Substring(0, 8);
           // ScrollViewer scroll = FindVisualChild<ScrollViewer>(Pic_list);
           // scroll.ScrollToHorizontalOffset(scroll_pos++);
        }


        public MainWindow()
        {
            InitializeComponent();
            initList();
            initTimer();
            back_button.Visibility = Visibility.Hidden;
            black_back.Visibility = Visibility.Hidden;
            time_slider.Visibility = Visibility.Hidden;
        }

        private void initList()
        {
            GetAllDirList(new DirectoryInfo(System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)));
            for (int i = 0; i < files.Count; i++)
            {
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(files[i], UriKind.RelativeOrAbsolute));
                image.Tag = files[i];
                image.Margin = new Thickness(10, 10, 10, 10);
                image.MaxHeight = 100;
                image.MaxWidth = 100;
                image.MouseLeftButtonDown += ClickMethod;
                Grid.SetRow(image, i/5);
                Grid.SetColumn(image, i%5);
                pic_grid.Children.Add(image);
                
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
            back_button.MouseLeftButtonDown += back_button_click;
            back_button.Visibility = Visibility.Visible;
            black_back.Visibility = Visibility.Visible;
            time_slider.Visibility = Visibility.Visible;
            Image image = (Image)sender;

            String name = image.Tag.ToString();
            
            media.PlayList(name.Replace("png","mp4"));
            media.MediaEnded += back_button_click;
            pic_grid.Visibility = Visibility.Hidden;
            
            //MessageBox.Show(name);
          //  MediaEnded += new RoutedEventHandler(media_MediaEnded);
           // this.Source = new Uri(files[index], UriKind.RelativeOrAbsolute);
          //  this.Play();
        }

        private void back_button_click(object sender, RoutedEventArgs e)
        {
            back_button.MouseLeftButtonDown -= back_button_click;
            pic_grid.Visibility = Visibility.Visible;
            back_button.Visibility = Visibility.Hidden;
            black_back.Visibility = Visibility.Hidden;
            media.Visibility = Visibility.Hidden;
            time_slider.Visibility = Visibility.Hidden;
            media.Close();
           
            
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

        private void time_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int SliderValue = (int)(time_slider.Value);
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            TimeSpan min= new TimeSpan(0,0,0,0, 500);
            if(media.Position > ts && media.Position-ts>min)
                media.Position = ts;
            if(media.Position < ts && ts-media.Position > min)
                media.Position = ts;
 
        }
        private void time_slider_Drop(object sender, DragEventArgs e)
        {
            int SliderValue = (int)(time_slider.Value);
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            media.Position = ts;
            
        }

        private void time_slider_MouseUp(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void time_slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void media_MediaOpened(object sender, RoutedEventArgs e)
        {
            time_slider.Maximum = media.NaturalDuration.TimeSpan.TotalMilliseconds;
        }


    }
}
