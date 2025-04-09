using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public User() { }

        public User(Guid id, string name, string email, ShoppingCart shoppingCart)
        {
            Id = id;
            Name = name;
            Email = email;
            ShoppingCart = shoppingCart;
        }
    }
}
