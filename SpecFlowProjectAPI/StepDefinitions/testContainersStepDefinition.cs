using Castle.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;
using ProyectoMock;
using NUnit.Framework;
using SpecFlowProjectAPI.Support;

namespace SpecFlowProjectAPI.StepDefinitions
{
    [Binding]
    internal class testContainersStepDefinition
    {
        private readonly ManageContainer _manageContainer;
        private UserService _userService;
        private IEnumerable<User> _users;
        public testContainersStepDefinition()
        {
            _manageContainer = new ManageContainer();
        }

        [Given("the PostgreSQL container is running")]
        public async Task GivenThePostgreSQLContainerIsRunning()
        {
            await _manageContainer.StartContainerAsync();
            _userService = _manageContainer.GetUserService();
            await _manageContainer.CreateTableForUserAsync();
        }

        [Given("I have created two customers")]
        public void GivenIHaveCreatedTwoCustomers()
        {
            _userService.Create(new User(1, "George", "soyGeorge@George.com"));
            _userService.Create(new User(2, "John", "soyJohn@John.com"));
        }

        [When("I retrieve the customers")]
        public void WhenIRetrieveTheCustomers()
        {
            _users = _userService.GetCustomers();
        }

        [Then("there should be (.*) customers")]
        public void ThenThereShouldBeCustomers(int expectedCount)
        {
            Assert.AreEqual(expectedCount, _users.Count());
            var firstCustomer = _users.First();
            var secondCustomer = _users.Skip(1).First();
            Assert.AreEqual(firstCustomer.Name, "George");
            Assert.AreEqual(secondCustomer.Name, "John");
            Assert.AreEqual(firstCustomer.Email, "soyGeorge@George.com");
            Assert.AreEqual(secondCustomer.Email, "soyJohn@John.com");
        }
    }
}
