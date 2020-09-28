using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace MediaTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ObservableCollection<MediaItem> lMediaItem;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(cmb.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lMediaItem = new ObservableCollection<MediaItem>();
            listView.ItemsSource = lMediaItem;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "视频文件|*.mkv;*.avi;*.mp4";
            if (ofd.ShowDialog() == true)
            {
                foreach (String fileName in ofd.FileNames)
                {
                    lMediaItem.Add(new MediaItem(fileName));
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            for (int i = listView.SelectedItems.Count - 1; i >= 0; i--)
            {
                lMediaItem.Remove((MediaItem)listView.SelectedItems[i]);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            String s;
            foreach (MediaItem item in lMediaItem)
            {
                item.Status = "等待中";
            }
            new Thread(() =>
            {
                foreach (MediaItem item in lMediaItem)
                {
                    item.Status = "进行中";
                    s = $"-i \"{item.Path}\"";
                    if (item.BitRate != "") s += $" -b:v {item.BitRate}";
                    if (item.Resolution != "") s += $" -vf scale={item.Resolution.Replace("*", ":")}";
                    if (item.BitRate == "" && item.Resolution == "") s += " -vcodec copy";
                    s += $" -acodec copy \"{item.Directory}\\{item.TargetName}\"";
                    Process process = new Process();
                    ProcessStartInfo processStartInfo = new ProcessStartInfo("ffmpeg.exe", s);
                    processStartInfo.UseShellExecute = false;
                    process.StartInfo = processStartInfo;
                    process.Start();
                    process.WaitForExit();
                    int r = process.ExitCode;
                    if (r == 0) item.Status = "已完成"; else item.Status = "错误";
                }
            }).Start();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            lMediaItem.Clear();
        }
    }
}
