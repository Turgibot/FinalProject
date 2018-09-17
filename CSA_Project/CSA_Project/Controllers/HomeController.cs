using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CSA_Project.Models;

namespace CSA_Project.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext DB = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            var setting = DB.Settings.FirstOrDefault();
            var selector = DB.SelectorModels.FirstOrDefault();
            //general settings
            string http = "http://";
            string streamPort = ":8080";
            string host = setting.ServerIP;
            string camera_src = http + setting.EuclidIP + streamPort + setting.CameraTopic;
            string server_url = setting.ServerIP;
            string selected_view = selector.SelectedValue;
            string euclid_ip = setting.EuclidIP;
            string euclid_port = setting.EuclidPort;

            //detection settings

            string people_src = http + setting.EuclidIP + streamPort + setting.PeopleTopic;
            string drowsiness_src = http + setting.EuclidIP + streamPort + setting.DrowsinessTopic;
            string panic_src = http + setting.EuclidIP + streamPort + setting.PanicTopic;
            
            int max_people = setting.MaxPeopleAllowed;
            ViewBag.cameraSrc = camera_src;
            ViewBag.PeopleSrc = people_src;
            ViewBag.DrowsinessSrc = drowsiness_src;
            ViewBag.PanicSrc = panic_src;
            ViewBag.maxPeople = max_people;
            ViewBag.serverUrl = server_url;
            ViewBag.euclidIP = euclid_ip;
            ViewBag.euclidPort = euclid_port;
            ViewBag.selectedView = selected_view;
            return View();
        }
    }
}
