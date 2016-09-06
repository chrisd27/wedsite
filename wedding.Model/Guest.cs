using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wedding.Model
{
    public class Guest
    {
        public int GuestId { get; set; }
        public int WeddingId { get; set; }
        public int InvitationId { get; set; }
        public GuestType guestType { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public bool importantGuest { get; set; }
        public bool RSVP { get; set; }
        public Nullable<DateTime> RSVPTimestampInitial { get; set; }
        public Nullable<DateTime> RSVPTimestamp { get; set; }
        public int dependants { get; set; }
        public string username { get; set; }


    }

    public enum GuestType
    {
        Family = 1,
        Friend = 2
    }
}
