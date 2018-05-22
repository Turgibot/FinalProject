using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CSA_Project.Models
{
    public class SettingsViewModels
    {

        [Key]
        public long Id { get; set; }
        [DisplayName("Number of people allowed")]
        public int MaxPeopleAllowed { get; set; }
        
        public List<AlertsModel> Alerts { get; set; }

    public SettingsViewModels()
        {
            this.MaxPeopleAllowed = 5;
            this.Alerts = new List<AlertsModel>();
        }
    }
    
    public class AlertsModel
    {
        public long Id { get; set; }
        [Required]
        [DisplayName("Type")]
        public string AlertType { get; set; }
        [Required]
        [DisplayName("Message")]
        public string Message { get; set; }
        [Required]
        [DisplayName("Code")]
        public int Code { get; set; }
    }
}