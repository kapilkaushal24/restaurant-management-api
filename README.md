# 🍽️ Restaurant Management API

A complete **Restaurant Management System** built with **ASP.NET Core 9**, **Entity Framework Core 9**, and **SQL Server** using **Clean Architecture** principles.

## 🏗️ Architecture

The project follows **Clean Architecture** with clear separation of concerns:

```
RestaurantManagement/
├── src/
│   ├── RestaurantManagement.API          # Presentation Layer (Controllers, Middleware)
│   ├── RestaurantManagement.Application  # Business Logic (Services, DTOs, Interfaces)
│   ├── RestaurantManagement.Domain       # Domain Layer (Entities, Enums, Interfaces)
│   └── RestaurantManagement.Infrastructure  # Data Access (DbContext, Repositories)
```

## ✨ Features

### 🔐 Authentication & Authorization
- **JWT-based authentication** with refresh tokens
- **Role-based access control** (Admin, Staff, Customer)
- Secure password hashing with BCrypt
- Token expiration and refresh mechanism

### 🏪 Restaurant Management
- CRUD operations for restaurants (Admin only)
- Search restaurants by name or location
- Rating system

### 📋 Menu Management
- Menu categories and items
- CRUD operations (Admin/Staff)
- Search and filter menu items by:
  - Name or description
  - Price range
  - Category

### 🛒 Order Management
- Customers can place orders
- Staff/Admin can update order status
- Order statuses: Pending, Preparing, Delivered, Cancelled
- Order history tracking
- Real-time total calculation

### 🛠️ Technical Features
- **Global exception handling** middleware
- **Logging** with structured output
- **Swagger UI** with JWT authentication support
- **AutoMapper** for object mapping
- **Repository pattern** for data access
- **Dependency Injection** throughout

## 🗄️ Database Schema

### Entities

1. **User**
   - Id, Name, Email, PasswordHash, Role
   - CreatedAt, UpdatedAt

2. **Restaurant**
   - Id, Name, Address, Description, Rating
   - CreatedAt, UpdatedAt

3. **MenuCategory**
   - Id, Name, RestaurantId (FK)
   - CreatedAt, UpdatedAt

4. **MenuItem**
   - Id, Name, Description, Price, CategoryId (FK)
   - CreatedAt, UpdatedAt

5. **Order**
   - Id, UserId (FK), RestaurantId (FK), TotalAmount, Status
   - CreatedAt, UpdatedAt

6. **OrderItem**
   - Id, OrderId (FK), MenuItemId (FK), Quantity, Price
   - CreatedAt

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or LocalDB
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Installation

1. **Clone the repository**
   ```bash
   cd MyRestaurant
   ```

2. **Update connection string**
   
   Edit `appsettings.json` in `RestaurantManagement.API` project:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=RestaurantManagementDb;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true"
     }
   }
   ```

3. **Apply database migrations**
   ```bash
   cd src/RestaurantManagement.Infrastructure
   dotnet ef database update --startup-project ../RestaurantManagement.API/RestaurantManagement.API.csproj
   ```

4. **Run the application**
   ```bash
   cd ../RestaurantManagement.API
   dotnet run
   ```

5. **Open Swagger UI**
   
   Navigate to: `https://localhost:5001` or `http://localhost:5000`

## 📡 API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login and get JWT token
- `POST /api/auth/refresh-token` - Refresh expired token

### Restaurants
- `GET /api/restaurants` - Get all restaurants
- `GET /api/restaurants/{id}` - Get restaurant by ID
- `GET /api/restaurants/search?searchTerm={term}` - Search restaurants
- `POST /api/restaurants` - Create restaurant (Admin only)
- `PUT /api/restaurants/{id}` - Update restaurant (Admin only)
- `DELETE /api/restaurants/{id}` - Delete restaurant (Admin only)

### Menu
- `GET /api/menu/categories/restaurant/{restaurantId}` - Get categories by restaurant
- `GET /api/menu/categories/{id}` - Get category by ID
- `POST /api/menu/categories` - Create category (Admin/Staff)
- `DELETE /api/menu/categories/{id}` - Delete category (Admin/Staff)
- `GET /api/menu/items/category/{categoryId}` - Get items by category
- `GET /api/menu/items/{id}` - Get menu item by ID
- `GET /api/menu/items/search` - Search menu items with filters
- `POST /api/menu/items` - Create menu item (Admin/Staff)
- `PUT /api/menu/items/{id}` - Update menu item (Admin/Staff)
- `DELETE /api/menu/items/{id}` - Delete menu item (Admin/Staff)

