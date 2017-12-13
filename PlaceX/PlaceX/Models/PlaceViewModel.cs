using Common.DLL.Entities.PlaceInfo;
using System;
using System.Collections.Generic;

namespace PlaceX.Models
{
    public class PlaceViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string IconPath { get; set; }
        public string GoogleRating { get; set; }
        public object [] PhotosUrls { get; set; }
        //public IEnumerable<Review> Reviews { get; set; }
    }
}