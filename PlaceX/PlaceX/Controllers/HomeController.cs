using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.DLL;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Net;
using PlaceX.Models;

namespace PlaceX.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PlaceInfo(string placeId)
        {
            string placeInfo = GetPlaceJson("https://maps.googleapis.com/maps/api/place/details/json?placeid=" + placeId + "&key=AIzaSyArQis35LbcmrOvvVlAJtsHABL7ObLubk8&language=ru");

            dynamic foo = JObject.Parse(placeInfo);

            try
            {
                PlaceViewModel targetPlace = new PlaceViewModel()
                {
                    Id = placeId,
                    Name = foo.result.name,
                    Address = foo.result.formatted_address,
                    PhoneNumber = foo.result.international_phone_number,
                    IconPath = foo.result.icon,
                    GoogleRating = foo.result.rating
                };

                return View(targetPlace);
            }
            catch(Exception ex)
            {
                return View("Index");
            }           
        }

        private string GetPlaceJson(string url)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;

                var json = wc.DownloadString(url);
                return json;
            }
        }
    }
}