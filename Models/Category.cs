using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Models
{
    public class Category
    {
        /**
         *  So each genre of books have sub-genre...
         *  This means each book can belong into 1 genre and each genre can have more than 1 book.
         */

        public int ID { get; set; }

        public String GenreName { get; set; }

        public int ParentID { get; set; } /*linking to its parent genre*/

        public String Initials { get; set; } /*short referencing letter for the genre*/

        public String Full_Listing_Name { get; set; }



        
    }
}