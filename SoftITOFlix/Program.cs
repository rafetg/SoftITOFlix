using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoftITOFlix.Data;
using Microsoft.AspNetCore.Identity;
namespace SoftITOFlix
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<SoftITOFlixContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SoftITOFlixContext") ?? throw new InvalidOperationException("Connection string 'SoftITOFlixContext' not found.")));

                        builder.Services.AddDefaultIdentity<SoftITOFlixUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<SoftITOFlixIdentityContext>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
                        app.UseAuthentication();;

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
