using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CSA_Project.Models
{
    public class SelectorModel
    {
        [Key]
        public long Id { get; set; }

        [NotMapped]
        public List<string> types = new List<string>()
        {   "People",
            "Drawsiness",
            "Panic"
        };

        [DisplayName("Selected viewing type")]
        public string SelectedValue { get; set; }

    }
}