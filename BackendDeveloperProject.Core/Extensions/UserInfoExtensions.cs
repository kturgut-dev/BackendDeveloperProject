using BackendDeveloperProject.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace BackendDeveloperProject.Core.Extensions
{
    public static class UserInfoExtensions
    {
        private static readonly IHttpContextAccessor HttpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        public static long? GetUserId()
        {
            var result = HttpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return result != null ? long.Parse(result) : default;
        }

        public static string? GetUserFullName()
        {
            return HttpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        }

        public static string? GetUserEmail()
        {
            return HttpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }

        public static string? Get(string key)
        {
            return HttpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == key)?.Value;
        }

        public static IEnumerable<T>? GetListForKey<T>(string key)
        {
            string jsonData = Get(key);
            if (string.IsNullOrEmpty(jsonData)) return default;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<T>>(jsonData);
        }
    }
}
