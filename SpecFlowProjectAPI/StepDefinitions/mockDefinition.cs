using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Reqnroll;
using System.Threading.Tasks;
using ProyectoMock;

namespace SpecFlowProjectAPI.StepDefinitions
{
    [Binding]
    public class mockDefinition
    {
        private readonly Mock<IUserService> _mockUserService;
        private UserManager _userManager;
        private User _resultUser;

        public mockDefinition()
        {
            _mockUserService = new Mock<IUserService>();
            _userManager = new UserManager(_mockUserService.Object);
        }

        [Given(@"the user service returns a user with ID (.*)")]
        public void GivenTheUserServiceReturnsAUserWithID(int userId)
        {
            _mockUserService.Setup(service => service.GetUserAsync(userId))
                       .ReturnsAsync(new User
                       {
                           Id = userId,
                           Name = "John Doe",
                           Email = "john.doe@example.com"
                       });
        }

        [When(@"I request the user with ID (.*)")]
        public async Task WhenIRequestTheUserWithID(int userId)
        {
            _resultUser = await _userManager.GetUser(userId);
        }

        [Then(@"I should receive the user details with ID (.*)")]
        public void ThenIShouldReceiveTheUserDetailsWithID(int expectedUserId)
        {
            Assert.NotNull(_resultUser);
            Assert.AreEqual(expectedUserId, _resultUser.Id);
            Assert.AreEqual("John Doe", _resultUser.Name);
            Assert.AreEqual("john.doe@example.com", _resultUser.Email);
            _mockUserService.Verify(service => service.GetUserAsync(expectedUserId), Times.Once);
        }

    }
}
