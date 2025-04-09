using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IShoppingCartRepository
    {
        public Task<ShoppingCart> GetCartByUserId(Guid userId);
        public Task AddToCart(Guid userId, Guid productId, int quantity);
        public Task RemoveFromCart(Guid userId, Guid productId);
        public Task ClearCart(Guid userId); 
    }
}
