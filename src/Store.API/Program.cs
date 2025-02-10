using System.Diagnostics;
using Store.API;
using Store.DataAccess;
using Store.Domain;
using Microsoft.EntityFrameworkCore;
using Store.API.Middleware;

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

builder.Services.AddDbContext<AppDbContext>(options =>
    options
        .UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")!));

builder.Services.RegisterRepositories();
builder.Services.RegisterServices();
builder.Services.RegisterValidators();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthorization();
app.UseAuthentication();
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