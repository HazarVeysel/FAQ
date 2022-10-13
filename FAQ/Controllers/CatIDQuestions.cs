using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAQ.Controllers
{
    public class CatIDQuestions
    {
        public int Question_ID { get; set; }
        public string Question_Title { get; set; }
        public string Question_Category_Name { get; set; }
        public string Question_Category_Img { get; set; }
        public string Question_Details { get; set; }
        public string Answer_Content { get; set; }
        public string Question_Category_Parent_Name { get; set; }
        public int Question_Category_Parent_ID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<DateTime> Create_Date { get; set; }
        public Nullable<int> Question_Category_ID { get; set; }
        public Nullable<int> Category_ID { get; set; }

        public Nullable<int> Reading_Count { get; set; }
    }

}