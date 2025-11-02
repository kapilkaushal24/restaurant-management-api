using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;

public interface IRestaurantService
{
    Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync();
    Task<RestaurantDto?> GetRestaurantByIdAsync(int id);
    Task<IEnumerable<RestaurantDto>> SearchRestaurantsAsync(string searchTerm);
    Task<RestaurantDto> CreateRestaurantAsync(CreateRestaurantDto createDto);
    Task<RestaurantDto> UpdateRestaurantAsync(int id, UpdateRestaurantDto updateDto);
    Task DeleteRestaurantAsync(int id);
}
