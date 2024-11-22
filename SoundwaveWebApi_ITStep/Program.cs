using Core;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using SoundwaveWebApi_ITStep.Extensions;
using SoundwaveWebApi_ITStep.ServiceExtensions;

namespace SoundwaveWebApi_ITStep
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.ConfigureKestrel(options => options.Limits.MaxRequestBodySize = 50 * 1024 * 1024);

            string connectionString = builder.Configuration.GetConnectionString("SomeeDb")!;

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext(connectionString);
            builder.Services.AddIdentity();
            builder.Services.AddRepository();

            builder.Services.AddFluentValidators();            
            builder.Services.AddAutoMapper();            
            builder.Services.AddCustomServices();            

            // Custom exception handler
            builder.Services.AddExceptionHandler();

            // Add JWT
            builder.Services.AddJWT(builder.Configuration);
            builder.Services.AddSwaggerJWT();

            // Add CORS
            builder.Services.AddCorsPolicies();

            var app = builder.Build();

            // Seed initial data
            using (var scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.SeedRoles().Wait();
                scope.ServiceProvider.SeedAdmin(app.Configuration).Wait();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (app.Environment.IsProduction())
            {
                //app.UseMiddleware<ErrorHandlerMiddleware>();
                app.UseExceptionHandler();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseCors("front-end-cors-policy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
