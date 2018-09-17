using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSA_Project.Models
{
    public class DetectDrowsiness
    {
        [Key]
        public long Id { get; set; }

        [DisplayName("Number of people Detected")]
        public bool IsAwake{ get; set; }

    }
}