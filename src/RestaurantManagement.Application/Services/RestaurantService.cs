using AutoMapper;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.Application.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IRepository<Restaurant> _restaurantRepository;
    private readonly IMapper _mapper;

    public RestaurantService(IRepository<Restaurant> restaurantRepository, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync()
    {
        var restaurants = await _restaurantRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
    }

    public async Task<RestaurantDto?> GetRestaurantByIdAsync(int id)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id);
        return restaurant == null ? null : _mapper.Map<RestaurantDto>(restaurant);
    }

    public async Task<IEnumerable<RestaurantDto>> SearchRestaurantsAsync(string searchTerm)
    {
        var restaurants = await _restaurantRepository.FindAsync(r => 
            r.Name.Contains(searchTerm) || r.Address.Contains(searchTerm));
        return _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
    }

    public async Task<RestaurantDto> CreateRestaurantAsync(CreateRestaurantDto createDto)
    {
        var restaurant = _mapper.Map<Restaurant>(createDto);
        restaurant.Rating = 0;
        restaurant.CreatedAt = DateTime.UtcNow;
        
        await _restaurantRepository.AddAsync(restaurant);
        return _mapper.Map<RestaurantDto>(restaurant);
    }

    public async Task<RestaurantDto> UpdateRestaurantAsync(int id, UpdateRestaurantDto updateDto)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id);
        if (restaurant == null)
            throw new Exception("Restaurant not found");

        _mapper.Map(updateDto, restaurant);
        restaurant.UpdatedAt = DateTime.UtcNow;
        
        await _restaurantRepository.UpdateAsync(restaurant);
        return _mapper.Map<RestaurantDto>(restaurant);
    }

    public async Task DeleteRestaurantAsync(int id)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id);
        if (restaurant == null)
            throw new Exception("Restaurant not found");

        await _restaurantRepository.DeleteAsync(restaurant);
    }
}
