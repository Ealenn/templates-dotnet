using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Dotnet.Template.IntegrationTests.Fake
{
    internal class FakeHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.FromResult(HealthCheckResult.Degraded());
        }
    }
}
