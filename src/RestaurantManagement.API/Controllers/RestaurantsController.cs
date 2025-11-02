using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;
    private readonly ILogger<RestaurantsController> _logger;

    public RestaurantsController(IRestaurantService restaurantService, ILogger<RestaurantsController> logger)
    {
        _restaurantService = restaurantService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
    {
        var restaurants = await _restaurantService.GetAllRestaurantsAsync();
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantDto>> GetById(int id)
    {
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
        if (restaurant == null)
            return NotFound();
        
        return Ok(restaurant);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> Search([FromQuery] string searchTerm)
    {
        var restaurants = await _restaurantService.SearchRestaurantsAsync(searchTerm);
        return Ok(restaurants);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<RestaurantDto>> Create([FromBody] CreateRestaurantDto createDto)
    {
        try
        {
            var restaurant = await _restaurantService.CreateRestaurantAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = restaurant.Id }, restaurant);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create restaurant");
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<RestaurantDto>> Update(int id, [FromBody] UpdateRestaurantDto updateDto)
    {
        try
        {
            var restaurant = await _restaurantService.UpdateRestaurantAsync(id, updateDto);
            return Ok(restaurant);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update restaurant");
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _restaurantService.DeleteRestaurantAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete restaurant");
            return BadRequest(new { message = ex.Message });
        }
    }
}
