using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using WebApi.Domain.Interfaces;
using WebApi.Domain.Entities;
using WebApi.Service.Controllers;

namespace WebApi.Service.Tests
{
    public class UserControllerTests
    {
        ILogger<UserController> _logger = new Logger<UserController>(new NullLoggerFactory());
        User user;

        [SetUp]
        public void Setup()
        {
            user = new User()
            {
                FirstName = "Adam",
                MiddleName = "Gerhard",
                LastName = "Beeson",
                UserId = "123456789"
            };
        }

        [Test]
        public async Task GetUser_ShouldReturnBorrower()
        {
            var goodId = "123456789";
            var userRepMock = new Mock<IUserRepository>();
            userRepMock.Setup(br => br.GetUser(goodId, new CancellationToken())).ReturnsAsync(user);

            var controller = new UserController(_logger, userRepMock.Object);
            var result = await controller.Get(goodId);

            Assert.IsTrue(result == user);
        }

        [Test]
        public async Task GetUser_ShouldReturnNull()
        {
            var badId = "potato";
            var userRepMock = new Mock<IUserRepository>();
            userRepMock.Setup(br => br.GetUser(badId, new CancellationToken())).ReturnsAsync((User)null);

            var controller = new UserController(_logger, userRepMock.Object);
            var result = await controller.Get(badId);

            Assert.IsTrue(result == null);
        }
    }
}