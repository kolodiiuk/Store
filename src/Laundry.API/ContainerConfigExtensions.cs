using FluentValidation;
using FluentValidation.AspNetCore;
using Laundry.API.Validators;

namespace Laundry.API;

public static class ContainerConfigExtensions
{
    public static void RegisterValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        services.AddValidatorsFromAssemblyContaining<UpdateServiceDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateServiceDtoValidator>();
        // services.AddValidatorsFromAssemblyContaining<UpdateServiceDtoValidator>();
        // services.AddValidatorsFromAssemblyContaining<UpdateServiceDtoValidator>();
        // services.AddValidatorsFromAssemblyContaining<UpdateServiceDtoValidator>();
        // services.AddValidatorsFromAssemblyContaining<UpdateServiceDtoValidator>();
        // services.AddValidatorsFromAssemblyContaining<UpdateServiceDtoValidator>();
    }

    public static void RegisterAutomapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddAutoMapper(typeof(MappingProfile));
    }
}
