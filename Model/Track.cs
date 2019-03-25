using System;


namespace YandexMusic.Model{
    class Track{
        public int Id { get; set; } = 0;
        public int RealId { get; set; } = 0;
        public string AutorName { get; set; } = "";
        public string Title { get; set; } = "";
        public string StorageDir { get; set; } = "";
        public int AlbumId { get; set; } = 0;
        public string Url { get; set; } = "";
    }
}