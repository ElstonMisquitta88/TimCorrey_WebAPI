using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MonitoringAPI.Controllers.HealthChecks;

public class RandomHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {

        // (1) Simulated Response Time
        int ResponseTime = Random.Shared.Next(0, 300);


        if (ResponseTime < 100)
        {
            return Task.FromResult(
                HealthCheckResult.Healthy($"The Response Time is Healthy : {ResponseTime}")
                );
        }
        else if (ResponseTime < 200)
        {
            return Task.FromResult(
                HealthCheckResult.Degraded($"The Response Time is Degraded : {ResponseTime}")
                );
        }
        else
        {
            return Task.FromResult(
                HealthCheckResult.Unhealthy($"The Response Time is Unhealthy : {ResponseTime}")
                );
        }



    }


}
