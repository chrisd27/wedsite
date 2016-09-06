using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wedding.Model;

namespace wedding.DAL.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }
        public DbSet<Accommodation> Accommodation { get; set; }
        public DbSet<Guest> Guest { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Invitation> Invitation { get; set; }
        public DbSet<Wedding> Weddings { get; set; }
        public DbSet<LoginDetails> LoginDetails { get; set; }
    }
}
