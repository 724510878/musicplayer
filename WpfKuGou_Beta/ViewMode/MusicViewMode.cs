using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfKuGou_Beta.Mode;


namespace WpfKuGou_Beta.ViewMode
{
    public class MusicViewMode : INotifyPropertyChanged
    {
       public MusicViewMode()
        {
            SongList = new ObservableCollection<Song>() ;
        
        }
        public string SongName { get; set; }
        public ObservableCollection<Song> SongList;


        private string musicUrl;
        public string   MusicUrl 
        {
            get
            {
                return musicUrl;
            }
            set
            {
                musicUrl = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("MusicUrl"));
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
    }
}
