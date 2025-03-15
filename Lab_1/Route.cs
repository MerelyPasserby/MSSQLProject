using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1
{
    public class Route
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Climate { get; set; }
        public int Duration { get; set; }
        public string Hotel { get; set; }
        public int Money { get; set; }
        public Route(string country, string climate, int duration, string hotel, int money)
        {
            (Country, Climate, Duration, Hotel, Money) = (country, climate, duration, hotel, money);
        }
        public Route(int id, string country, string climate, int duration, string hotel, int money) : 
            this(country, climate, duration, hotel, money)
        {
            Id = id;
        }

    }
}
