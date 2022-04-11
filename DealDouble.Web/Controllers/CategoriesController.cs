using DealDouble.Entities;
using DealDouble.Services;
using DealDouble.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class CategoriesController : Controller
    {
        CategoryService cser = new CategoryService();

        [HttpGet]
        public ActionResult Index()
        {
            CategoriesListingViewModel clvm = new CategoriesListingViewModel();
            clvm.PageTitle = "Category Index";
            clvm.PageDescription = "This is Category Index page";

            return View(clvm);
        }

        public PartialViewResult Listing()
        {
            CategoriesListingViewModel model = new CategoriesListingViewModel();

            model.PageTitle = "Category Index";
            model.PageDescription = "This is Category Index page";

            model.Categories = cser.GetCategories();

            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CategoryViewModel model = new CategoryViewModel();
            
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModel model)
        {
            Category category = new Category();
            category.Name = model.Name;            
            category.Description = model.Description;

            cser.SaveCategory(category);
            return RedirectToAction("Listing");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            CategoryViewModel model = new CategoryViewModel();
            var category = cser.GetCategory(id);
            model.ID = category.Id;
            model.Name = category.Name;
            model.Description = category.Description;

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModel model)
        {
            Category category = new Category();
            category.Id = model.ID;
            category.Name = model.Name;
            category.Description = model.Description;

            cser.UpdateCategory(category);
            return RedirectToAction("Listing");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            cser.DeleteCategory(id);
            return RedirectToAction("Listing");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            CategoryDetailsViewModel model = new CategoryDetailsViewModel();
            model.Category = cser.GetCategory(id);
            model.PageTitle = model.Category.Name + " Details";
            model.PageDescription = model.Category.Description != null ? (model.Category.Description.Length > 10 ? model.Category.Description.Substring(0, 10) : model.Category.Description) : "Category details.";

            return View(model);
        }
    }
}