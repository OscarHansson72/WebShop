using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public List<ShoppingCartItem> Items { get; set; } = new();

        [Timestamp]
        public byte[] Version { get; set; }

        private ShoppingCart() { }

        public ShoppingCart(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
        }
    }
}
