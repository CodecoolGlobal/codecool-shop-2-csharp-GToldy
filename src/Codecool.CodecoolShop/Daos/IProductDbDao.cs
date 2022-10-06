using Codecool.CodecoolShop.Models;
using System.Collections;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IProductDbDao : IDbDao<Product>
    {
        IEnumerable<Product> GetBy(Supplier supplier);
        IEnumerable<Product> GetBy(ProductCategory productCategory);
        IEnumerable<Product> GetBy(Supplier supplier, ProductCategory productCategory);
    }
}
