using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;
    private readonly ILogger<MenuController> _logger;

    public MenuController(IMenuService menuService, ILogger<MenuController> logger)
    {
        _menuService = menuService;
        _logger = logger;
    }

    // Categories
    [HttpGet("categories/restaurant/{restaurantId}")]
    public async Task<ActionResult<IEnumerable<MenuCategoryDto>>> GetCategoriesByRestaurant(int restaurantId)
    {
        var categories = await _menuService.GetCategoriesByRestaurantAsync(restaurantId);
        return Ok(categories);
    }

    [HttpGet("categories/{id}")]
    public async Task<ActionResult<MenuCategoryDto>> GetCategoryById(int id)
    {
        var category = await _menuService.GetCategoryByIdAsync(id);
        if (category == null)
            return NotFound();
        
        return Ok(category);
    }

    [Authorize(Roles = "Admin,Staff")]
    [HttpPost("categories")]
    public async Task<ActionResult<MenuCategoryDto>> CreateCategory([FromBody] CreateMenuCategoryDto createDto)
    {
        try
        {
            var category = await _menuService.CreateCategoryAsync(createDto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create category");
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize(Roles = "Admin,Staff")]
    [HttpDelete("categories/{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            await _menuService.DeleteCategoryAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete category");
            return BadRequest(new { message = ex.Message });
        }
    }

    // Menu Items
    [HttpGet("items/category/{categoryId}")]
    public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetItemsByCategory(int categoryId)
    {
        var items = await _menuService.GetItemsByCategoryAsync(categoryId);
        return Ok(items);
    }

    [HttpGet("items/{id}")]
    public async Task<ActionResult<MenuItemDto>> GetMenuItemById(int id)
    {
        var item = await _menuService.GetMenuItemByIdAsync(id);
        if (item == null)
            return NotFound();
        
        return Ok(item);
    }

    [HttpGet("items/search")]
    public async Task<ActionResult<IEnumerable<MenuItemDto>>> SearchMenuItems(
        [FromQuery] string searchTerm,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice)
    {
        var items = await _menuService.SearchMenuItemsAsync(searchTerm, minPrice, maxPrice);
        return Ok(items);
    }

    [Authorize(Roles = "Admin,Staff")]
    [HttpPost("items")]
    public async Task<ActionResult<MenuItemDto>> CreateMenuItem([FromBody] CreateMenuItemDto createDto)
    {
        try
        {
            var item = await _menuService.CreateMenuItemAsync(createDto);
            return CreatedAtAction(nameof(GetMenuItemById), new { id = item.Id }, item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create menu item");
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize(Roles = "Admin,Staff")]
    [HttpPut("items/{id}")]
    public async Task<ActionResult<MenuItemDto>> UpdateMenuItem(int id, [FromBody] UpdateMenuItemDto updateDto)
    {
        try
        {
            var item = await _menuService.UpdateMenuItemAsync(id, updateDto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update menu item");
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize(Roles = "Admin,Staff")]
    [HttpDelete("items/{id}")]
    public async Task<IActionResult> DeleteMenuItem(int id)
    {
        try
        {
            await _menuService.DeleteMenuItemAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete menu item");
            return BadRequest(new { message = ex.Message });
        }
    }
}
