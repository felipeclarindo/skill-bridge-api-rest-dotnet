using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using WebApi.Models;
using Xunit;

namespace Tests.Controllers
{
    public class ApiDescriptionControllerTests
    {
        [Fact]
        public void GetApiInfo_ShouldReturnOk()
        {
            // Arrange
            var controller = new ApiDescriptionController();

            // Act
            var result = controller.GetApiDescription() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result!.StatusCode);

            var data = result.Value as ApiDescription;
            Assert.NotNull(data);
            Assert.Equal("SkillBridge API", data!.Name);
            Assert.Equal("1.0.0", data.Version);
            Assert.Equal("Online", data.Status);
            Assert.Equal("Felipe Clarindo", data.Desenvolvedor);
            Assert.Equal("https://github.com/felipeclarindo", data.Github);
            Assert.False(string.IsNullOrEmpty(data.Environment));
            Assert.True((DateTime.UtcNow - data.Timestamp).TotalSeconds < 10);
        }
    }
}
