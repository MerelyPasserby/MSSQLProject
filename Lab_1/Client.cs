using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic {  get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Client(string name, string surname, string patro, string address, string phone)
        {
            (Name, Surname, Patronymic, Address, Phone) = (name, surname, patro, address, phone);
        }
        public Client(int id, string name, string surname, string patro, string address, string phone) : 
            this(name, surname, patro, address, phone)
        {
            Id = id;
        }

    }
}
