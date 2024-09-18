using Core;
using Data;
using Microsoft.EntityFrameworkCore;
using SoundwaveWebApi_ITStep.ServiceExtensions;

namespace SoundwaveWebApi_ITStep
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("LocalDb")!;

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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseExceptionHandler();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
