using AutoMapper;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Enums;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.Application.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<OrderItem> _orderItemRepository;
    private readonly IRepository<MenuItem> _menuItemRepository;
    private readonly IMapper _mapper;

    public OrderService(
        IRepository<Order> orderRepository,
        IRepository<OrderItem> orderItemRepository,
        IRepository<MenuItem> menuItemRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _menuItemRepository = menuItemRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        var orderDtos = new List<OrderDto>();

        foreach (var order in orders)
        {
            var orderDto = await GetOrderByIdAsync(order.Id);
            if (orderDto != null)
                orderDtos.Add(orderDto);
        }
        
        return orderDtos;
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByUserAsync(int userId)
    {
        var orders = await _orderRepository.FindAsync(o => o.UserId == userId);
        var orderDtos = new List<OrderDto>();

        foreach (var order in orders)
        {
            var orderDto = await GetOrderByIdAsync(order.Id);
            if (orderDto != null)
                orderDtos.Add(orderDto);
        }
        
        return orderDtos;
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            return null;

        var orderItems = await _orderItemRepository.FindAsync(oi => oi.OrderId == id);
        var orderItemDtos = new List<OrderItemDto>();

        foreach (var item in orderItems)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(item.MenuItemId);
            orderItemDtos.Add(new OrderItemDto
            {
                Id = item.Id,
                MenuItemId = item.MenuItemId,
                MenuItemName = menuItem?.Name ?? "Unknown",
                Quantity = item.Quantity,
                Price = item.Price
            });
        }

        var orderDto = _mapper.Map<OrderDto>(order);
        orderDto.OrderItems = orderItemDtos;
        
        return orderDto;
    }

    public async Task<OrderDto> CreateOrderAsync(int userId, CreateOrderDto createDto)
    {
        var order = new Order
        {
            UserId = userId,
            RestaurantId = createDto.RestaurantId,
            Status = OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        decimal totalAmount = 0;
        await _orderRepository.AddAsync(order);

        foreach (var itemDto in createDto.OrderItems)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(itemDto.MenuItemId);
            if (menuItem == null)
                throw new Exception($"Menu item with ID {itemDto.MenuItemId} not found");

            var orderItem = new OrderItem
            {
                OrderId = order.Id,
                MenuItemId = itemDto.MenuItemId,
                Quantity = itemDto.Quantity,
                Price = menuItem.Price,
                CreatedAt = DateTime.UtcNow
            };

            await _orderItemRepository.AddAsync(orderItem);
            totalAmount += menuItem.Price * itemDto.Quantity;
        }

        order.TotalAmount = totalAmount;
        await _orderRepository.UpdateAsync(order);

        return await GetOrderByIdAsync(order.Id) ?? throw new Exception("Failed to retrieve created order");
    }

    public async Task<OrderDto> UpdateOrderStatusAsync(int id, UpdateOrderStatusDto updateDto)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            throw new Exception("Order not found");

        if (!Enum.TryParse<OrderStatus>(updateDto.Status, true, out var status))
            throw new Exception("Invalid order status");

        order.Status = status;
        order.UpdatedAt = DateTime.UtcNow;
        
        await _orderRepository.UpdateAsync(order);
        return await GetOrderByIdAsync(order.Id) ?? throw new Exception("Failed to retrieve updated order");
    }
}
