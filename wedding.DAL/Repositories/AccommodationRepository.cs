using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wedding.DAL.Data;
using wedding.Model;

namespace wedding.DAL.Repositories
{
    public class AccommodationRepository : RepositoryBase<Accommodation>
    {
        public AccommodationRepository(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();

        }

    }
}
