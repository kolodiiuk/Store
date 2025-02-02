using FluentValidation;
using FluentValidation.AspNetCore;
using Store.API.Validators;

namespace Store.API;

public static class ContainerConfigExtensions
{
    public static void RegisterValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        services.AddValidatorsFromAssemblyContaining<UpdateProductDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateProductDtoValidator>();
        // services.AddValidatorsFromAssemblyContaining<UpdateProductDtoValidator>();
        // services.AddValidatorsFromAssemblyContaining<UpdateProductDtoValidator>();
        // services.AddValidatorsFromAssemblyContaining<UpdateProductDtoValidator>();
        // services.AddValidatorsFromAssemblyContaining<UpdateProductDtoValidator>();
        // services.AddValidatorsFromAssemblyContaining<UpdateProductDtoValidator>();
    }

    public static void RegisterAutomapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddAutoMapper(typeof(MappingProfile));
    }
}
