using Codecool.CodecoolShop.Models;
using System.Collections;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IProductDbDao : IDbDao<Product>
    {
        IEnumerable<Product> GetBySupplier(Supplier supplier);
        IEnumerable<Product> GetByProductCategory(ProductCategory productCategory);
        IEnumerable<Product> GetByBoth(Supplier supplier, ProductCategory productCategory);
    }
}
