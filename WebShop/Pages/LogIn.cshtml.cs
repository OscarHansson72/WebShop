using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebShop.Pages
{
    public class LogInModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnGetGoogleLogin()
        {
            var redirectUrl = Url.Page("/Index"); // Redirect to home page after login
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
    }
}
