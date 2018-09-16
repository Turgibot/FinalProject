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
            string host = setting.ServerIP;
            string video_src = setting.EuclidIP + ":8080" + setting.Topic;
            string server_url = setting.ServerIP;
            string selected_view = selector.SelectedValue;
            string euclid_ip = setting.EuclidIP;
            string euclid_port = setting.EuclidPort;


            int max_people = setting.MaxPeopleAllowed;
            ViewBag.videoSrc = video_src;
            ViewBag.maxPeople = max_people;
            ViewBag.serverUrl = server_url;
            ViewBag.euclidIP = euclid_ip;
            ViewBag.euclidPort = euclid_port;
            ViewBag.selectedView = selected_view;
            return View();
        }
    }
}
