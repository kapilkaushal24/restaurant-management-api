using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    Task<IEnumerable<OrderDto>> GetOrdersByUserAsync(int userId);
    Task<OrderDto?> GetOrderByIdAsync(int id);
    Task<OrderDto> CreateOrderAsync(int userId, CreateOrderDto createDto);
    Task<OrderDto> UpdateOrderStatusAsync(int id, UpdateOrderStatusDto updateDto);
}
