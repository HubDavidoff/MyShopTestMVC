using Shop.Core.Models;
using Shop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        // GET: ProductCategory
        ProductCategoryRepository context;
        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository();
        }
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }
        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (productCategory != null)
            {
                context.Insert(productCategory);
                context.Commit();

                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult Edit(string Id)
        {
            ProductCategory productCategory = context.Find(Id);
            if (productCategory != null)
            {
                return View(productCategory);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string Id)
        {
            ProductCategory categoryToEdit = context.Find(Id);
            if (categoryToEdit != null)
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }
                else
                {
                    categoryToEdit.Category = productCategory.Category;
                    context.Commit();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult Delete(string Id)
        {
            ProductCategory productCategory = context.Find(Id);
            if (productCategory != null)
            {
                return View(productCategory);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategory = context.Find(Id);
            if (productCategory != null)
            {
                context.Delete(productCategory.Id);
                context.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}