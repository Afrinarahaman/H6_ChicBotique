using H6_ChicBotique.Database.Entities;
using H6_ChicBotique.DTOs;
using H6_ChicBotique.Helpers;
using H6_ChicBotique.Repositories;
using H6_ChicBotique.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H6_ChicBotiqueTestProject.ServiceTests
{
    public class AccountInfoServiceTests
    {
        private readonly AccountInfoService _accountInfoService;
        private readonly Mock<IAccountInfoRepository> _mockAccountInfoRepository = 
            new Mock<IAccountInfoRepository>();

        Guid acc1id = Guid.NewGuid();
        Guid acc2id = Guid.NewGuid();

        public AccountInfoServiceTests()
        {
            // Initializing the AccountInfoService with the mock accountInfo repository
            _accountInfoService = new AccountInfoService(_mockAccountInfoRepository.Object);
        }
        //Test for GetAllAccountInfo method of service
        [Fact]
        public async void GetAllAccountInfo_ShouldReturnListOfAccountInfoResponses_WhenAccountInfoExist()
        {
            // Arrange
            // Creating a list of accountInfo
            List<AccountInfo> accountInfos = new();


            User newUser = new()
            {
                Id = 1,
                FirstName = "Peter",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Role = Role.Administrator

            };
            accountInfos.Add(new()
            {
                Id = acc1id,
                CreatedDate = DateTime.UtcNow,
                UserId=1,
            });
            /*Order newOrder = new()
            {
                Id = 1,
                OrderDate= DateTime.Now,
                AccountId = acc1id,

            };*/
            accountInfos.Add(new()
            {
                Id = acc2id,
                CreatedDate = DateTime.UtcNow,
                UserId=2,
            });



            /* HomeAddress newHomeAddress = new()
             {
                  AccountInfoId = acc1id,
                  Id = 1,

                  Address = "Husum",
                  City = "Copenhagen",
                  PostalCode = "2200",
                  Country = "Danmark",
                  TelePhone = "+228415799"

              };*/



            // Setting up the mock accountInfo repository to return the list of accountInfo
            _mockAccountInfoRepository
                .Setup(x => x.SelectAll())
                .ReturnsAsync(accountInfos);

            // Act
            var result = await _accountInfoService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
            Assert.IsType<List<AccountInfo>>(result);
        }

        
        }

    }


