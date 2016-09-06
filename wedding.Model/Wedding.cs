using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wedding.Model
{
    public class Wedding
    {
        public int WeddingId { get; set; }
        [MaxLength(50)]
        public string weddingNickName { get; set; }
        [MaxLength(50)]
        public string weddingVenueName { get; set; }
        [MaxLength(50)]
        public string weddingVenueAddress1 { get; set; }
        [MaxLength(50)]
        public string weddingVenueAddress2 { get; set; }
        [MaxLength(50)]
        public string weddingVenueTown { get; set; }
        [MaxLength(50)]
        public string weddingVenueCity { get; set; }
        [MaxLength(50)]
        public string weddingVenuePostcode { get; set; }

        [DisplayFormat(DataFormatString = "{0:HH:mm}",
               ApplyFormatInEditMode = true)]
        public DateTime ceremonyTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime weddingDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:HH:mm}",
               ApplyFormatInEditMode = true)]
        public DateTime weddingStart { get; set; }

        [DisplayFormat(DataFormatString = "{0:HH:mm}",
               ApplyFormatInEditMode = true)]
        public DateTime weddingEnd { get; set; }
        [MaxLength(510)]
        public string weddingInformation { get; set; }

        public int getNumberOfGuests()
        {
            int numberOfGuests = 0;

            return numberOfGuests;
        }
    }
}
