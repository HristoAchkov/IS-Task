using IS_Task.Core.Interfaces;
using IS_Task.Core.Services;
using IS_Task.Data;
using IS_Task.Extensions;
using IS_Task.Interfaces;
using IS_Task.Mappings;
using IS_Task.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddIdentity<IdentityApplicationUser, IdentityRole<long>>()
    .AddRoles<IdentityRole<long>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IEmailSender, NoOpEmailSender>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<BusinessMapperProfile>();
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<long>>>();

    string[] roles = { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole<long>(role));
    }

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityApplicationUser>>();

    var user = await userManager.FindByEmailAsync("achkovh@yahoo.com");

    if (user != null)
        await userManager.AddToRoleAsync(user, "Admin");

}

app.Run();
