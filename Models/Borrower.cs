using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Models
{
    public class Borrower
    {
        public int ID { get; set; }

        public String firstName { get; set; }
        public String address { get; set; }
        public int homePhone { get; set; }
        public int mobilePhone { get; set; }


    }
}