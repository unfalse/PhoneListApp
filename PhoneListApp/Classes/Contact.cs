using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneListApp.Classes
{
    public class Contact
    {
        public int ContactId;
        public int ContactType;
        public string ContactValue; // [0=phone, 1=email, 2=skype, 3=icq]
    }
}