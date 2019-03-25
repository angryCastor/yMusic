using System;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using System.IO;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace YandexMusic
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().GetAwaiter().GetResult();
        }


        static async Task Run(){
            await new YandexAuth().Auth();
            var tracks = await DayliList.GetInstance().GetTracks();
            var trackUrl = new TrackUrl();
            foreach(var t in tracks){
                t.Url = await trackUrl.Get(t);
            }
            (await new DbClient().ClearDailyList())
                .FillColection(tracks);
        }
    }
}
