using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ShoppingCartItem
    {
        public Guid Id { get; set; }
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public ShoppingCartItem() { }

        public ShoppingCartItem(ShoppingCart cart, Product product, int quantity)
        {
            Id = Guid.NewGuid();
            ShoppingCartId = cart.Id;
            ShoppingCart = cart;

            ProductId = product.Id;
            Product = product;

            Quantity = quantity;
        }
    }
}
