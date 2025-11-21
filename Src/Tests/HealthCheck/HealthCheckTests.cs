using System.Net;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Tests.HealthCheck
{
    public class HealthCheckTests
    {
        private readonly CustomWebApplicationFactory _factory;

        public HealthCheckTests()
        {
            _factory = new CustomWebApplicationFactory();
        }

        [Fact]
        public async Task HealthCheck_ShouldReturnHealthy()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/healthz");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var body = await response.Content.ReadAsStringAsync();

            // Verifica que contém "Healthy" ou "OK"
            (body.Contains("Healthy") || body.Contains("OK")).Should().BeTrue();
        }
    }
}
