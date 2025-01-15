using FluentValidation;
using Laundry.API.Validators;
using FluentValidation.AspNetCore;
using Laundry.API;
using Laundry.DataAccess;
using Laundry.DataAccess.Repositories;
using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Services;

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
// builder.Services.RegisterRepositories();
builder.Services.AddScoped<IServiceRepository, MockServiceRepo>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<UpdateServiceDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateServiceDtoValidator>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseAuthorization();

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
