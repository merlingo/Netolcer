using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Arayuz
{
    /*
     * 	- aynı proje farklı bir isimle açılır - ->github a farklı bir branch ( git checkout -b|-B <new_branch> [<start point>] ) eklenecek +
	- arayüzde datalogger ekle ve sil butonları çıkarılır -
	- uygulama kurulumunda müsteri bilgileri alınır ve request olarak sunucuya gönderilir 
	- sunucudan gelen id uygulama içine kayıt edilir
	- o id için datalogger bilgileri girilir ve sunucuya gönderilir (*) -> admin panelde de yapılabilir YA DA - id ile datalogger ve sensor bilgileri çekilir ve uygulama içinde xml dosyasına kayıt edilir 
	- her 10 dk da bir uygulama kendi içindeki dlidler için yeni alarm ya da deger var mı diye request kontrol yapar
	- eğer yeni veri varsa request gönderilir ve dönen cevaptaki veriler txt dosyasına kayıt edilir
	/YA DA
     */
    public class HttpHelper
    {
        public static string sendGetRequest(String link)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return content;
        }
        public static string sendPostRequest(String link, byte[] byteArray)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
            request.Method = "POST";
            request.ContentLength = byteArray.Length;
            request.ContentType = "application/json; charset=utf-8";
            Stream dataStream = request.GetRequestStream (); 
            dataStream.Write (byteArray, 0, byteArray.Length);
            dataStream.Close (); 
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return content;
        }
    }
}
