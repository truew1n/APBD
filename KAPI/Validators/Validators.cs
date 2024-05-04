using FluentValidation;

namespace LAB06.Validators
{
    public static class Validators
    {
        public static void RegisterValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<AnimalCreateRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<AnimalUpdateRequestValidator>();
        }
    }

}
