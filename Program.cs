using System;
using System.Net;

namespace YandexMusic
{
    class Program
    {
        static void Main(string[] args)
        {
            new YandexAuth().Auth().GetAwaiter().GetResult();
            Console.WriteLine("Hello World!");
        }

    }
}
