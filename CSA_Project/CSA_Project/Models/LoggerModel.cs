using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CSA_Project.Models
{
    public class LoggerModel
    {
    /*
    200 OK
    204 No Content
    400 Bad Request
    401 Unauthorized
    500 Internal Server Error
    501 Not Implemented
    502 Bad Gateway
    503 Service Unavailable
    600 All Good
    601 OverPopulation Alert
    */
        [Key]
        public long Id { get; set; }
       
        [DisplayName("Alert time")]
        public DateTime DateTime {
            get; set; }

        [DisplayName("Remarks")]
        public String Message { get; set; }

        [DisplayName("Code")]
        public int Code { get; set; }

        [DisplayName("Parameter")]
        public String Key { get; set; }

        [DisplayName("Value")]
        public String Value { get; set; }

        [DisplayName("User Email")]
        public String Email { get; set; }
    }
}