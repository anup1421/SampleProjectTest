using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Security.Cryptography;

namespace BuildingBlocks.API.Configs;

internal class CustomHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var expectedNumber = RandomNumberGenerator.GetInt32(60, 100);
        var actualNumber = RandomNumberGenerator.GetInt32(0, 100);

        var data = new Dictionary<string, object>
        {
            { nameof(expectedNumber), expectedNumber },
            { nameof(actualNumber), actualNumber }
        };

        var status = actualNumber switch
        {
            >= 0 and < 30 => HealthStatus.Unhealthy,
            >= 30 and < 60 => HealthStatus.Degraded,
            _ => HealthStatus.Healthy,
        };

        var result = new HealthCheckResult(status, null, null, data);
        return Task.FromResult(result);
    }
}
