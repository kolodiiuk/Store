using System.Diagnostics;
using FluentValidation;
using Laundry.API.Validators;
using FluentValidation.AspNetCore;
using Laundry.API;
using Laundry.DataAccess;
using Laundry.DataAccess.Repositories;
using Laundry.Domain;
using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddControllers();

builder.Services.AddDbContext<LaundryDbContext>(options =>
    options
        .UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.RegisterRepositories();
builder.Services.RegisterServices();
// builder.Services.AddFluentValidationAutoValidation();
// builder.Services.AddFluentValidationClientsideAdapters();
// builder.Services.AddValidatorsFromAssemblyContaining<UpdateServiceDtoValidator>();
// builder.Services.AddValidatorsFromAssemblyContaining<CreateServiceDtoValidator>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseAuthorization();
// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     try
//     {
//         await DataSeeder.SeedAsync(services);
//     }
//     catch (Exception ex)
//     {
//         Debug.WriteLine(ex.Message, ex.StackTrace);
//     }
// }
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();