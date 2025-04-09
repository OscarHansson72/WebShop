using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IProductRepository
    {
        public Task<Product> GetProduct(Guid id);
        public Task<IEnumerable<Product>> GetProducts();
    }
}
