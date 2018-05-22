using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CSA_Project.Models
{
    
    public class MainViewerViewModels
    {
        public long Id { get; set; }
        public int InferenceResult { get; set; }
        public int MaxPeopleAllowed { get; set; }
    }
    
}