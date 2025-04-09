using Domain;
using DataAccess;
using DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    options.CallbackPath = builder.Configuration["Authentication:Google:CallbackPath"];

    options.Events.OnTicketReceived = async context =>
    {
        var claims = context.Principal.Identities.FirstOrDefault()?.Claims;
        var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(name))
        {
            using var scope = context.HttpContext.RequestServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<WebShopDbContext>();

            var user = dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                user = new Domain.User
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email
                };

                var shoppingCart = new Domain.ShoppingCart(user.Id);
                dbContext.Users.Add(user);
                dbContext.ShoppingCarts.Add(shoppingCart);

                await dbContext.SaveChangesAsync();
            }
        }
    };
});

builder.Services.AddAuthorization();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddDbContext<WebShopDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("WebShopDB")
    ));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WebShopDbContext>();

    if (!dbContext.Database.CanConnect())
    {
        throw new NotImplementedException("cant connect to db");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
