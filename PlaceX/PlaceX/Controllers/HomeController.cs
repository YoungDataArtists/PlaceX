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
using Common.DLL.Entities.PlaceInfo;

namespace PlaceX.Controllers
{
    public class HomeController : Controller
    {
        PlaceInfoContext placeInfoDb = new PlaceInfoContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult PlaceInfo(string placeId)
        {
            string placeInfo = GetPlaceJson("https://maps.googleapis.com/maps/api/place/details/json?placeid=" + placeId + "&key=AIzaSyDAVUjsKnnRMjs32MZxlWvpAu-tQeQKMpY&language=ru");

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
                    GoogleRating = foo.result.rating,
                    PhotosUrls = new object[foo.result.photos.Count],
                    //Reviews = (placeInfoDb.Reviews.Where(r => r.GooglePlaceId == placeId).Count() > 0) ? placeInfoDb.Reviews.Where(r => r.GooglePlaceId == placeId).ToList() : new List<Review>()
                };
                for (int i = 0; i < foo.result.photos.Count; i++)
                {
                    targetPlace.PhotosUrls[i] = (foo.result.photos != null) ? "https://maps.googleapis.com/maps/api/place/photo?maxwidth=300&photoreference=" + foo.result.photos[i].photo_reference + "&key=AIzaSyDAVUjsKnnRMjs32MZxlWvpAu-tQeQKMpY" : foo.result.icon;
                }

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

        [HttpPost]
        public ActionResult AddReview(string textContent, string author, string googlePlaceId, int rating)
        {
            Review newReview = new Review()
            {
                TextContent = textContent,
                Author = author,
                GooglePlaceId = googlePlaceId,
                Rating = rating,
                Usefulness = 0,
                PhotoUrl = String.Empty,
                Date = DateTime.Now
            };

            placeInfoDb.Reviews.Add(newReview);
            placeInfoDb.SaveChanges();

            return RedirectToAction("ShowReviews", new { googlePlaceId = googlePlaceId , sortedByDateDesc = true});
        }

        [AllowAnonymous]
        public ActionResult ShowReviews(string googlePlaceId, bool? sortedByDateAds, bool? sortedByDateDesc, bool? sortedByRatingMaxMin, bool? sortedByRatingMinMax)
        {
            List<Review> reviews;

            if (placeInfoDb.Reviews.Where(r => r.GooglePlaceId == googlePlaceId).Count() > 0)
            {
                if (sortedByDateAds == true)
                {
                    reviews = placeInfoDb.Reviews.Where(r => r.GooglePlaceId == googlePlaceId).OrderBy(p => p.Date).ToList();
                }
                else if (sortedByDateDesc == true)
                {
                    reviews = placeInfoDb.Reviews.Where(r => r.GooglePlaceId == googlePlaceId).OrderByDescending(p => p.Date).ToList();
                }
                else if (sortedByRatingMaxMin == true)
                {
                    reviews = placeInfoDb.Reviews.Where(r => r.GooglePlaceId == googlePlaceId).OrderByDescending(p => p.Rating).ToList();
                }
                else if (sortedByRatingMinMax == true)
                {
                    reviews = placeInfoDb.Reviews.Where(r => r.GooglePlaceId == googlePlaceId).OrderBy(p => p.Rating).ToList();
                }
                else
                {
                    reviews = placeInfoDb.Reviews.Where(r => r.GooglePlaceId == googlePlaceId).ToList();
                }
            }
            else
            {
                reviews = new List<Review>();
            }            

            return PartialView(reviews);
        }
    }
}