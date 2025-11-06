using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoDB2.Models;

namespace DemoDB2.Controllers
{
    public class CategoriesController : Controller
    {
        DBUserEntities database = new DBUserEntities();
        public PartialViewResult CategoryPartial()
        {
            var cateList = database.Categories.ToList();
            return PartialView(cateList);
        }
        // GET: Categories
        public ActionResult Index(string _name)
        {
            if(_name != null)
            return View(database.Categories.ToList());
            else
                return View(database.Categories.Where(s => s.NameCate.Contains(_name)).ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                database.Categories.Add(category);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error Create New");
            }
        }
        public ActionResult Details(int id)
        {
            return View(database.Categories.Where(s => s.Id == id).FirstOrDefault());
        }
        public ActionResult Edit(int id)
        {
            return View(database.Categories.Where(s => s.Id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Category category)
        {          
                database.Entry(category).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("Index");           
        }
        public ActionResult Delete(int id)
        {
            return View(database.Categories.Where(s => s.Id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                category = database.Categories.Where(s => s.Id == id).FirstOrDefault();
                database.Categories.Remove(category);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This Data is using in other table, Error Delete!");
            }
        }
    }
}