using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using System.IO;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic; 
using YandexMusic.Model;
using System;

namespace YandexMusic
{
    class DbClient{
        private IMongoDatabase database;
        private MongoClient client;

        private IConfigurationRoot config;

        private IMongoCollection<Track> collection;
        private string nameCollection = "dailylist";
        public DbClient(){
            config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
 
            client = new MongoClient(config["conectionString"]);
            database = client.GetDatabase(config["dbName"]);
            collection = database.GetCollection<Track>(nameCollection);
        }


        public async Task<DbClient> ClearDailyList()
        {
            await collection.DeleteManyAsync((e) => true);
            return this;
        }

        public void FillColection(List<Track> tracks)
        {
            collection.InsertManyAsync(tracks);
        }
    }
}