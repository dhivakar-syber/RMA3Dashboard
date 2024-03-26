using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SyberGate.RMACT.EntityFrameworkCore;

namespace SyberGate.RMACT.HealthChecks
{
    public class RMACTDbContextHealthCheck : IHealthCheck
    {
        private readonly DatabaseCheckHelper _checkHelper;

        public RMACTDbContextHealthCheck(DatabaseCheckHelper checkHelper)
        {
            _checkHelper = checkHelper;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_checkHelper.Exist("db"))
            {
                return Task.FromResult(HealthCheckResult.Healthy("RMACTDbContext connected to database."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("RMACTDbContext could not connect to database"));
        }
    }
}
