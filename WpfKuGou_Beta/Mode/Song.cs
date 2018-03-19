using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfKuGou_Beta.Mode
{
     public class Song : INotifyPropertyChanged 
    {
        private string songName; //歌曲名称
        private string filePath; //歌曲文件的完整路径
        private string totalTime;  //总的播放时间
        private string hasPlay;     //已经播放时间
   

        public int Number { get; set; }  //当前歌曲在文件中的序号


        public string SongName 
        {
            get
            {
                return songName;
            }
                
            set
            {
                songName = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SongName"));
                }
            }
        }
        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("FilePath"));
                }
            }
        }

        public string TotalTime
        {
            get
            {
                return totalTime;
            }

            set
            {
                totalTime = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("TotalTime"));
                }
            }
        }

        public string HasPlay
        {
            get
            {
                return hasPlay;
            }

            set
            {
                hasPlay = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("HasPlay"));
                }
            }
        }

       
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
