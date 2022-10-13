using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAQ.ViewModel
{
    public class categoryListViewModel
    {

        public List<FAQ.Models.Categories> CategoryList { get; set; }
        public FAQ.Models.Categories Category { get; set; }

    }

   
}