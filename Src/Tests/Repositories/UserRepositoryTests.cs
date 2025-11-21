using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using WebApi.Repositories.Interfaces;
using WebApi.Models; 
using Xunit;

namespace Tests.Repositories
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task GetByIdAsync_ShouldReturnUsuario()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();

            var expected = new Usuario
            {
                Id = 1,
                Nome = "Felipe",
                Email = "felipe@example.com"
            };

            mockRepo.Setup(r => r.GetByIdAsync(1))
                    .ReturnsAsync(expected);

            // Act
            var result = await mockRepo.Object.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result!.Nome.Should().Be("Felipe");
            result.Email.Should().Be("felipe@example.com");
        }
    }
}
