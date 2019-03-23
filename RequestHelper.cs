using System;
using System.Net;
using System.Web;
using System.Threading.Tasks;
using System.IO;
using HtmlAgilityPack;
using System.Collections.Generic; 
using System.Text;

namespace YandexMusic{
    class RequestHelper{
        private static RequestHelper instance;

        private RequestHelper(){}

        public static RequestHelper GetInstance()
        {
            if (instance == null)
                instance = new RequestHelper();
            return instance;
        }

        public byte[] PostData(Dictionary<string,string> postParameters){
            string postData = "";

            foreach (string key in postParameters.Keys)
            {
                postData += HttpUtility.UrlEncode(key) + "="
                    + HttpUtility.UrlEncode(postParameters[key]) + "&";
            }
            byte[] data = Encoding.ASCII.GetBytes(postData);
            return data;
        }

        public Task<string> GetResponseText(HttpWebResponse response){
            using(var streamResponse = response.GetResponseStream())
            {
                using (var sr = new StreamReader(streamResponse))
                {
                    return sr.ReadToEndAsync();
                }
            }
        }
    }
}