using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            // var objCategoryList = _db.Categories.ToList(); syntax 1
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            //For Checking if the Name & Display order is same on Server Side
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name & Display Order Can not be of same Match");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["Success"] = "Added Successfully !!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(x=> x.Id==id); //Ways to retrive particular record from db
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(x=> x.Id==id);

            //Check if categories are null
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            //For Checking if the Name & Display order is same on Server Side
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name & Display Order Can not be of same Match");
            }
            if (ModelState.IsValid)
            {
                _db.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = "Updated Successfully !!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(x=> x.Id==id); //Ways to retrive particular record from db
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(x=> x.Id==id);

            //Check if categories are null
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Remove(obj);
            _db.SaveChanges();
            TempData["Success"] = "Deleted Successfully !!";
            return RedirectToAction("Index");

        }

    }
}
