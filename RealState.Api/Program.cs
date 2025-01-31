
using Microsoft.EntityFrameworkCore;
using RealState.Application.Contracts;
using RealState.Application.Contracts.Repositories;
using RealState.Application.Features.Properties.Create.Commands;
using RealState.Application.Services;
using RealState.Infrastructure;
using RealState.Infrastructure.Persistence;
using RealState.Infrastructure.Repositories;
using System.Text.Json.Serialization;

namespace RealState.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //add context
            builder.Services.AddDbContext<RealStateContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PropertiesDB"),
                sqlOptions => sqlOptions.EnableRetryOnFailure()));

            //add repositories
            builder.Services.AddScoped<IRealStateRepository, RealStateRepository>();

            //add UOW
            builder.Services.AddScoped<RealStateContext>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            //add services 
            builder.Services.AddScoped<IRealStateService, RealStateService>();
            builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(CreatePropertyCommandHandler).Assembly));


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
