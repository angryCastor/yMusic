using System;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using HtmlAgilityPack;
using System.Net.Http;
using System.Collections.Generic; 

namespace YandexMusic{
    class YandexAuth{

        private string password = "3i912D65";
        private string login = "astax419";
        private string urlAuth = "https://passport.yandex.ru/auth";
        private string userAgent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.96 Safari/537.36";
        private CookieContainer cookieContainer;
        private HttpClientHandler handler;
        
        public YandexAuth()
        {
            cookieContainer = new CookieContainer();
            handler = new HttpClientHandler();
            handler.AllowAutoRedirect = false;
            handler.CookieContainer = cookieContainer;
        }


        private async Task<string> GetCsrfToken()
        {
            var client = new HttpClient(handler);

            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            client.DefaultRequestHeaders.Add("Pragma", "no-cache");
            client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            // client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("Host", "passport.yandex.ru");
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);

            var result = await client.GetAsync(urlAuth);
            var html = await result.Content.ReadAsStringAsync();

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var csrfToken = htmlDoc.DocumentNode
                .SelectSingleNode("//input[@name='csrf_token']")
                .Attributes["value"].Value;

            
            return csrfToken;
        }


        private async Task OpenOldForm(){
            var client = new HttpClient(handler);

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("retpath", ""),
                new KeyValuePair<string, string>("fretpath", ""),
                new KeyValuePair<string, string>("clean", ""),
                new KeyValuePair<string, string>("service", ""),
                new KeyValuePair<string, string>("origin", ""),
                new KeyValuePair<string, string>("policy", ""),
                new KeyValuePair<string, string>("is_pdd", ""),
                new KeyValuePair<string, string>("csrf_token", await GetCsrfToken()),
                new KeyValuePair<string, string>("login", login),
                new KeyValuePair<string, string>("hidden-password", password),
            });
            
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            client.DefaultRequestHeaders.Add("Pragma", "no-cache");
            client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            client.DefaultRequestHeaders.Add("Origin", "https://passport.yandex.ru");
            client.DefaultRequestHeaders.Add("Referer", "https://passport.yandex.ru/auth");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            // client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("Host", "passport.yandex.ru");
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);

            var result = await client.PostAsync(urlAuth, content);

            content.Dispose();
        }

        private async Task TryAuth(){

            var client = new HttpClient(handler);

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("retpath", ""),
                new KeyValuePair<string, string>("login", login),
                new KeyValuePair<string, string>("password", password),
            });
            
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            client.DefaultRequestHeaders.Add("Pragma", "no-cache");
            client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            client.DefaultRequestHeaders.Add("Host", "passport.yandex.ru");
            client.DefaultRequestHeaders.Add("Origin", "https://passport.yandex.ru");
            client.DefaultRequestHeaders.Add("Referer", "https://passport.yandex.ru/auth");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            // client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);

            var result = await client.PostAsync(urlAuth, content);
            var html = await result.Content.ReadAsStringAsync();
            content.Dispose();
            int i = 0;
        }


        public async Task Auth(){
            await OpenOldForm();
            await TryAuth();
        }
    }   
}