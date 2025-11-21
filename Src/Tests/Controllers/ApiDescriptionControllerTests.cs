using WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using WebApi.Controllers;

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
            Assert.Equal(200, result.StatusCode);

            dynamic data = result.Value!;
            Assert.Equal("SkillBridge API", data.name);
            Assert.Equal("1.0.0", data.version);
            Assert.Equal("Online", data.status);
        }
    }
}
