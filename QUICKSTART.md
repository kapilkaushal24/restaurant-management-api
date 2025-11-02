# 🚀 Quick Start Guide

## Prerequisites
- .NET 9 SDK installed
- SQL Server or SQL Server LocalDB

## Steps to Run

### 1. Navigate to Infrastructure project
```powershell
cd "d:\Kapil\DotNet\MyRestaurant\src\RestaurantManagement.Infrastructure"
```

### 2. Apply Database Migrations
```powershell
dotnet ef database update --startup-project ../RestaurantManagement.API/RestaurantManagement.API.csproj
```

### 3. Run the API
```powershell
cd ../RestaurantManagement.API
dotnet run
```

### 4. Open Swagger UI
Navigate to: **https://localhost:5001** or **http://localhost:5000**

## 🧪 Testing the API

### Step 1: Register a User
In Swagger, use **POST /api/auth/register**:
```json
{
  "name": "Test Admin",
  "email": "test@admin.com",
  "password": "Test@123",
  "role": "Admin"
}
```

### Step 2: Login
Use **POST /api/auth/login**:
```json
{
  "email": "test@admin.com",
  "password": "Test@123"
}
```

Copy the `token` from the response.

### Step 3: Authorize in Swagger
1. Click the **"Authorize"** button at the top
2. Enter: `Bearer {paste_your_token_here}`
3. Click **"Authorize"** then **"Close"**

### Step 4: Create a Restaurant
Use **POST /api/restaurants**:
```json
{
  "name": "The Italian Place",
  "address": "123 Main Street, New York",
  "description": "Authentic Italian cuisine with a modern twist"
}
```

### Step 5: Create Menu Category
Use **POST /api/menu/categories**:
```json
{
  "name": "Main Dishes",
  "restaurantId": 1
}
```

### Step 6: Create Menu Items
Use **POST /api/menu/items**:
```json
{
  "name": "Spaghetti Carbonara",
  "description": "Classic Roman pasta with eggs, cheese, and guanciale",
  "price": 15.99,
  "categoryId": 1
}
```

### Step 7: Register as Customer
Use **POST /api/auth/register**:
```json
{
  "name": "John Customer",
  "email": "john@customer.com",
  "password": "Customer@123",
  "role": "Customer"
}
```

### Step 8: Login as Customer
Login and authorize with the new customer token.

### Step 9: Place an Order
Use **POST /api/orders**:
```json
{
  "restaurantId": 1,
  "orderItems": [
    {
      "menuItemId": 1,
      "quantity": 2
    }
  ]
}
```

### Step 10: Check Orders
- **GET /api/orders/my-orders** - View your orders
- **GET /api/orders/{id}** - View specific order

## 🎯 Default Admin Account

A default admin account is seeded:
- **Email**: `admin@restaurant.com`
- **Password**: `Admin@123`

## 📊 Roles & Permissions

### Admin
- Full access to all endpoints
- Manage restaurants
- Manage menu categories and items
- View all orders
- Update order status

### Staff
- Manage menu categories and items
- Update order status
- View orders

### Customer
- Place orders
- View own orders
- Search restaurants and menu items

## 🐛 Troubleshooting

### Database Connection Issues
If you get connection errors:
1. Check if SQL Server is running
2. Update connection string in `appsettings.json`
3. Ensure LocalDB is installed: `sqllocaldb info`

### Migration Issues
```powershell
# Remove all migrations
dotnet ef database drop --startup-project ../RestaurantManagement.API
dotnet ef migrations remove --startup-project ../RestaurantManagement.API

# Recreate
dotnet ef migrations add InitialCreate --startup-project ../RestaurantManagement.API
dotnet ef database update --startup-project ../RestaurantManagement.API
```

### Port Already in Use
Edit `Properties/launchSettings.json` to change ports.

## 📝 Sample API Calls (PowerShell)

### Register User
```powershell
$body = @{
    name = "Test User"
    email = "test@example.com"
    password = "Test@123"
    role = "Customer"
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:5001/api/auth/register" -Method Post -Body $body -ContentType "application/json"
```

### Login
```powershell
$body = @{
    email = "test@example.com"
    password = "Test@123"
} | ConvertTo-Json

$response = Invoke-RestMethod -Uri "https://localhost:5001/api/auth/login" -Method Post -Body $body -ContentType "application/json"
$token = $response.token
```

### Get Restaurants (with auth)
```powershell
$headers = @{
    "Authorization" = "Bearer $token"
}

Invoke-RestMethod -Uri "https://localhost:5001/api/restaurants" -Method Get -Headers $headers
```

## 🎉 Success!

You now have a fully functional Restaurant Management API running!

Try exploring all the endpoints in Swagger UI.

---

**Need help?** Check the main [README.md](README.md) for detailed documentation.
