using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;

public interface IMenuService
{
    Task<IEnumerable<MenuCategoryDto>> GetCategoriesByRestaurantAsync(int restaurantId);
    Task<MenuCategoryDto?> GetCategoryByIdAsync(int id);
    Task<MenuCategoryDto> CreateCategoryAsync(CreateMenuCategoryDto createDto);
    Task DeleteCategoryAsync(int id);
    
    Task<IEnumerable<MenuItemDto>> GetItemsByCategoryAsync(int categoryId);
    Task<MenuItemDto?> GetMenuItemByIdAsync(int id);
    Task<IEnumerable<MenuItemDto>> SearchMenuItemsAsync(string searchTerm, decimal? minPrice, decimal? maxPrice);
    Task<MenuItemDto> CreateMenuItemAsync(CreateMenuItemDto createDto);
    Task<MenuItemDto> UpdateMenuItemAsync(int id, UpdateMenuItemDto updateDto);
    Task DeleteMenuItemAsync(int id);
}
