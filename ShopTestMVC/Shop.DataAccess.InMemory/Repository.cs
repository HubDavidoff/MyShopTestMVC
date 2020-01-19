using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using Shop.Core.Models;

namespace Shop.DataAccess.InMemory
{
    public class Repository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public Repository()
        {
            products = cache["products"] as List<Product>;
            if(products == null)
            {
                products = new List<Product>();
            }
        }
        public void Commit()
        {
            cache["products"] = products;
        }
        
        public void Insert(Product p)
        {
            products.Add(p);
        }
        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id);
            if(productToUpdate != null)
            {
                productToUpdate = product;

            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        public Product Find(String id)
        {
            Product productToFind = products.Find(p => p.Id == id);
            if (productToFind != null)
            {
                return productToFind;

            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }
        public void Delete(string id)
        {
            Product product = products.Find(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);

            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
