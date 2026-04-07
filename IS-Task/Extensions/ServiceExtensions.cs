using IS_Task.Core.Interfaces;
using IS_Task.Core.Services;
using IS_Task.Data;
using IS_Task.Data.Entities;
using IS_Task.Interfaces;
using IS_Task.Mappings;
using IS_Task.Services;
using IS_Task.Shared.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace IS_Task.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .UseSnakeCaseNamingConvention());

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }

        public static IServiceCollection AddIdentityToTheApplication(this IServiceCollection services)
        {
            services.AddIdentity<IdentityApplicationUser, IdentityRole<long>>()
                .AddRoles<IdentityRole<long>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IEmailSender, NoOpEmailSender>();

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<BusinessMapperProfile>();
            });

            return services;
        }

        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<long>>>();

                if (!await roleManager.RoleExistsAsync("Admin"))
                    await roleManager.CreateAsync(new IdentityRole<long>("Admin"));

                if (!await roleManager.RoleExistsAsync("User"))
                    await roleManager.CreateAsync(new IdentityRole<long>("User"));

                if (await userManager.FindByEmailAsync("admin@test.com") == null)
                {
                    var admin = new IdentityApplicationUser { UserName = "admin@test.com", Email = "admin@test.com" };
                    await userManager.CreateAsync(admin, "Admin123!");
                    await userManager.AddToRoleAsync(admin, "Admin");
                }

                if (await userManager.FindByEmailAsync("user@test.com") == null)
                {
                    var user = new IdentityApplicationUser { UserName = "user@test.com", Email = "user@test.com" };
                    await userManager.CreateAsync(user, "User123!");
                    await userManager.AddToRoleAsync(user, "User");
                }

                if (!dbContext.Categories.Any())
                {
                    dbContext.Categories.AddRange(
                        new Category { Name = "Electronics", Description = "Gadgets and devices", Status = CategoryStatus.Active },
                        new Category { Name = "Clothing", Description = "Apparel and accessories", Status = CategoryStatus.Active },
                        new Category { Name = "Books", Description = "Fiction and non-fiction", Status = CategoryStatus.Active }
                    );
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Products.Any())
                {
                    var electronics = dbContext.Categories.First(x => x.Name == "Electronics");
                    var clothing = dbContext.Categories.First(x => x.Name == "Clothing");
                    var books = dbContext.Categories.First(x => x.Name == "Books");

                    dbContext.Products.AddRange(
                        new Product { Name = "Laptop", Description = "High performance laptop", Price = 1299.99m, CategoryId = electronics.Id, Status = ProductStatus.Active },
                        new Product { Name = "Smartphone", Description = "Latest model smartphone", Price = 799.99m, CategoryId = electronics.Id, Status = ProductStatus.Active },
                        new Product { Name = "Headphones", Description = "Noise cancelling headphones", Price = 199.99m, CategoryId = electronics.Id, Status = ProductStatus.Active },
                        new Product { Name = "T-Shirt", Description = "Cotton crew neck t-shirt", Price = 19.99m, CategoryId = clothing.Id, Status = ProductStatus.Active },
                        new Product { Name = "Jeans", Description = "Slim fit denim jeans", Price = 49.99m, CategoryId = clothing.Id, Status = ProductStatus.Active },
                        new Product { Name = "Clean Code", Description = "A handbook of agile software craftsmanship", Price = 34.99m, CategoryId = books.Id, Status = ProductStatus.Active },
                        new Product { Name = "The Pragmatic Programmer", Description = "Your journey to mastery", Price = 29.99m, CategoryId = books.Id, Status = ProductStatus.Active }
                    );
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
