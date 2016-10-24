using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneListApp.Classes
{
    public class Abonent
    {
        public int id;
        public string FIO;
        public DateTime Birthday_date;
        public string Passport_series;
        public string INN;
        public string Work;
        public int Education;
        public string Address;
        public string Photo;
        public string Sex;
        public List<Contact> Contacts;
    }

    public class SearchAbonent : Abonent
    {
        public string UnionOperation;
    }
}