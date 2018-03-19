using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows;

namespace WpfKuGou_Beta
{
    /// <summary>
    /// 在线搜索歌词
    /// </summary>
    class LycSearch
    {
        public delegate void LyicDeleage(object obj);


        public  LyicDeleage loadLyic;
        public  void SearchLyc(string key)
        {
            string strAPI = "http://www.cnlyric.com/search.php?k=";
 
            byte[]  bKeys = Encoding.Default.GetBytes(key);    //关键字编码转换
            foreach (int i  in bKeys)
            {
                strAPI += '%' + i.ToString("X");
            }
            
            strAPI = strAPI + "&t=s" ;  //按歌名搜索
            Uri url = new Uri(strAPI);
            WebClient MyWebClient = new WebClient();
            string Web = MyWebClient.DownloadString(url);

            string lycUrl = "lrc";
            int index = Web.IndexOf(".lrc");
            if (index >= 0)
            {
                for (int i = index; i >= 0; i--)
                {
                    if (Web[i] == '\"')
                    {
                        break;
                    }
                    lycUrl = lycUrl.Insert(0, Web[i].ToString());

                }

            }
            else
                return;

            lycUrl = lycUrl.Insert(0, "http://www.cnlyric.com/");
            DownLoadLyc(lycUrl);

        }

        public  void  DownLoadLyc(string url)
        {
            WebClient MyWebClient = new WebClient();
            string Web = MyWebClient.DownloadString(url);

            if (loadLyic != null)
            {
                loadLyic(Web);
            }
            

        }
    }
}
