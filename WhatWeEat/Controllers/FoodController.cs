using System.Linq;
using System.Web.Mvc;
using WhatWeEat.Models;

namespace WhatWeEat.Controllers
{
    public class FoodController : Controller
    {
        private readonly WhatWeEatContext _db = new WhatWeEatContext();

        public ActionResult Index()
        {
            return RedirectToAction("Create", "Eating");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Food food)
        {
            if (ModelState.IsValid)
            {
                _db.Foods.Add(food);
                _db.SaveChanges();
                return RedirectToAction("Create", "Eating");
            }
            return PartialView(food);
        }

        public JsonResult IsFoodNameExist(string Name)
        {
            var isExist = _db.Foods.Any(x => x.Name == Name);
            return Json(!isExist, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}