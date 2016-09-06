using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wedding.DAL.Data;
using wedding.Model;

namespace wedding.DAL.Repositories
{
    public class LoginRepository : RepositoryBase<LoginDetails>
    {
        public LoginRepository(DataContext context): base(context)
        {
            if (context == null)
                throw new ArgumentNullException();

        }

    }
}
