using LibraryManagementTask.Errors;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementTask.Extensions
{
    public static class ApiConfigurationExtensions
    {
        public static IServiceCollection ConfigureApiBehaviorOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = ctx =>
                {
                    var errors = ctx.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                    return new BadRequestObjectResult(new ValidationResponse(errors));
                };
            });

            return services;
        }
    }
}
