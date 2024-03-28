using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BackendDeveloperProject.Core.Helpers
{
    public static class ServiceTool
    {
        public static IServiceProvider? ServiceProvider { get; set; }
        public static IConfiguration? Configuration { get; set; }
        public static IHttpContextAccessor? HttpContextAccessor { get; set; }
    }
}
