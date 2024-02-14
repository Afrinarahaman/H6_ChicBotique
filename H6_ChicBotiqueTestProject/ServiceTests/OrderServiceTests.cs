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
using System.Numerics;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace H6_ChicBotiqueTestProject.ServiceTests
{
    public class OrderServiceTests
    {
        private readonly OrderService _orderService;
        private readonly Mock<IOrderRepository> _mockOrderRepository = new();
        private readonly Mock<IShippingDetailsRepository> _mockShippingDetailsRepository = new();
        private readonly Mock<IPaymentRepository> _mockPaymentRepository = new();
        private readonly Mock<IStockHandlerService> _mockStockHandlerService = new();

        Guid acc1id = Guid.NewGuid();
        Guid acc2id = Guid.NewGuid();

        public OrderServiceTests()
        {
            // Initializing the OrderService with the mock order repository
            _orderService = new OrderService(_mockOrderRepository.Object, _mockShippingDetailsRepository.Object,
                _mockPaymentRepository.Object, _mockStockHandlerService.Object);
        }
        //Test for GetAllOrders method of service
        [Fact]
        public async void GetAllOrders_ShouldReturnListOfOrderResponses_WhenOrderExist()
        {
            // Arrange
            // Creating a list of accountInfo
            List<Order> orders = new List<Order>();
            List<OrderDetails> orderDetails = new List<OrderDetails>();
            orderDetails.Add(new OrderDetails
            {
                ProductId =1,
                ProductTitle = "asd",
                ProductPrice = 145,
                Quantity=1

            });
            ShippingDetails shippingDetails = new()
            {
                Address="zxc",
                City="cph",
                Country="dk",
                PostalCode="2200",
                Phone="12345678"

            };
           
            Payment payment = new()
            {
                Id=1,
                Status="paid",
                Amount=255,
                PaymentMethod="credit",
                TimePaid=DateTime.UtcNow,
                TransactionId="1234564321"
            };
            AccountInfo accountInfo = new()
            {
                Id = acc1id,
                CreatedDate = DateTime.UtcNow,
                
            };
            orders.Add(new Order
            {
                OrderDate = DateTime.Now,
                AccountInfoId =acc1id,
                Payment=payment,
                AccountInfo=accountInfo,
                ShippingDetails=shippingDetails,
                OrderDetails = orderDetails

            });
            // Setting up the mock accountInfo repository to return the list of accountInfo
            _mockOrderRepository
                .Setup(x => x.SelectAllOrders())
                .ReturnsAsync(orders);

            // Act
            var result = await _orderService.GetAllOrders();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
            Assert.IsType<List<OrderAndPaymentResponse>>(result);
        }

        

    }

}


