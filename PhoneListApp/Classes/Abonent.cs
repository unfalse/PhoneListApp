using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneListApp.Classes
{
    public class Abonent
    {
        public int id { get; set; }
        public string FIO { get; set; }
        public DateTime Birthday_date { get; set; }
        public string Passport_series { get; set; }
        public string INN { get; set; }
        public string Work { get; set; }
        public int Education { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string Sex { get; set; }
        public List<Contact> Contacts { get; set; }
    }

    public class SearchAbonent : Abonent
    {
        public string UnionOperation;
    }
}