using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Models
{
    public class Transaction
    {
        public int ID { get; set; }

        public virtual Borrower borrowerID { get; set; }



    }
}