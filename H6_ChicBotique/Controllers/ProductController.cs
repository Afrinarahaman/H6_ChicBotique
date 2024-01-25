﻿using H6_ChicBotique.DTOs;
using Microsoft.AspNetCore.Mvc;
using H6_ChicBotique.Services;

namespace H6_ChicBotique.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            // Get all products
            try
            {
                List<ProductResponse> productResponses = await _productService.GetAllProducts();

                if (productResponses == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (productResponses.Count == 0)
                {
                    return NoContent();
                }

                return Ok(productResponses);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int productId)
        {
            // Get a product by ID
            try
            {
                ProductResponse productResponse = await _productService.GetProductById(productId);

                if (productResponse == null)
                {
                    return NotFound();
                }

                return Ok(productResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("Category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsByCategoryId([FromRoute] int categoryId)
        {
            // Get products by category ID
            try
            {
                List<ProductResponse> productResponse = await _productService.GetProductsByCategoryId(categoryId);

                if (productResponse == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (productResponse.Count == 0)
                {
                    return NoContent();
                }

                return Ok(productResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ProductRequest newProduct)
        {
            // Create a new product
            try
            {
                ProductResponse productResponse = await _productService.CreateProduct(newProduct);

                if (productResponse == null)
                {
                    return NotFound();
                }

                return Ok(productResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int productId, [FromBody] ProductRequest updateProduct)
        {
            // Update an existing product
            try
            {
                ProductResponse productResponse = await _productService.UpdateProduct(productId, updateProduct);

                if (productResponse == null)
                {
                    return NotFound();
                }

                return Ok(productResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int productId)
        {
            // Delete a product by ID
            try
            {
                ProductResponse productResponse = await _productService.DeleteProduct(productId);

                if (productResponse == null)
                {
                    return NotFound();
                }

                return Ok(productResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
