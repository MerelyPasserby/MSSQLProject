using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1
{
    public class Travel
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public int Discount { get; set; }
        public int Cost { get; set; }
        public Travel(int routeId, int clientId, DateTime date, int amount, int discount, int cost)
        {
            (RouteId, ClientId, Date,  Amount, Discount, Cost) = (routeId, clientId, date, amount, discount, cost);
        }
        public Travel(int id, int routeId, int clientId, DateTime date, int amount, int discount, int cost): 
            this(routeId, clientId, date, amount, discount, cost)
        {
            Id = id;
        }

    }
}
