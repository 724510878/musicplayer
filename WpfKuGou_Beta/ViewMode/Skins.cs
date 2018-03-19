using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

using WpfKuGou_Beta.Mode;



namespace WpfKuGou_Beta.ViewMode
{
    class Skins : INotifyPropertyChanged
    {
        public Skins()
        {
            backImages = new List<BackImage>();

            string sPath = AppDomain.CurrentDomain.BaseDirectory;
            sPath += "BackGround\\";

            DirectoryInfo TheFolder = new DirectoryInfo(sPath);


            foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
            {
              
                string describe = "";

                if ("35006795ad19d36d50c0b0e87f8ad682" == NextFolder.Name)
                {
                    describe = "妳好射手座";
                }
                else if ("8a305f31191282ca211e72c008edbb6b" == NextFolder.Name)
                {
                    describe = "隽永七夕";
                }
                else if ("4e25cd23c097c72566421beb2dbde3cf" == NextFolder.Name)
                {
                    describe = "土豪金";
                }
                else if ("fa026ab563e71cd9c42973c8a97b9bf6" == NextFolder.Name)
                {
                    describe = "妳好狮子座";
                }
                else if ("cbebf0902554be4a6a3f3eaed05ec44d" == NextFolder.Name)
                {
                    describe = "你好处女座";
                }
                else if ("db7b297b798eb196de4b7be155c2e7ea" == NextFolder.Name)
                {
                    describe = "妳好天秤座";
                }
                else if ("6b261d7055272b4de8fdd55412250af8" == NextFolder.Name)
                {
                    describe = "你好摩羯座";
                }
                else if ("131e6bdabfc9be39c67efe7d84846699" == NextFolder.Name)
                {
                    describe = "经典皮肤";
                }
                else if ("5cdf715b6736e851f78c86174ff0d839" == NextFolder.Name)
                {
                    describe = "经典酷灰";
                }
                else if ("166b36c6c06e895f3dfc9e471c36050b" == NextFolder.Name)
                {
                    describe = "绿色爵士";
                }
                else if ("eaf9e4323bb741f3e2a1141468dca8c0" == NextFolder.Name)
                {
                    describe = "工业时代";
                }
                else if ("6c34dff5f732331864cdd3ca833cf6d7" == NextFolder.Name)
                {
                    describe = "红粉菲菲";
                }
                else if ("a603a6e27c34c2e078a3f59dffb252ca" == NextFolder.Name)
                {
                    describe = "星语心愿";
                }

                BackImage bkimg = new BackImage();
                bkimg.ImageDescribe = describe;
                bkimg.SmallImagePath = sPath + NextFolder.Name + "\\Thumbnail.jpg";
                bkimg.BigImagePath = sPath + NextFolder.Name + "\\back.png";

                backImages.Add(bkimg);

            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private ImageSource selectBack;
        public ImageSource SelectBack 
        {
            get
            {
                return selectBack;
            }
            set
            {
                selectBack = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectBack"));
                }
            }
        }


        private List<BackImage> backImages;

        public List<BackImage> BackImages 
        { 
            get
            {
                return backImages;
            }
           
        }


    }
}
