using System;
using System.Net;
using System.Web;
using System.Threading.Tasks;
using System.IO;
using HtmlAgilityPack;
using System.Collections.Generic; 
using System.Text;
using RestSharp;
using Newtonsoft.Json.Linq;

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
    }
}