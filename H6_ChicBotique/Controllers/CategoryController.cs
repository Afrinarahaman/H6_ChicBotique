﻿using H6_ChicBotique.DTOs;
using H6_ChicBotique.Services;
using Microsoft.AspNetCore.Mvc;

namespace H6_ChicBotique.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService; //Creating an instance of ICategoryService

        public CategoryController(ICategoryService categoryService) ////Dependency injection of AccountInforService
        {
            _categoryService = categoryService;
        }

        // GET: api/Category
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //For getting all categoties details with products details
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<CategoryResponse> categoryResponse = await _categoryService.GetAllCategories(); //getting all categories details
                                                                                                     //from the Categoryservice

                if (categoryResponse == null) 
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (categoryResponse.Count == 0)
                {
                    return NoContent();
                }

                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET: api/Category/{categoryId}  
        [HttpGet("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //Get one specific entity's details by categoryId
        public async Task<IActionResult> GetById([FromRoute] int categoryId)
        {
            try
            {
                CategoryResponse categoryResponse = await _categoryService.GetCategoryById(categoryId);

                if (categoryResponse == null)
                {
                    return NotFound();
                }

                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET: api/Category/WithoutProducts
        [HttpGet("WithoutProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //For getting all categoties details only ( without products details)
        public async Task<IActionResult> GetAllCategoriesWithoutProducts()
        {
            try
            {
                List<CategoryResponse> categoryResponse = await _categoryService.GetAllCategoriesWithoutProducts();

                if (categoryResponse == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (categoryResponse.Count == 0)
                {
                    return NoContent();
                }

                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // POST: api/Category
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //Creating Category by Admin with the CategoryRequest
        public async Task<IActionResult> Create([FromBody] CategoryRequest newCategory)
        {
            try
            {
                CategoryResponse categoryResponse = await _categoryService.CreateCategory(newCategory); //calling CreateCategory method
                                                                                                        //by the instance of categoryservice

                if (categoryResponse == null)
                {
                    return Problem("Category was not created, something went wrong");
                }

                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // PUT: api/Category/{categoryId}
        [HttpPut("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //updating category info by categoryrequest and categoryId
        public async Task<IActionResult> Update([FromRoute] int categoryId, [FromBody] CategoryRequest updateCategory)
        {
            try
            {
                //calling updating method of categoryservice
                CategoryResponse categoryResponse = await _categoryService.UpdateCategory(categoryId, updateCategory);

                if (categoryResponse == null)
                {
                    return NotFound();
                }

                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE: api/Category/{categoryId}
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int categoryId)
        {
            try
            {
                CategoryResponse categoryResponse = await _categoryService.DeleteCategory(categoryId);

                if (categoryResponse == null)
                {
                    return NotFound();
                }

                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }

}
