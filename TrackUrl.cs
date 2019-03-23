using System;
using YandexMusic.Model;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;

namespace YandexMusic{
    class TrackUrl{
        private string hashSalt = "XGRlBW9FXlekgbPrRHuSiA";
        public TrackUrl(){}


        public async Task<string> Get(Track track){
            return await Get(track.Id, track.AlbumId);
        }

        public async Task<string> Get(int trackId, int albumId){
            var stringTrack = $"{trackId}:{albumId}";
            var srcData = await GetUrlData(stringTrack);
            var url = await GetUrl(srcData);
            return url;
        }


        private async Task<string> GetUrlData(string partTrack){
            var client = new RestClient($"https://music.yandex.ru/api/v2.1/handlers/track/{partTrack}/web-auto_playlists-playlist_of_the_day-track-main/download/m?hq=0&external-domain=music.yandex.ru&overembed=no");
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-Retpath-Y", "https://music.yandex.ru/");
            var response = await client.ExecuteTaskAsync(request);
            var json = JObject.Parse(response.Content);
            var src = (string)json["src"];

            return src;
        }


        private async Task<string> GetUrl(string src){
            var client = new RestClient(src + "&format=json");
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteTaskAsync(request);
            var json = JObject.Parse(response.Content);
            var url = GenerateUrl(json);

            return url;
        }

        private string GenerateUrl(JObject json){
            var host = (string)json["host"];
            var s = (string)json["s"];
            var ts = (string)json["ts"];
            var path = (string)json["path"];
            var hash = GetHash($"{hashSalt}{path.TrimStart('/')}{s}");

            var url = $"https://{host}/get-mp3/{hash}/{ts}{path}";

            return url;
        }

        
        public string GetHash(string input)
        {
            var result = "";

            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                result = sb.ToString();
            }
            return result;
        }
    }
}
