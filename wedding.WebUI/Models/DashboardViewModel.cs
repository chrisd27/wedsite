using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wedding.Model;

namespace wedding.WebUI.Models
{
    public class DashboardViewModel
    {
        public int weddingId { get; set; }
        public Wedding wedding { get; set; }
        public Guest guests { get; set; }
        public Accommodation accommodation { get; set; }
        public Invitation invitation { get; set; }
        public Food food { get; set; }
    }
}