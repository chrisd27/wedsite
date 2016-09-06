using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wedding.Model
{
    public class Food
    {
        public int FoodId { get; set; }
        public int GuestId { get; set; }
        public int WeddingId { get; set; }
        [MaxLength(100)]
        public string foodCompanyName { get; set; }
        [MaxLength(100)]
        public string foodCompanyContactName { get; set; }
        [MaxLength(100)]
        public string foodCompanyContactEmail { get; set; }
        public int foodCompanyContactNumber { get; set; }
        [MaxLength(100)]
        public string foodMenuStarter { get; set; }
        [MaxLength(100)]
        public string foodMenuMain { get; set; }
        [MaxLength(100)]
        public string foodMenuDesert { get; set; }
        public DateTime foodAvailableFrom { get; set; }
    }
}
