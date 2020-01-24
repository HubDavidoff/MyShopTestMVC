using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }
        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }
        public void Update(ProductCategory productCategory)
        {
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.Id == productCategory.Id);
            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productCategory;

            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        public ProductCategory Find(String id)
        {
            ProductCategory productCategoryToFind = productCategories.Find(p => p.Id == id);
            if (productCategoryToFind != null)
            {
                return productCategoryToFind;

            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }
        public void Delete(string id)
        {
            ProductCategory productCategory = productCategories.Find(p => p.Id == id);
            if (productCategory != null)
            {
                productCategories.Remove(productCategory);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}

