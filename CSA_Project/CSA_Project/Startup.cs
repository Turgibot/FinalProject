using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSA_Project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Owin;

[assembly: OwinStartup(typeof(CSA_Project.Startup))]

namespace CSA_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            AppInisialSetupAsync();
        }

        private async void AppInisialSetupAsync()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //setting up the app
            if (context.Settings.Count() == 0)
            {
                var settings = new SettingsViewModels();
                settings.MaxPeopleAllowed = 3;
                string json;
                using (StreamReader r = new StreamReader(@"D:\Projects\FinalProject\CSA_Project\CSA_Project\CSA_Config\config.json"))
                {
                    json = r.ReadToEnd();
                }
                JObject jObject = JObject.Parse(json);
                JToken euclid = jObject["euclid"];
                JToken server = jObject["server"];
                JToken net = jObject["neural_net"];
                JToken db = jObject["db_name"];
                JToken connection = jObject["connection_string"];
                settings.DB_Name = (string)db;
                settings.ConnectionString = (string)connection;
                settings.EuclidIP = (string)euclid["ip"];
                settings.EuclidMAC = (string)euclid["mac"];
                settings.EuclidPort = (string)euclid["stream_port"];
                settings.Topic = (string)euclid["cam_topic"];

                settings.ServerIP = (string)server["ip"];
                settings.ServerMAC = (string)server["mac"];
                settings.ServerPort = (string)euclid["stream_port"];
                settings.RecordingPath = (string)euclid["video_recording_path"];

                settings.NN_Model = (string)net["model"];
                settings.NN_Weights = (string)net["weights"];

                context.Settings.Add(settings);
                await context.SaveChangesAsync();
            }

            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

              
            }

            // creating Creating Employee role    
            if (!roleManager.RoleExists("Viewer"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Viewer";
                roleManager.Create(role);

            }
            if(context.SelectorModels.Count() == 0)
            {
                var selector = new SelectorModel();
                selector.SelectedValue = "People";
                context.SelectorModels.Add(selector);
                await context.SaveChangesAsync();
            }
            
            //create a default alert
            if (context.Alerts.Count() == 0)
            {
                var alert = new AlertsModel();
                alert.AlertType = "OK";
                alert.Code = 200;
                alert.Message = "OK";
                context.Alerts.Add(alert);
                await context.SaveChangesAsync();

            }
        }

    }
}
