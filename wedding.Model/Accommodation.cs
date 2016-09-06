using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wedding.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wedding.Model
{
    [Table("Accommodation")]
    public class Accommodation
    {
        public int AccommodationId { get; set; }
        public int WeddingId { get; set; }
        [MaxLength(100)]
        public string accommodationName { get; set; }
        [MaxLength(100)]
        public string accommodationWebsite { get; set; }
        public string accommodationPostcode { get; set; }
        public string accommodationCity { get; set; }
    }
}
