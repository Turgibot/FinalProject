﻿using System;
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

        [DisplayName("Euclid IP")]
        public string EuclidIP { get; set; }

        [DisplayName("Euclid MAC")]
        public string EuclidMAC { get; set; }

        [DisplayName("Euclid HTTP Server Port number")]
        public string EuclidPort { get; set; }

        [DisplayName("Euclid Camera Topic")]
        public string CameraTopic { get; set; }

        [DisplayName("Euclid People Detection Topic")]
        public string PeopleTopic { get; set; }

        [DisplayName("Euclid Drowsiness Detection Topic")]
        public string DrowsinessTopic { get; set; }

        [DisplayName("Euclid Panic Detection Topic")]
        public string PanicTopic { get; set; }

        [DisplayName("Server IP")]
        public string ServerIP { get; set; }

        [DisplayName("Server MAC")]
        public string ServerMAC { get; set; }

        [DisplayName("Server Port number")]
        public string ServerPort { get; set; }
        
        [DisplayName("Db Name")]
        public string DB_Name { get; set; }
        [DisplayName("DB Connection String")]
        public string ConnectionString { get; set; }
        
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