using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Product { get; set; }
        public IEnumerable<ProductCategory> CategoryList { get; set; }
    }
}
