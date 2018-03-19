using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using WpfKuGou_Beta.Mode;
using WpfKuGou_Beta.ViewMode;


namespace WpfKuGou_Beta
{
    /// <summary>
    /// Window_Skin.xaml 的交互逻辑
    /// </summary>
    public partial class Window_Skin : Window
    {
        List<string> listBackImages;

        public delegate void changebackground(string obj);

        public event changebackground changeback;
        public Window_Skin()
        {
            InitializeComponent();

            listBackImages = new List<string>();

 

            ShowPic();
        }  
        void ShowPic()
        {
            Skins skin = new Skins();
           

            int count = skin.BackImages.Count();
            if (count == 0) return;

            for (int i = 0; i < count; i++)
            {


                Button btn = new Button
                {
                    Width = 118,
                    Height = 70
                };

                Image img = new Image
                {
                    Stretch = Stretch.Fill
                };


                string path = skin.BackImages[i].SmallImagePath;
                img.Source = new BitmapImage(new Uri( path));


                btn.Content = img;
                btn.Tag = skin.BackImages[i].BigImagePath;
                btn.Click += BackSelect_Click;
                skins.Children.Add(btn);

                 if (i == 0)
                 {
                 
                     ImageBrush br = new ImageBrush(new BitmapImage(new Uri(skin.BackImages[i].BigImagePath)));
                     back.Background = br;

                 }
            }


        }

        void BackSelect_Click(object sender, RoutedEventArgs e)
        {
            Button btn =  sender as Button;
            ImageBrush br = new ImageBrush(new BitmapImage(new Uri(btn.Tag.ToString())));
            back.Background = br;

            changeback(btn.Tag.ToString());
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnHitTest(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

    }
}
