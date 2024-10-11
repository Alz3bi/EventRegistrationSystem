using EventRegistrationSystem.Data;
using EventRegistrationSystem.Repositories.Services;
using Microsoft.EntityFrameworkCore;

namespace EventRegistrationSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotNetEnv.Env.Load();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            string ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

            builder.Services.AddDbContext<EventSystemDbContext>(options => options.UseSqlServer(ConnectionString));

            // Register EmailService
            builder.Services.AddSingleton<EmailService>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var apiKey = Environment.GetEnvironmentVariable("MAILJET_API_KEY");
                var apiSecret = Environment.GetEnvironmentVariable("MAILJET_API_SECRET");
                return new EmailService(apiKey, apiSecret, configuration);
            });
            var app = builder.Build();

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
