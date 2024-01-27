using H6_ChicBotique.DTOs;
using H6_ChicBotique.Services;
using Microsoft.AspNetCore.Mvc;

namespace H6_ChicBotique.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeAddressController : ControllerBase
    {
        private readonly IHomeAddressService _HomeAddressService;
        public HomeAddressController(IHomeAddressService HomeAddressService)
        {
            _HomeAddressService = HomeAddressService;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<HomeAddressResponse> HomeAddressResponse = await _HomeAddressService.GetAllHomeAddresses();
                if (HomeAddressResponse == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }
                if (HomeAddressResponse.Count == 0)
                {
                    return NoContent();
                }
                return Ok(HomeAddressResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);

            }


        }
        // https://localhost:5001/api/HomeAddress/derp
        [HttpGet("{homeAddressId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int homeAddressId)
        {
            try
            {
                HomeAddressResponse homeAddressResponse = await _HomeAddressService.GetHomeAddressById(homeAddressId);

                if (homeAddressResponse == null)
                {
                    return NotFound();
                }

                return Ok(homeAddressResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpPut("{homeAddressId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int homeAddressId, [FromBody] HomeAddressRequest updateHomeAddress)
        {
            try
            {
                HomeAddressResponse homeResponse = await _HomeAddressService.UpdateHomeAddress(homeAddressId, updateHomeAddress);

                if (homeResponse == null)
                {
                    return NotFound();
                }

                return Ok(homeResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }







    }
}
