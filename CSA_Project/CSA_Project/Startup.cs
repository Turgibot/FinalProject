using System;
using System.Collections.Generic;
using System.Linq;
using CSA_Project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CSA_Project.Startup))]

namespace CSA_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsersAsync();
        }

        private async void CreateRolesandUsersAsync()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                //var user = new ApplicationUser();
                //user.UserName = "Guy";
                //user.Email = "turgibot@gmail.com";
                //user.EmailConfirmed = true;
                //user.FirstName = "Guy";
                //user.LastName = "Tordjman";
                //user.PhoneNumber = "0537203788";

                //string userPWD = "Q@w3e4";

                //var chkUser = UserManager.Create(user, userPWD);

                ////Add default User to Role Admin   
                //if (chkUser.Succeeded)
                //{
                //    var result1 = UserManager.AddToRole(user.Id, "Admin");

                //}
            }

            // creating Creating Employee role    
            if (!roleManager.RoleExists("Viewer"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Viewer";
                roleManager.Create(role);

            }
            //create a default alert
            if (context.Alerts.Count() == 0)
            {
                var alert = new AlertsModel();
                alert.AlertType = "OK";
                alert.Code = 200;
                alert.Message = "OK";
                var setting = new SettingsViewModels();
                setting.Alerts.Add(alert);
                context.Settings.Add(setting);
                await context.SaveChangesAsync();

            }
        }

    }
}
