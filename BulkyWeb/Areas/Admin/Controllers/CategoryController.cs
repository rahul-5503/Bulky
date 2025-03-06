using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        public readonly IUnitOfWork _db;
        public CategoryController(IUnitOfWork db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> categories = _db.Category.GetAll().ToList();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.Description)
            {
                ModelState.AddModelError("Name", "Name and Desciption must not be same");
            }
            if (ModelState.IsValid)
            {
                _db.Category.Add(category);
                _db.save();
                TempData["success"] = "Category Created Successful";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category category = _db.Category.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category? category)
        {

            if (ModelState.IsValid)
            {
                _db.Category.Update(category);
                _db.save();
                TempData["success"] = "Category Edited Successful";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category category = _db.Category.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category category = _db.Category.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Category.Remove(category);
                _db.save();
                TempData["success"] = "Category Deleted Successful";
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
