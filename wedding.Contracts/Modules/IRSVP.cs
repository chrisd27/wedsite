using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wedding.Model;

namespace wedding.Contracts.Modules
{
    public interface IRSVP
    {
        void processRSVP(Invitation invitation, Wedding wedding);
    }
}
