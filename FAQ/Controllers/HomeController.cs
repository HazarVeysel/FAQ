using FAQ.Entities;
using FAQ.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FAQ.Controllers
{
    public class HomeController : Controller
    {
        public FAQ_DBEntities Repository { get; set; } = new FAQ_DBEntities();
        public HomeController()
        {
            ViewBag.Repository = Repository;

        }


        HelpCenter help = new HelpCenter();
        public ActionResult Index()
        {
            FAQ.ViewModel.IndexModel indexModel = new ViewModel.IndexModel();
            indexModel.Category = new Categories();
            indexModel.CategoryList = help.ListAllCategory();
            indexModel.Question = new Questions();
            indexModel.QuestionList = help.ListAllQuestion();
            return View(indexModel);
            //return RedirectToAction("AdminCategories");
        }
        
        public ActionResult AdminCategories()
        {
            List<SelectListItem> valuecategory = (from x in help.ListAllCategory()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Category_Name,
                                                      Value = x.Category_ID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;

            FAQ.ViewModel.categoryListViewModel ttt = new ViewModel.categoryListViewModel();
            ttt.CategoryList = help.ListAllCategory();
            ttt.Category = new Categories();
            return View(ttt);
        }
        public ActionResult AdminQuestions()
        {
            List<QuestionCategory> Listeleme =
               (from question in help.ListAllQuestion()
                join category in help.ListAllCategory() on question.Category_ID equals category.Category_ID
                select new QuestionCategory
                {
                    Question_ID = question.Question_ID,
                    Question_Category_Name = category.Category_Name,
                    Question_Title = question.Question_Title,
                    Question_Details = question.Question_Details,
                    Answer_Content = question.Answer_Content,
                    Category_ID = question.Category_ID,
                    Create_Date = question.Create_Date,
                    IsActive = question.IsActive,
                    Reading_Count = question.Reading_Count
                }).ToList();
            return View(Listeleme);
        }

        [HttpGet]
        public ActionResult AddNewQuestion()
        {
            List<SelectListItem> valuecategory = (from x in help.ListAllCategory()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Category_Name,
                                                      Value = x.Category_ID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNewQuestion(Questions question)
        {
            List<SelectListItem> valuecategory = (from x in help.ListAllCategory()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Category_Name,
                                                      Value = x.Category_ID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;

            question.IsActive = true;
            question.Create_Date = DateTime.Parse(DateTime.Now.ToString());
            question.Reading_Count = Convert.ToInt32("0");
            //question.Category_ID = 1;
            help.AddQuestion(question);
            return RedirectToAction("AdminQuestions");
        }
        //public ActionResult PassivateQuestion(int id)
        //{

        //    help.DeleteQuestion(id);

        //    return RedirectToAction("AdminQuestions");
        //}
        //public ActionResult ActivateQuestion(int id)
        //{
        //    var questionValue = help.GetQuestion(id);
        //    help.UpdateQuestion(questionValue);

        //    return RedirectToAction("AdminQuestions");
        //}

        public JsonResult ActivePassiveQuestion(int ID)
        {


            bool QuestionValue = help.UpdateQuestionWithID(ID);



            return Json(QuestionValue, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditQuestion(int id)
        {
            var questionValue = help.GetQuestion(id);
            List<SelectListItem> valuecategory = (from x in help.ListAllCategory()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Category_Name,
                                                      Value = x.Category_ID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;

            return View(questionValue);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditQuestion(Questions question)
        {
            var questionValue = help.GetQuestion(question.Question_ID);
            List<SelectListItem> valuecategory = (from x in help.ListAllCategory()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Category_Name,
                                                      Value = x.Category_ID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;

            help.UpdateQuestion(question);

            return RedirectToAction("AdminQuestions");
        }
        public ActionResult Index1()
        {
            return View();
        }
        //------------------------------------------------Categories--------------------------------------------------------------------
        [HttpGet]
        public ActionResult AddNewCategory()
        {
            List<SelectListItem> valuecategory = (from x in help.ListAllCategory()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Category_Name,
                                                      Value = x.Category_ID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNewCategory(Categories category, HttpPostedFileBase Img_Url, string Category_Name, string Category_Description)
        {
            List<SelectListItem> valuecategory = (from x in help.ListAllCategory()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Category_Name,
                                                      Value = x.Category_ID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;

            if (Img_Url != null)
            {
                string filename = Path.GetFileName(Img_Url.FileName);
                string extension = Path.GetExtension(Img_Url.FileName);
                string filepath = "/CategoryImage/" + filename;
                Img_Url.SaveAs(Server.MapPath(filepath));
                category.Img_Url = "/CategoryImage/" + filename;
            }
            category.Category_Name = Category_Name;
            category.Category_Name = Category_Description;
            category.IsActive = true;
            category.Create_Date = DateTime.Parse(DateTime.Now.ToString());

            //question.Category_ID = 1;
            help.AddCategory(category);
            return RedirectToAction("AdminCategories");
        }
        //public ActionResult PassivateCategory(int id)
        //{

        //    help.DeleteCategory(id);

        //    return RedirectToAction("AdminCategories");
        //}
        //public ActionResult ActivateCategory(int id)
        //{
        //    var categoryValue = help.GetCategory(id);
        //    help.UpdateCategory(categoryValue);

        //    return RedirectToAction("AdminCategories");
        //}
        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var categoryValue = help.GetCategory(id);
            List<SelectListItem> valuecategory = (from x in help.ListAllCategory()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Category_Name,
                                                      Value = x.Category_ID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;

            return View(categoryValue);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditCategory(Categories category, HttpPostedFileBase Img_Url)
        {
            var categoryValue = help.GetCategory(category.Category_ID);
            List<SelectListItem> valuecategory = (from x in help.ListAllCategory()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Category_Name,
                                                      Value = x.Category_ID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;


            categoryValue.Category_Name = category.Category_Name;
            categoryValue.Category_Description = category.Category_Description;
            categoryValue.Category_Parent_ID = category.Category_Parent_ID;


            if (Img_Url != null)
            {
                string filename = Path.GetFileName(Img_Url.FileName);
                string extension = Path.GetExtension(Img_Url.FileName);
                string filepath = "/CategoryImage/" + filename;
                Img_Url.SaveAs(Server.MapPath(filepath));
                categoryValue.Img_Url = "/CategoryImage/" + filename;
            }

            help.UpdateCategory(categoryValue);

            return RedirectToAction("AdminCategories");
        }


        public PartialViewResult partialcagir(int id = 0)
        {

            Categories Obj = help.GetCategory(id);

            List<SelectListItem> valuecategory = (from x in help.ListAllCategory()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Category_Name,
                                                      Value = x.Category_ID.ToString(),
                                                  }).ToList();
            ViewBag.vlc = valuecategory;

            return PartialView("/Views/Home/_edit.cshtml", Obj);

        }
        public JsonResult ActivePassive(int ID)
        {


            bool CategoryValue = help.UpdateCategorywithID(ID);



            return Json(CategoryValue, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Search(string word)
        {
            ViewBag.word = word;
            var values = from x in help.ListAllQuestion() select x;
            if (!string.IsNullOrEmpty(word))
            {
                values = values.Where(y => y.Answer_Content.Contains(word) || y.Question_Details.Contains(word) || y.Question_Title.Contains(word));
            }

            return View(values.ToList());
        }

        [HttpGet]
        public ActionResult CategoryDetails(int id)
        {

            //id = 1;
            //List<CatIDQuestions> Listeleme =
            //   (from question in help.ListAllQuestion()
            //    join category in help.ListAllCategory() on question.Category_ID equals category.Category_ID
            //    join parentcategory in help.ListAllCategory() on category.Category_Parent_ID equals parentcategory.Category_ID
            //    where parentcategory.Category_ID==id
            //    select new CatIDQuestions
            //    {
            //        Question_ID = question.Question_ID,
            //        Question_Category_Name = category.Category_Name,
            //        Question_Title = question.Question_Title,
            //        Question_Details = question.Question_Details,
            //        Answer_Content = question.Answer_Content,
            //        Question_Category_ID = question.Category_ID,
            //        Category_ID = category.Category_ID,
            //        Question_Category_Parent_ID = id,
            //        Question_Category_Parent_Name=parentcategory.Category_Name,
            //        Create_Date = question.Create_Date,
            //        IsActive = question.IsActive,
            //        Reading_Count = question.Reading_Count
            //    }).ToList();
            //return View(Listeleme);
            FAQ.ViewModel.IndexModel indexModel = new ViewModel.IndexModel();
            indexModel.Category = help.GetCategory(id);
            indexModel.CategoryList = help.ListSubCategoriesByID(indexModel.Category.Category_ID);
            indexModel.QuestionList = help.ListAllQuestion();

            return View(indexModel);

        }
        public ActionResult Categories()
        {
            FAQ.ViewModel.categoryListViewModel ttt = new ViewModel.categoryListViewModel();
            ttt.CategoryList = help.ListAllCategory();
            ttt.Category = new Categories();
            return View(ttt);
        }
        [HttpGet]
        public ActionResult QuestionDetails(int id)
        {
            Questions questions = help.GetQuestion(id);
            questions.Reading_Count++;
            help.UpdateQuestion(questions);
            return View(questions);
        }
        public ActionResult Questions()
        {
            List<QuestionCategory> Listeleme =
               (from question in help.ListAllQuestion()
                join category in help.ListAllCategory() on question.Category_ID equals category.Category_ID
                select new QuestionCategory
                {
                    Question_ID = question.Question_ID,
                    Question_Category_Name = category.Category_Name,
                    Question_Title = question.Question_Title,
                    Question_Details = question.Question_Details,
                    Answer_Content = question.Answer_Content,
                    Category_ID = question.Category_ID,
                    Create_Date = question.Create_Date,
                    IsActive = question.IsActive,
                    Reading_Count = question.Reading_Count
                }).ToList();
            return View(Listeleme);
        }

    }
}