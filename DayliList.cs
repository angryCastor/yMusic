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
using YandexMusic.Model;

namespace YandexMusic{
    class DayliList{
        private static DayliList instance;
        private int playListId = 0;
        private JObject json;

        private DayliList(){}

        public static DayliList GetInstance()
        {
            if (instance == null)
                instance = new DayliList();
            return instance;
        }

        private async Task<int> GetPlaylistIdFromServer()
        {
            var client = new RestClient("https://music.yandex.ru/handlers/main.jsx");
            client.CookieContainer = Cookie.GetInstance().GetCookieContainer();
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteTaskAsync(request);
            var json = response.Content;

            JObject o = JObject.Parse(json);
            var count = (int?)o.SelectToken("blocks[0].entities[0].data.data.kind") ?? 0;
            return count;
        }

        public async Task<int> GetPlaylistId(){
            if(playListId == 0){
                playListId = await GetPlaylistIdFromServer();
            }
            return playListId;
        }

        public async Task<List<Track>> GetTracks(){
            var jsonObj = await GetJsonPlaylist();
            var jsonTrackList = jsonObj["playlist"]["tracks"] as JArray;
            var trackList = new List<Track>();
            foreach(var track in jsonTrackList){
                trackList.Add(new Track{
                    Id = (int?)track["id"] ?? 0,
                    RealId = (int?)track["realId"] ?? 0,
                    Title = (string)track["title"] ?? "",
                    AutorName = (string)track["artists"][0]["name"] ?? "",
                    StorageDir = (string)track["storageDir"] ?? "",
                    AlbumId = (int?)track["albums"][0]["id"] ?? 0
                });
            }

            return trackList;
        }

        private async Task<JObject> GetJsonPlaylist(){
            if(json == null){
                var client = new RestClient("https://music.yandex.ru/handlers/playlist.jsx?owner=yamusic-daily&kinds=26951281");
                var request = new RestRequest(Method.GET);
                var response = await client.ExecuteTaskAsync(request);
                json = JObject.Parse(response.Content);
            }

            return json;
        }
    }   
}