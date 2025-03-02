using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using proekt1.Data;
using Microsoft.AspNetCore.Identity;
using proekt1.Models;
namespace proekt1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<proekt1Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("proekt1Context") ?? throw new InvalidOperationException("Connection string 'proekt1Context' not found.")));
            builder.Services.AddDbContext<PersonContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PersonContextConnection")));

            builder.Services.AddDefaultIdentity<Person>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<PersonContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.MapRazorPages();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            /*
            using (var scope = app.Services.CreateScope())
            {
                //suzdava novi roli kato proverqva dali veche ne sushtestvuvat
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "admin", "employee", "user" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            //dobavq admin, ako veche nqma
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Client>>();

                string email = "admin@admin.com";
                string password = "AdminPassword1234$";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new Person();
                    user.UserName = email;
                    user.Email = email;
                    user.EmailConfirmed = true;
                    user.FirstName = "Administrator";

                    await userManager.CreateAsync(user, password);

                    await userManager.AddToRoleAsync(user, "admin");

                }
            }
            //kraj na pormqnta*/

            app.Run();
        }
    }
}
