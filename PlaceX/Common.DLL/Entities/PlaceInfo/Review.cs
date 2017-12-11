using System;
using System.Data.Entity;

namespace Common.DLL.Entities.PlaceInfo
{
    public class Review
    {
        public int Id { get; set; }
        public string TextContent { get; set; }
        public string Author { get; set; }
        public int Usefulness { get; set; }
        public int? Rating { get; set; }
        public string GooglePlaceId { get; set; }
        public DateTime Date { get; set; }
        public string PhotoUrl { get; set; }
    }
}
