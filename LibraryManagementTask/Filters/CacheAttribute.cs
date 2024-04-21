using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace LibraryManagementTask.Filters
{
    public class CacheAttribute: ActionFilterAttribute
    {
        private readonly TimeSpan _expiry = TimeSpan.FromMinutes(3);

        public CacheAttribute()
        {
            
        }

        public CacheAttribute(TimeSpan expiry)
        {
            _expiry = expiry;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cache = context.HttpContext.RequestServices
                        .GetRequiredService<IMemoryCache>();

            var key = context.HttpContext.Request.Path +
                context.HttpContext.Request.QueryString.Value;

            var cachedResult = cache.Get(key);

            if (cachedResult != null)
                context.Result = new OkObjectResult(cachedResult);
            else
            {
                var response = await next();
                if (response.Result is OkObjectResult objectResult)
                     cache.Set(key, objectResult.Value, _expiry);
            }
        }
    }
}
