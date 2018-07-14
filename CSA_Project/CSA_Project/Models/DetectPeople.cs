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
    public class DetectPeople
    {
        [Key]
        public long Id { get; set; }

        [NotMapped]
        public List<string> BoxesValue { get; set; }

        [AllowHtml]
        public string ValuesString
        {
            get
            {
                if (BoxesValue == null)
                    return "";
                return string.Join("%%%", BoxesValue);
            }
            set
            {
                if (value == null || value == "")
                    BoxesValue = new List<string>();
                else
                    BoxesValue = value.Split(new string[] { "%%%" }, StringSplitOptions.None).ToList();
            }
        }


        [DisplayName("Number of people Detected")]
        public int NumberOfPeople { get; set; }

    }

}