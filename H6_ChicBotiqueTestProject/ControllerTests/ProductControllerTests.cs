﻿using H6_ChicBotique.Controllers;
using H6_ChicBotique.Database.Entities;
using H6_ChicBotique.DTOs;
using H6_ChicBotique.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;

namespace H6_ChicBotiqueTestProject.ControllerTests
{
    public class ProductControllerTests
    {
        // Instance of the ProductController being tested
        private readonly ProductController _productController;

        // Mocked ProductService for dependency injection in ProductController
        private readonly Mock<IProductService> _mockProductService = new();

        // Constructor initializes the ProductControllerTests instance
        public ProductControllerTests()
        {
            // Inject the mock ProductService into the ProductController
            _productController = new(_mockProductService.Object);
        }

        // Test case: GetAll should return StatusCode 200 when products exist
        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenProductsExist()
        {
            // Arrange

            List<ProductResponse> products = new();
            products.Add(new()
            {
                Id = 1,
                Title = "Fancy dress",
                Price = 299.99M,
                Description = "Kids dress",
                Image = "dress1.jpg",
                Stock = 10,
                CategoryId = 1
            });
            // Another product...
            products.Add(new()
            {
                Id = 2,
                Title = "Blue T-Shirt",
                Price = 199.99M,
                Description = "T-Shirt for men",
                Image = "BlueTShirt.jpg",
                Stock = 10,
                CategoryId = 2

            });

            products.Add(new()
            {
                Id = 3,
                Title = "Skirt",
                Price = 159.99M,
                Description = "Girls skirt",
                Image = "skirt1.jpg",
                Stock = 10,
                CategoryId = 1

            });
            products.Add(new()
            {
                    Id = 4,
                    Title = "Jumpersuit",
                    Price = 279.99M,
                    Description = "kids jumpersuit",
                    Image = "jumpersuit1.jpg",
                    Stock = 10,
                    CategoryId = 1

            });
            products.Add(new()
                  {
                    Id = 5,
                    Title = "Red T-Shirt",
                    Price = 199.99M,
                    Description = "T-Shirt for men",
                    Image = "RedT-Shirt.jpg",
                    Stock = 10,
                    CategoryId = 2
            });
            products.Add(new()
                    {
                     Id = 6,
                     Title = "Long dress",
                     Price = 299.99M,
                     Description = "Summer clothing",
                     Image = "floral-dress.jpg",
                     Stock = 10,
                     CategoryId = 3
            });
            products.Add(new()
                  {
                    Id = 7,
                    Title = "Red dress",
                    Price = 299.99M,
                    Description = "Party dress for women",
                    Image = "Red-dress.jpg",
                    Stock = 10,
                    CategoryId = 3
            });


            // Setup the mock ProductService to return the list of products
            _mockProductService.Setup(x => x.GetAllProducts()).ReturnsAsync(products);

            // Act
            var result = await _productController.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        // Test case: GetAll should return StatusCode 204 when no products exist
        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoProductsExist()
        {
            // Arrange
            List<ProductResponse> products = new List<ProductResponse>();

            // Setup the mock ProductService to return an empty list of products
            _mockProductService.Setup(x => x.GetAllProducts()).ReturnsAsync(products);

            // Act
            var result = await _productController.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        // Test case: GetAll should return StatusCode 500 when null is returned from the service
        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            // Arrange

            // Setup the mock ProductService to return null
            _mockProductService.Setup(x => x.GetAllProducts()).ReturnsAsync(() => null);

            // Act
            var result = await _productController.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        // Test case: GetAll should return StatusCode 500 when an exception is raised
        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange

            // Setup the mock ProductService to throw an exception
            _mockProductService.Setup(x => x.GetAllProducts()).ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _productController.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        // Test case: GetById should return StatusCode 200 when data exists
        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            int productId = 1;

            // Create a sample product response
            ProductResponse product = new ProductResponse
            {
                Id = 1,
                Title = " Fancy dress",
                Price = 299.99M,
                Description = "kids dress",
                Image = "dress1.jpg",
                Stock = 10,
                CategoryId = 1

            };

            // Setup the mock ProductService to return the sample product
            _mockProductService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync(product);

            // Act
            var result = await _productController.GetById(productId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        // Test case: GetById should return StatusCode 404 when product does not exist
        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenProductDoesNotExist()
        {
            // Arrange
            int productId = 1;

            // Setup the mock ProductService to return null
            _mockProductService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = await _productController.GetById(productId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        // Test case: GetById should return StatusCode 500 when an exception is raised
        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange

            // Setup the mock ProductService to throw an exception
            _mockProductService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync(() => throw new System.Exception("This is an Exception"));

            // Act
            var result = await _productController.GetById(1);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        // Test case: Create should return StatusCode 200 when product is successfully created
        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenProductIsSuccessfullyCreated()
        {
            // Arrange
            ProductRequest newProduct = new ProductRequest
            {
                Title = " Fancy dress",
                Price = 299.99M,
                Description = "kids dress",
                Image = "dress1.jpg",
                Stock = 10,
                CategoryId = 1

            };

            // Setup the mock ProductService to return a product response
            ProductResponse productResponse = new ProductResponse
            {
                Id = 1,
                Title = " Fancy dress",
                Price = 299.99M,
                Description = "kids dress",
                Image = "dress1.jpg",
                Stock = 10,
                CategoryId = 1

            };
            _mockProductService.Setup(x => x.CreateProduct(It.IsAny<ProductRequest>())).ReturnsAsync(productResponse);

            // Act
            var result = await _productController.Create(newProduct);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        // Similar test cases for Update and Delete follow the same structure...
        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenProductIsSuccessfullyUpdated()
        {
            // Arrange
            ProductRequest updateProduct = new ProductRequest
            {
                Title = "Fancy dress",
                Price = 299.99M,
                Description = "Kids dress",
                Image = "dress1.jpg",
                Stock = 10,
                CategoryId = 1
            };

            int productId = 1;

            // Create a sample product response for the mock ProductService
            ProductResponse productResponse = new ProductResponse
            {
                Id = 1,
                Title = "Fancy dress",
                Price = 299.99M,
                Description = "Kids dress",
                Image = "dress1.jpg",
                Stock = 10,
                CategoryId = 1
            };

            // Setup the mock ProductService to return the sample product response
            _mockProductService.Setup(x => x.UpdateProduct(It.IsAny<int>(), It.IsAny<ProductRequest>()))
                .ReturnsAsync(productResponse);

            // Act
            var result = await _productController.Update(productId, updateProduct);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        // Test case to verify that Update returns StatusCode 404 when trying to update a product that does not exist
        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenTryingToUpdateProductWhichDoesNotExist()
        {
            // Arrange
            ProductRequest updateProduct = new ProductRequest
            {
                Title = "Fancy dress",
                Price = 299.99M,
                Description = "Kids dress",
                Image = "dress1.jpg",
                Stock = 10,
                CategoryId = 1
            };

            // Mock the ProductService to return null, simulating the product not being found
            _mockProductService
                .Setup(x => x.UpdateProduct(It.IsAny<int>(), It.IsAny<ProductRequest>()))
                .ReturnsAsync(() => null);

            // Act
            int productId = 1;
            var result = await _productController.Update(productId, updateProduct);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        // Test case to verify that Update returns StatusCode 500 when an exception is raised during the update process
        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            ProductRequest updateProduct = new ProductRequest
            {
                Title = "Fancy dress",
                Price = 299.99M,
                Description = "Kids dress",
                Image = "dress1.jpg",
                Stock = 10,
                CategoryId = 1
            };

            int productId = 1;

            // Mock the ProductService to throw an exception, simulating an error during the update process
            _mockProductService
                .Setup(x => x.UpdateProduct(It.IsAny<int>(), It.IsAny<ProductRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _productController.Update(productId, updateProduct);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        // Test case to verify that Delete returns StatusCode 200 when the product is successfully deleted
        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenProductIsDeleted()
        {
            // Arrange
            int productId = 1;

            // Mock the ProductService to return a product response, simulating a successful deletion
            ProductResponse productResponse = new ProductResponse
            {
                Id = 1,
                Title = "Fancy dress",
                Price = 299.99M,
                Description = "Kids dress",
                Image = "dress1.jpg",
                Stock = 10,
                CategoryId = 1
            };
            _mockProductService
                .Setup(x => x.DeleteProduct(It.IsAny<int>()))
                .ReturnsAsync(productResponse);

            // Act
            var result = await _productController.Delete(productId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        // Test case to verify that Delete returns StatusCode 404 when trying to delete a product that does not exist
        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenTryingToDeleteProductWhichDoesNotExist()
        {
            // Arrange
            int productId = 1;

            // Mock the ProductService to return null, simulating the product not being found
            _mockProductService
                .Setup(x => x.DeleteProduct(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _productController.Delete(productId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        // Test case to verify that Delete returns StatusCode 500 when an exception is raised during the deletion process
        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int productId = 1;

            // Mock the ProductService to throw an exception, simulating an error during the deletion process
            _mockProductService
                .Setup(x => x.DeleteProduct(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _productController.Delete(productId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }



       

    }

}
