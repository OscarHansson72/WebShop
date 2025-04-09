using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DbContexts;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ProductRepository : IProductRepository
    {
        WebShopDbContext _dbContext;

        public ProductRepository(WebShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Product> GetProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _dbContext.Set<Product>().ToListAsync();

            return products;
        }
    }
}
