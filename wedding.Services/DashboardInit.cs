using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wedding.Model;

namespace wedding.Services
{
    public class DashboardInit
    {
        int numGuests { get; set; }
        int numFood { get; set; }
        int numAccom { get; set; }

        public int countGuests(int weddingId, IEnumerable<Guest> guests)
        {
            var numGuests = 0;
            foreach (Guest guest in guests)
            {
                if (guest.WeddingId == weddingId)
                {
                    numGuests++;
                }
            }
            return numGuests;
        }

        public int countFood(int weddingId, IEnumerable<Food> food)
        {
            var numFood = 0;
            foreach (Food item in food)
            {
                if (item.WeddingId == weddingId)
                {
                    numFood++;
                }
            }
            return numFood;
        }

        public int countAccom(int weddingId, IEnumerable<Accommodation> accom)
        {
            var numAccom = 0;
            foreach (Accommodation item in accom)
            {
                if (item.WeddingId == weddingId)
                {
                    numAccom++;
                }
            }
            return numAccom;
        }

        
    }
}
