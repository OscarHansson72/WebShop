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
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        WebShopDbContext _dbContext;

        public ShoppingCartRepository(WebShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddToCart(Guid userId, Guid productId, int quantity)
        {
            var cart = await GetCartByUserId(userId);
            if (cart == null) return;

            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null) return;

            var existingItem = cart.Items.FirstOrDefault(p => p.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                await _dbContext.ShoppingCartItems.AddAsync(new ShoppingCartItem(cart, product, quantity));
            }

            await _dbContext.SaveChangesAsync();

            //bool saveFailed;
            //do
            //{
            //    saveFailed = false;
            //    try
            //    {
            //        await _dbContext.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException ex)
            //    {
            //        saveFailed = true;
            //        foreach (var entry in ex.Entries)
            //        {
            //            if (entry.Entity is ShoppingCart)
            //            {
            //                var databaseValues = await entry.GetDatabaseValuesAsync();
            //                if (databaseValues == null)
            //                {
            //                    throw new Exception("The shopping cart was deleted by another transaction.");
            //                }
            //                entry.OriginalValues.SetValues(databaseValues);
            //            }
            //        }
            //    }
            //} while (saveFailed);
        }


        public async Task ClearCart(Guid userId)
        {
            var cart = await GetCartByUserId(userId);
            if (cart == null) return;

            cart.Items.Clear();
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ShoppingCart> GetCartByUserId(Guid userId)
        {
            var cart = await _dbContext.ShoppingCarts
                .Include(sc => sc.Items)
                .ThenInclude(sci => sci.Product)
                .FirstOrDefaultAsync(sc => sc.UserId == userId);

            Console.WriteLine("-----------------------------");
            cart.Items.ForEach(item => Console.WriteLine(item));

            return cart;
        }

        public async Task RemoveFromCart(Guid userId, Guid productId)
        {
            var cart = await GetCartByUserId(userId);
            if (cart == null) return;

            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null) return;
            Console.WriteLine("-----------------------------");
            cart.Items.ForEach(item => Console.WriteLine(item));
            var existingItem = cart.Items.FirstOrDefault(p => p.ProductId == productId);
            if (existingItem != null)
            {
                cart.Items.Remove(existingItem);
                Console.WriteLine("HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
            }
            Console.WriteLine("dsjkalödjashödhaskjdhaskjhdskajndhlksand");
            Console.WriteLine("");
            cart.Items.ForEach(item => Console.WriteLine(item));

            await _dbContext.SaveChangesAsync();
        }
    }
}
