using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfKuGou_Beta.ViewMode;
using WpfKuGou_Beta.Mode;
using System.Windows.Threading;
using Shell32;
using LyricShows;
using Lyric;
using System.Windows.Media.Animation;

namespace WpfKuGou_Beta
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MusicViewMode viewMode;
        private DispatcherTimer timer;
        private DispatcherTimer scrollTextTimer;


        DoubleAnimation  scrollTextAni;
        DoubleAnimation  scrollTextAni2;
        /// 自定义的歌词秀类
        /// </summary>
        private LyricShow lyricShow;
        /// <summary>
        /// 自定义的解析歌词类
        /// </summary>
        GetLyricAndLyricTime getLT;

       

        private LycSearch lycSearch;
        public MainWindow()
        {
            InitializeComponent();

            Storyboard baiyun = Resources["cloudMove"] as Storyboard;
            baiyun.Begin();

            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            timer.Tick += timer_Tick;
            timer.Start();

            scrollTextTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 8)
            };
            scrollTextTimer.Tick += scrollTextTimer_Tick;
            scrollTextTimer.Start();

            scrollTextAni = new DoubleAnimation();
            scrollTextAni2 = new DoubleAnimation();

            viewMode = new MusicViewMode();
            SongList.ItemsSource = viewMode.SongList;


            lyricShow = new LyricShow(CanvasLyric, StackPanelCommonLyric, CanvasFocusLyric, TBFocusLyricBack, CanvasFocusLyricFore, TBFocusLyricFore);
            getLT = new GetLyricAndLyricTime();

            lycSearch = new LycSearch();
            //lyricShow = new LyricShow(CanvasLyric, StackPanelCommonLyric, CanvasFocusLyric, TBFocusLyricBack, CanvasFocusLyricFore, TBFocusLyricFore);
            //关联绑定
            Web.Source = new Uri( "http://www2.kugou.kugou.com/yueku/v8/html/home.html");
             
        }

        void scrollTextAni_Completed(object sender, EventArgs e)
        { 
            //double top = Canvas.GetTop(textBlock2);
            //if (top <= -20)
            //{
            //    Canvas.SetTop(textBlock2, 0d);
              
            //}

            //top = Canvas.GetTop(textBlock3);
            //if (top <= -20)
            //{
               
            //    Canvas.SetTop(textBlock3, 0d);
               
            //} 
        }

        void scrollTextTimer_Tick(object sender, EventArgs e)
        {
        
            scrollTextAni.From = 0d;
            scrollTextAni.To = -20d;

            scrollTextAni2.From = 20d;
            scrollTextAni2.To = 0d;

            scrollTextAni.Duration = new Duration(new TimeSpan(0, 0, 5));
            scrollTextAni2.Duration = new Duration(new TimeSpan(0, 0, 5));

            textBlock2.BeginAnimation(Canvas.TopProperty, scrollTextAni);
            textBlock3.BeginAnimation(Canvas.TopProperty, scrollTextAni2);
        }

        void timer_Tick(object sender, EventArgs e)
        {
           if ( Sound.NaturalDuration.HasTimeSpan)
           {

              

               TimeSpan tPos = Sound.Position;

               slider_play.Value = tPos.TotalSeconds;

               LyricShow.refreshLyricShow(tPos.TotalSeconds);

               Hasplay.Text = tPos.ToString("mm\\:ss");
           }
       
        }


        /************************ 引用COM组件Microsoft Shell Controls And Automation来获取文件信息  ********************************************************
      iCol 对应文件详细属性汇总
      ID  => DETAIL-NAME
      0   => Name
      1   => Size     // MP3 文件大小
      2   => Type
      3   => Date modified
      4   => Date created
      5   => Date accessed
      6   => Attributes
      7   => Offline status
      8   => Offline availability
      9   => Perceived type
      10  => Owner
      11  => Kinds
      12  => Date taken
      13  => Artists   // MP3 歌手
      14  => Album     // MP3 专辑
      15  => Year
      16  => Genre
      17  => Conductors
      18  => Tags
      19  => Rating
      20  => Authors
      21  => Title     // MP3 歌曲名
      22  => Subject
      23  => Categories
      24  => Comments
      25  => Copyright
      26  => #
      27  => Length    // MP3 时长
      28  => Bit rate
      29  => Protected
      30  => Camera model
      31  => Dimensions
      32  => Camera maker
      33  => Company
      34  => File description
      35  => Program name
      36  => Duration
      37  => Is online
      38  => Is recurring
      39  => Location
      40  => Optional attendee addresses
      41  => Optional attendees
      42  => Organizer address
      43  => Organizer name
      44  => Reminder time
      45  => Required attendee addresses
      46  => Required attendees
      47  => Resources
      48  => Free/busy status
      49  => Total size
      50  => Account name
      51  => Computer
      52  => Anniversary
      53  => Assistant's name
      54  => Assistant's phone
      55  => Birthday
      56  => Business address
      57  => Business city
      58  => Business country/region
      59  => Business P.O. box
      60  => Business postal code
      61  => Business state or province
      62  => Business street
      63  => Business fax
      64  => Business home page
      65  => Business phone
      66  => Callback number
      67  => Car phone
      68  => Children
      69  => Company main phone
      70  => Department
      71  => E-mail Address
      72  => E-mail2
      73  => E-mail3
      74  => E-mail list
      75  => E-mail display name
      76  => File as
      77  => First name
      78  => Full name
      79  => Gender
      80  => Given name
      81  => Hobbies
      82  => Home address
      83  => Home city
      84  => Home country/region
      85  => Home P.O. box
      86  => Home postal code
   * *******************************************************************************/
        private void AddSong(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "选择文件";
            dlg.Filter = "mp3文件|*.mp3";
            dlg.Multiselect = true;
            dlg.FileName = string.Empty;
            dlg.FilterIndex = 1;
            
            if (dlg.ShowDialog() == true)
            {
               int count =  dlg.SafeFileNames.GetLength(0);
               for (int i = 0; i < count; i++)
               {
                    Song song = new Song
                    {
                        FilePath = dlg.FileNames[i]
                    };


                    // 获取歌曲时长
                    ShellClass sh = new ShellClass();
                    Folder dir = sh.NameSpace(System.IO.Path.GetDirectoryName(song.FilePath));
                    FolderItem item = dir.ParseName(System.IO.Path.GetFileName(song.FilePath));
                    string  temp = dir.GetDetailsOf(item, 27);
                    song.TotalTime = temp.Substring(temp.IndexOf(":")+1);


                    string name = dlg.SafeFileNames[i];
                    song.SongName =  name.Substring(0, name.Length - 4 ); // -.mp3
                    song.Number = viewMode.SongList.Count() + 1;

                    viewMode.SongList.Add(song);
               }
                   
            }
        }

        private void SongList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object O = ((ListBox)sender).SelectedItem;
            Song s = O as Song;
             
            Sound.Source = new Uri(s.FilePath);
             
            Sound.Play(); 
        }

  
        private void Sound_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan tTotal = Sound.NaturalDuration.TimeSpan;

            slider_play.Maximum = tTotal.TotalSeconds;
            totaltime.Text = tTotal.ToString("mm\\:ss");



            BtnPause.Visibility = Visibility.Visible;
            BtnPlay.Visibility = Visibility.Hidden;

        }

        private void OnBtnClose(object sender, System.Windows.RoutedEventArgs e)
        {
        	// 在此处添加事件处理程序实现。
            Close();
        }

        private void OnDrag(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void web_Checked(object sender, RoutedEventArgs e)
        {
            if (Web == null) return;

            string url = "http://www2.kugou.kugou.com/yueku/v8/html/home.html";

            Web.Visibility = Visibility.Visible;
            CanvasLyric.Visibility = Visibility.Hidden;

            RadioButton rd =  sender as RadioButton;
            if (rd.Name == "yueku")
            {
                //http://www2.kugou.kugou.com/yueku/v8/html/home.html
                
                url = "http://music.163.com/discover/playlist";

            }
            if (rd.Name == "fm")
            {
                url = "http://www2.kugou.com/fm2/index.html?ver=";

            }
            if (rd.Name == "mv")
            {
                url = "http://www2.kugou.kugou.com/mv/v8/mtv/index/getData.js?cdn=cdn";

            }
            if (rd.Name == "show")
            {
                url = "http://www2.kugou.com/show/html/index.html?ver=7677";

            }
            if (rd.Name == "jiemu")
            {
                url = "http://fanxing.kugou.com/index.php?action=miniIndexNew&id=0&ver=7677";

            }
            if (rd.Name == "lrc")
            {
                Web.Visibility = Visibility.Hidden;
                CanvasLyric.Visibility = Visibility.Visible;

            }


            if (Web != null)
               Web.Navigate(url);

        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ShowChangeSkinDlg(object sender, RoutedEventArgs e)
        {
            Window_Skin dlg = new Window_Skin();

            dlg.changeback += dlg_changeback;
            dlg.ShowDialog();

        }

        void dlg_changeback(string obj)
        {
            if (obj != null)
                Root.Background = new ImageBrush(new BitmapImage(new Uri(obj)));
                              
        }
        
        private void OnBtnPause(object sender, RoutedEventArgs e)
        {
            Sound.Pause();

            BtnPause.Visibility = Visibility.Hidden;
            BtnPlay.Visibility = Visibility.Visible;
        }

        private void OnBtnPlay(object sender, RoutedEventArgs e)
        {
            Sound.Play();

            BtnPause.Visibility = Visibility.Visible;
            BtnPlay.Visibility = Visibility.Hidden;
        }

        private void BtnSearchMusic(object sender, RoutedEventArgs e)
        {
            //MusicSearch.SouSouSearch(SearchContent.Text, 1);

            if (SearchContent.Text.Length <= 0)
                return;
                    
            lycSearch.loadLyic = LoadLyicComplete;   
            lycSearch.SearchLyc(SearchContent.Text);

            lrc.IsChecked = true;
            
        }

       void  LoadLyicComplete(Object obj)
        {
            getLT.getLyricAndLyricTimeByLyricPath(obj.ToString());
            LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词  
        }

       private void SongList_DBClick(object sender, MouseButtonEventArgs e)
       {
           Object O = ((ListBox)sender).SelectedItem;
           Song s = O as Song;


           Sound.Source = new Uri(s.FilePath);

          //ListBoxItem.IsMouseDirectlyOverProperty
           Sound.Play();
       }

        //拖放文件
       private void SongList_Drop(object sender, DragEventArgs e)
       {
           string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

           if (files.Length > 0  &&
                (e.AllowedEffects & DragDropEffects.Copy) == DragDropEffects.Copy)
           {
               e.Effects = DragDropEffects.Copy;
           }
           else
           {
               e.Effects = DragDropEffects.None;
           }

           foreach (string file in files)
           {
               Song song = new Song();
               song.FilePath = file;


               // 获取歌曲时长
               ShellClass sh = new ShellClass();
               Folder dir = sh.NameSpace(System.IO.Path.GetDirectoryName(song.FilePath));
               FolderItem item = dir.ParseName(System.IO.Path.GetFileName(song.FilePath));
               string temp = dir.GetDetailsOf(item, 27);
               song.TotalTime = temp.Substring(temp.IndexOf(":") + 1);


               string name = file;
               int dex = name.LastIndexOf("\\");
               name = name.Substring(dex+1 , name.Length - dex - 1);
               song.SongName = name.Substring(0, name.Length - 4); // -.mp3
               song.Number = viewMode.SongList.Count() + 1;

               viewMode.SongList.Add(song);
           }
       }
            
    }
}