### Orders
- `GET /api/orders` - Get all orders (Admin only)
- `GET /api/orders/my-orders` - Get current user's orders
- `GET /api/orders/{id}` - Get order by ID
- `POST /api/orders` - Create order (Customer)
- `PATCH /api/orders/{id}/status` - Update order status (Admin/Staff)

## 🔑 Authentication Flow

1. **Register** a new user:
   ```json
   POST /api/auth/register
   {
     "name": "John Doe",
     "email": "john@example.com",
     "password": "SecurePassword123!",
     "role": "Customer"
   }
   ```

2. **Login** to get JWT token:
   ```json
   POST /api/auth/login
   {
     "email": "john@example.com",
     "password": "SecurePassword123!"
   }
   ```

3. **Use the token** in Swagger:
   - Click "Authorize" button in Swagger UI
   - Enter: `Bearer {your_token_here}`
   - Now you can access protected endpoints

## 🧪 Testing with Swagger

1. **Create an Admin user** (for testing):
   ```json
   POST /api/auth/register
   {
     "name": "Admin User",
     "email": "admin@restaurant.com",
     "password": "Admin@123",
     "role": "Admin"
   }
   ```

2. **Login as Admin** and copy the token

3. **Click "Authorize"** in Swagger and paste the token

4. **Try creating a restaurant**:
   ```json
   POST /api/restaurants
   {
     "name": "Italian Bistro",
     "address": "123 Main St, City",
     "description": "Authentic Italian cuisine"
   }
   ```

## 🛡️ Security Features

- ✅ Password hashing with BCrypt
- ✅ JWT token-based authentication
- ✅ Role-based authorization
- ✅ Secure token storage
- ✅ HTTPS enforcement
- ✅ CORS configuration
- ✅ SQL injection prevention (EF Core parameterized queries)

## 📦 Technologies Used

- **ASP.NET Core 9** - Web API framework
- **Entity Framework Core 9** - ORM
- **SQL Server** - Database
- **AutoMapper** - Object mapping
- **BCrypt.Net** - Password hashing
- **JWT Bearer** - Authentication
- **Swashbuckle** - Swagger/OpenAPI documentation
- **Serilog** (optional) - Structured logging

## 📁 Project Structure

### Domain Layer
Contains business entities and domain logic:
- Entities (User, Restaurant, MenuCategory, MenuItem, Order, OrderItem)
- Enums (UserRole, OrderStatus)
- Repository interfaces

### Application Layer
Contains business logic and use cases:
- DTOs (Data Transfer Objects)
- Service interfaces and implementations
- AutoMapper profiles
- Business rules and validations

### Infrastructure Layer
Contains data access and external services:
- DbContext
- Repository implementations
- JWT service
- Database migrations

### Presentation Layer (API)
Contains API controllers and middleware:
- Controllers (Auth, Restaurants, Menu, Orders)
- Exception handling middleware
- Program.cs configuration

## 🔧 Configuration

### JWT Settings (appsettings.json)
```json
{
  "Jwt": {
    "Key": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!@#",
    "Issuer": "RestaurantManagementAPI",
    "Audience": "RestaurantManagementClient",
    "ExpirationHours": 24
  }
}
```

**⚠️ Important**: Change the JWT secret key in production!

## 🐛 Troubleshooting

### Migration Issues
```bash
# Remove last migration
dotnet ef migrations remove --startup-project ../RestaurantManagement.API

# Create new migration
dotnet ef migrations add MigrationName --startup-project ../RestaurantManagement.API

# Update database
dotnet ef database update --startup-project ../RestaurantManagement.API
```

### Connection String Issues
- Ensure SQL Server is running
- Verify connection string in `appsettings.json`
- Check if database exists: `SELECT name FROM sys.databases;`

## 📈 Future Enhancements

- [ ] Add payment integration
- [ ] Implement real-time order tracking with SignalR
- [ ] Add email notifications
- [ ] Implement caching with Redis
- [ ] Add unit and integration tests
- [ ] Implement rate limiting
- [ ] Add API versioning
- [ ] Implement audit logging
- [ ] Add image upload for restaurants and menu items
- [ ] Implement reviews and ratings system

## 👨‍💻 Development

### Add New Entity

1. Create entity in `Domain/Entities`
2. Add DbSet to `ApplicationDbContext`
3. Create DTOs in `Application/DTOs`
4. Add mappings to `MappingProfile`
5. Create service interface and implementation
6. Create controller
7. Run migration:
   ```bash
   dotnet ef migrations add AddNewEntity --startup-project ../RestaurantManagement.API
   ```

## 📝 License

This project is open source and available for educational purposes.

## 🤝 Contributing

Contributions, issues, and feature requests are welcome!

## 📧 Contact

For questions or support, please open an issue in the repository.

---

**Built with ❤️ using .NET 9 and Clean Architecture**
