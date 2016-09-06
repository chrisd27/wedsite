using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wedding.Contracts.Modules;
using wedding.Model;

namespace wedding.Modules.Guests.RSVP.Yes
{
    class RSVP : IRSVP
    {
        public void processRSVP(Guest guest, Invitation invitation, Wedding wedding)
        {
            // Process Yes RSVP
        }
    }
}
