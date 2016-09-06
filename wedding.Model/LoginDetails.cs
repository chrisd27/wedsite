using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wedding.Model
{
    [Table("LoginDetails")]
    public class LoginDetails
    {
        [Key]
        public int LoginId { get; set; }
        public int GuestId { get; set; }
        public int WeddingId { get; set; }
        public DateTime initialLoginTime { get; set;}
        public string email { get; set; }
        public string username { get; set; }

    }
}