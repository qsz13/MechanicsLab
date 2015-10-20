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

        public void PlayList()
        {
            if (files.Count > 0)
            {
                this.UnloadedBehavior = MediaState.Manual;
                this.LoadedBehavior = MediaState.Manual;
                this.MediaEnded += new RoutedEventHandler(media_MediaEnded);
                this.Source = new Uri(files[index], UriKind.RelativeOrAbsolute);
                this.Play();
            }
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
            this.Source = new Uri(files[++index % files.Count], UriKind.RelativeOrAbsolute);
            this.Play();
        }

        private ObservableCollection<string> files = new ObservableCollection<string>();
        private int index = 0;
        private string[] filters = new string[] { "*.mp4","*.wmv" };
    }
}
