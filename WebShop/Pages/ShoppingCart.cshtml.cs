using DataAccess;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace WebShop.Pages
{
    [IgnoreAntiforgeryToken]
    public class ShoppingCartModel : PageModel
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IUserRepository _userRepositoty;
        public List<ShoppingCartItem> ShoppingCart { get; private set; } = new();


        public ShoppingCartModel(IShoppingCartRepository shoppingCartRepository, IUserRepository userRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepositoty = userRepository;
        }

        public async Task OnGet()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userRepositoty.GetByEmail(email);
            var cart = await _shoppingCartRepository.GetCartByUserId(user.Id);
            ShoppingCart = cart.Items.ToList();
        }

        public async Task<IActionResult> OnPostRemoveFromCart(Guid productId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userRepositoty.GetByEmail(email);

            if (user == null)
            {
                Console.WriteLine("User not found!");
                return RedirectToPage();
            }

            Console.WriteLine("Removing product " + productId + " from cart for user " + user.Id);

            await _shoppingCartRepository.RemoveFromCart(user.Id, productId);

            return RedirectToPage();
        }
    }
}
