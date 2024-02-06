using H6_ChicBotique.Controllers;
using H6_ChicBotique.DTOs;
using H6_ChicBotique.Helpers;
using H6_ChicBotique.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H6_ChicBotiqueTestProject.ControllerTests
{
    public class UserControllerTest
    {
        private readonly UserController _userController;
        private readonly Mock<IUserService> _mockuserService = new();
        public UserControllerTest()
        {
            _userController = new(_mockuserService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenuserExists()
        {
            //Arrange
            List<UserResponse> users = new();
            users.Add(new()
            {

                Id = 1,
                FirstName = "Peter",
                LastName = "Aksten",
                Email = "peter@abc.com",

                Role = Role.Administrator

            });

            users.Add(new()
            {
                Id = 2,
                FirstName = "Rizwanah",

                LastName = "Mustafa",
                Email = "riz@abc.com",

                Role = Role.Member


            });

            _mockuserService
                .Setup(x => x.GetAll())
                .ReturnsAsync(users);

            //Act
            var result = await _userController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNouserExists()
        {
            //Arrange

            List<UserResponse> users = new();

            _mockuserService
                .Setup(x => x.GetAll())
                .ReturnsAsync(users);

            //Act
            var result = await _userController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            //Arrange                      
            _mockuserService
                .Setup(x => x.GetAll())
                .ReturnsAsync(() => null);

            //Act
            var result = await _userController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange                      
            _mockuserService
                .Setup(x => x.GetAll())
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _userController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
       /* [Fact]
        public async void GetIdByEmail_ShouldReturnStatusCode200_WhenDataExists()
        {
            //Arrange
            // int AccountId = 1;
            string Email = "peter@abc.com";
            UserResponse User = new()
            {
                Id = 1,
                FirstName = "Peter",

                LastName = "Aksten",
                Email = "peter@abc.com",

                Role = Role.Administrator

            };

            _mockuserService
                .Setup(x => x.GetIdByEmail(It.IsAny<string>()))
                .ReturnsAsync(User);

            //Act
            var result = await _userController.GetIdByEmail(Email);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
       */
       /* [Fact]
        public async void GetIdByEmail_ShouldReturnStatusCode404_WhenUserDoesNotExists()
        {
            //Arrange
            //int userId = 1;
            string UserEmail = "peter@abc.com";

            _mockuserService
                .Setup(x => x.GetIdByEmail(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _userController.GetIdByEmail(UserEmail);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetIdByEmail_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange

            _mockuserService
                .Setup(x => x.GetIdByEmail(It.IsAny<string>()))
                .ReturnsAsync(() => throw new System.Exception("This is an Exception"));

            //Act
            var result = await _userController.GetIdByEmail("peter@abc.com");

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
       */
    }
}
