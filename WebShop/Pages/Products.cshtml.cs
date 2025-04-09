using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace WebShop.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly ILogger<ProductsModel> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IUserRepository _userRepository;

        public List<Product> Products { get; private set; } = new();

        public ProductsModel(ILogger<ProductsModel> logger, IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
        }

        public void OnGet()
        {
            Products = _productRepository.GetProducts().Result.ToList(); 
        }

        public async Task<IActionResult> OnGetAddToCart(Guid productId)
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
            if (email != null)
            {
                User user = await _userRepository.GetByEmail(email);
                await _shoppingCartRepository.AddToCart(user.Id, productId, 1);
            }
            return RedirectToPage("/ShoppingCart");
        }
    }
}
