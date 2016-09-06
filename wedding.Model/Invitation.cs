using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wedding.Model
{
    [Table("Invitation")]
    public class Invitation
    {
        public int InvitationId { get; set; }
        public int WeddingId { get; set; }
        public string username { get; set; }
        public bool RSVP { get; set; }
        public DateTime dateRSVP { get; set; }
        public bool saveTheDateRSVP { get; set; }
        public DateTime dateSaveTheDateRSVP { get; set; }
    }
}
