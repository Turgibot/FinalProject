﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CSA_Project.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("FullName", this.FirstName+" " +this.LastName));
            return userIdentity;
        }

       
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("final_project", throwIfV1Schema: false)
        {
        }
        //link all models to database here
        public DbSet<SettingsViewModels> Settings { get; set; }
        public DbSet<MainViewerViewModels> Viewer { get; set; }
        public DbSet<AlertsModel> Alerts { get; set; }


        



        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<CSA_Project.Models.SelectorModel> SelectorModels { get; set; }

        public System.Data.Entity.DbSet<CSA_Project.Models.DetectPeople> DetectPeoples { get; set; }

        public System.Data.Entity.DbSet<CSA_Project.Models.LoggerModel> LoggerModels { get; set; }

        public System.Data.Entity.DbSet<CSA_Project.Models.DetectDrowsiness> DetectDrowsinesses { get; set; }

        public System.Data.Entity.DbSet<CSA_Project.Models.DetectPanic> DetectPanics { get; set; }
    }
}