using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.IO;
using System.Configuration;
using System.Windows;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Media;

namespace MediaApplication
{

    public class MediaManager : MediaElement
    {
        public MediaManager()
        {
           
            try
            {
                GetAllDirList(new DirectoryInfo(System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)));
            }
            catch
            {

            }
        }

        public void PlayList(String fileName)
        {
            this.Visibility = Visibility.Visible;

            this.UnloadedBehavior = MediaState.Manual;
            this.LoadedBehavior = MediaState.Manual;
            
            this.Source = new Uri(fileName, UriKind.RelativeOrAbsolute);
            this.Play();

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

        private void media_MediaEnded(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            this.Parent.GetLocalValueEnumerator();
            Grid grid = FindVisualChild<Grid>(this.Parent);
            grid.Visibility = Visibility.Visible;          
            //this.Source = new Uri(files[++index % files.Count], UriKind.RelativeOrAbsolute);
  //          this.Play();
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
        private ObservableCollection<string> files = new ObservableCollection<string>();
        private int index = 0;
        private string[] filters = new string[] { "*.mp4" };
    }
}
