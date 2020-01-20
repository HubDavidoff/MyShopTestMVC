using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Core.Models;
using Shop.DataAccess.InMemory;

namespace Shop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        // GET: ProductManager
        ProductRepository context;
        public ProductManagerController()
        {
            context = new ProductRepository();
        }
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }
        public ActionResult CreateProduct()
        {
            Product product = new Product();
            return View(product);
        }
        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string Id)
        {
            Product product = context.Collection().ToList().Find(p => p.Id == Id);
            if(product != null)
            {
                return View(product);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult Edit(Product p, string Id)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit != null)
            {
                if (ModelState.IsValid)
                {
                    productToEdit.Category = p.Category;
                    productToEdit.Name = p.Name;
                    productToEdit.Price = p.Price;
                    productToEdit.Image = p.Image;
                    productToEdit.Description = p.Description;
                    context.Commit();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(p);
                }
            }
            else
            {
                return HttpNotFound();
            }

        }
        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if(productToDelete != null)
            {
                return View(productToDelete);
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
            Product productToDelete = context.Find(Id);
            if (productToDelete != null)
            {
                context.Delete(productToDelete.Id);
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