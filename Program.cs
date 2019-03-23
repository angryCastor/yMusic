using System;
using System.Net;

namespace YandexMusic
{
    class Program
    {
        static void Main(string[] args)
        {
            new YandexAuth().Auth().GetAwaiter().GetResult();
            var tracks = DayliList.GetInstance().GetTracks().GetAwaiter().GetResult();
            var trackUrl = new TrackUrl();
            foreach(var t in tracks){
                Console.WriteLine(trackUrl.Get(t).GetAwaiter().GetResult());
            }
            Console.WriteLine("Hello World!");
        }

    }
}
