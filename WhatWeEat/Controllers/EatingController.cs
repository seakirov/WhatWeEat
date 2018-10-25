using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WhatWeEat.Models;

namespace WhatWeEat.Controllers
{
    public class EatingController : Controller
    {
        private readonly WhatWeEatContext _db = new WhatWeEatContext();

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.FoodId = new SelectList(_db.Foods, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FoodId")] Eating eating, User user)
        {
            if (ModelState.IsValid)
            {
                var newUser = _db.Users.FirstOrDefault(q => q.Name.Equals(user.Name, StringComparison.CurrentCultureIgnoreCase) && q.Email.Equals(user.Email, StringComparison.CurrentCultureIgnoreCase));
                var isNewUser = false;
                if (newUser == null)
                {
                    isNewUser = true;
                    newUser = _db.Users.Add(user);
                    _db.SaveChanges();
                }
                eating.UserId = newUser.Id;
                eating.DateTimeStamp = DateTime.Now;
                _db.Eatings.Add(eating);
                _db.SaveChanges();

                ViewBag.isNewUser = isNewUser;
                ViewBag.foodName = _db.Foods.Find(eating.FoodId)?.Name;
                ViewBag.curUser = newUser;
                ViewBag.cntFoodToday = _db.Eatings.Count(q => DbFunctions.TruncateTime(q.DateTimeStamp) == DbFunctions.TruncateTime(DateTime.Now) && q.FoodId == eating.FoodId);
                ViewBag.cntFoodUser = _db.Eatings.Count(q => q.UserId == newUser.Id && q.FoodId == eating.FoodId);

                var eatings = _db.Eatings.Include(e => e.Food).Include(e => e.User);
                return View("Index", eatings.OrderByDescending(q => q.DateTimeStamp).Take(15).ToList());
            }

            ViewBag.FoodId = new SelectList(_db.Foods, "Id", "Name");
            return View(eating);
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