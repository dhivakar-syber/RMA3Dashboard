using Microsoft.Extensions.DependencyInjection;
using SyberGate.RMACT.HealthChecks;

namespace SyberGate.RMACT.Web.HealthCheck
{
    public static class AbpZeroHealthCheck
    {
        public static IHealthChecksBuilder AddAbpZeroHealthCheck(this IServiceCollection services)
        {
            var builder = services.AddHealthChecks();
            builder.AddCheck<RMACTDbContextHealthCheck>("Database Connection");
            builder.AddCheck<RMACTDbContextUsersHealthCheck>("Database Connection with user check");
            builder.AddCheck<CacheHealthCheck>("Cache");

            // add your custom health checks here
            // builder.AddCheck<MyCustomHealthCheck>("my health check");

            return builder;
        }
    }
}
