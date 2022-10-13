using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAQ.ViewModel
{
    public class IndexModel
    {
        public List<FAQ.Models.Categories> CategoryList { get; set; }
        public FAQ.Models.Categories Category { get; set; }

        public List<FAQ.Models.Questions> QuestionList { get; set; }
        public FAQ.Models.Questions Question { get; set; }
    }
}