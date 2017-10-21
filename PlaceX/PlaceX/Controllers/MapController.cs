using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaceX.Controllers
{
    public class MapController : Controller
    {
        //Common positions for out test period 
        //It's gonna be seved in DB in future
        static List<Position> positions;

        public MapController()
        {
            if (positions == null)
                positions = new List<Position>();
        }

        //Show map
        public ActionResult Index()
        {
            return View();
        }

        //Get data from staic field "positions" and
        //send it to UI
        public ActionResult GetGeoJson()
        {
            string serializedData = string.Empty;

            FeatureCollection featureCollection = new FeatureCollection();

            foreach (var pos in positions)
            {
                var point = new Point(pos);
                var featureProperties = new Dictionary<string, object> { { "Name", pos.GetHashCode().ToString() } };
                var model = new Feature(point, featureProperties);
                featureCollection.Features.Add(model);
            }

            serializedData = JsonConvert.SerializeObject(featureCollection, Formatting.Indented,
                      new JsonSerializerSettings
                      {
                          ContractResolver = new CamelCasePropertyNamesContractResolver(),
                          NullValueHandling = NullValueHandling.Ignore
                      });
            return Content(serializedData, "application/json");
        }

        //Get data from UI and
        //save in our list "positions"
        [HttpPost]
        public RedirectToRouteResult GetCoordinatesFromUser(string stringOfCoordinates)
        {
            string[] s = stringOfCoordinates.Replace(',', ' ').Replace('(', ' ').Replace(')', ' ').Split(' ');
            for (int i = 0; i < s.Length - 1;)
            {
                Position newPosition = new Position(s[i + 1], s[i + 3]);
                if (newPosition != null)
                {
                    positions.Add(newPosition);
                }
                i = i + 4;
            }
            return RedirectToAction("Index", "Map", null);
        }
    }
}