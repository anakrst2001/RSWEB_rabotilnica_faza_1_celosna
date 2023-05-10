using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCBook.Data;
using MVCBook.Interfaces;
using MVCBook.Models;
using MVCBook.Services;

namespace MVCBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<MVCBookContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MVCBookContext") ?? throw new InvalidOperationException("Connection string 'MVCBookContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<IBufferedFileUploadService, BufferedFileUploadLocalService>();
            builder.Services.AddTransient<IBufferedFileUploadService1, BufferedFileUploadLocalService1>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                SeedData.Initialize(services);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Books}/{action=Index}/{id?}");

            app.Run();
        }
    }
}