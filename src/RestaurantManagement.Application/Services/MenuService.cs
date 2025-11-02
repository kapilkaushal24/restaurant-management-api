using AutoMapper;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.Application.Services;

public class MenuService : IMenuService
{
    private readonly IRepository<MenuCategory> _categoryRepository;
    private readonly IRepository<MenuItem> _menuItemRepository;
    private readonly IMapper _mapper;

    public MenuService(
        IRepository<MenuCategory> categoryRepository,
        IRepository<MenuItem> menuItemRepository,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _menuItemRepository = menuItemRepository;
        _mapper = mapper;
    }

    // Category methods
    public async Task<IEnumerable<MenuCategoryDto>> GetCategoriesByRestaurantAsync(int restaurantId)
    {
        var categories = await _categoryRepository.FindAsync(c => c.RestaurantId == restaurantId);
        return _mapper.Map<IEnumerable<MenuCategoryDto>>(categories);
    }

    public async Task<MenuCategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        return category == null ? null : _mapper.Map<MenuCategoryDto>(category);
    }

    public async Task<MenuCategoryDto> CreateCategoryAsync(CreateMenuCategoryDto createDto)
    {
        var category = _mapper.Map<MenuCategory>(createDto);
        category.CreatedAt = DateTime.UtcNow;
        
        await _categoryRepository.AddAsync(category);
        return _mapper.Map<MenuCategoryDto>(category);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
            throw new Exception("Category not found");

        await _categoryRepository.DeleteAsync(category);
    }

    // MenuItem methods
    public async Task<IEnumerable<MenuItemDto>> GetItemsByCategoryAsync(int categoryId)
    {
        var items = await _menuItemRepository.FindAsync(m => m.CategoryId == categoryId);
        return _mapper.Map<IEnumerable<MenuItemDto>>(items);
    }

    public async Task<MenuItemDto?> GetMenuItemByIdAsync(int id)
    {
        var item = await _menuItemRepository.GetByIdAsync(id);
        return item == null ? null : _mapper.Map<MenuItemDto>(item);
    }

    public async Task<IEnumerable<MenuItemDto>> SearchMenuItemsAsync(string searchTerm, decimal? minPrice, decimal? maxPrice)
    {
        var items = await _menuItemRepository.FindAsync(m => 
            m.Name.Contains(searchTerm) || m.Description.Contains(searchTerm));

        if (minPrice.HasValue)
            items = items.Where(m => m.Price >= minPrice.Value);
        
        if (maxPrice.HasValue)
            items = items.Where(m => m.Price <= maxPrice.Value);

        return _mapper.Map<IEnumerable<MenuItemDto>>(items);
    }

    public async Task<MenuItemDto> CreateMenuItemAsync(CreateMenuItemDto createDto)
    {
        var menuItem = _mapper.Map<MenuItem>(createDto);
        menuItem.CreatedAt = DateTime.UtcNow;
        
        await _menuItemRepository.AddAsync(menuItem);
        return _mapper.Map<MenuItemDto>(menuItem);
    }

    public async Task<MenuItemDto> UpdateMenuItemAsync(int id, UpdateMenuItemDto updateDto)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(id);
        if (menuItem == null)
            throw new Exception("Menu item not found");

        _mapper.Map(updateDto, menuItem);
        menuItem.UpdatedAt = DateTime.UtcNow;
        
        await _menuItemRepository.UpdateAsync(menuItem);
        return _mapper.Map<MenuItemDto>(menuItem);
    }

    public async Task DeleteMenuItemAsync(int id)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(id);
        if (menuItem == null)
            throw new Exception("Menu item not found");

        await _menuItemRepository.DeleteAsync(menuItem);
    }
}
