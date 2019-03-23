using System;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using HtmlAgilityPack;
using System.Net.Http;
using System.Collections.Generic; 
using RestSharp;

namespace YandexMusic{
    class YandexAuth{

        private string password = "3i912D65";
        private string login = "astax419";
        private string urlAuth = "https://passport.yandex.ru/auth";
        private string userAgent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.96 Safari/537.36";
        
        public YandexAuth(){
            
        }

    
        public async Task Auth(){
            var client = new RestClient(urlAuth);
            client.CookieContainer = Cookie.GetInstance().GetCookieContainer();
            var request = new RestRequest(Method.POST);
            request.AddHeader("Pragma", "no-cache");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Origin", "https://passport.yandex.ru");
            request.AddHeader("Upgrade-Insecure-Requests", "1");
            request.AddHeader("Host", "passport.yandex.ru");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            request.AddHeader("Referer", "https://passport.yandex.ru/auth");
            request.AddHeader("User-Agent", userAgent);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", $"login={login}&passwd={password}&retpath=&undefined=", ParameterType.RequestBody);
            await client.ExecuteTaskAsync(request);
        }
    }   
}