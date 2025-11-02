# SuperAdmin Role and Get All Users Feature

## Overview
Added a new **SuperAdmin** role with exclusive access to view all users in the system.

## Changes Made

### 1. New Role - SuperAdmin
- **Location**: `RestaurantManagement.Domain\Enums\UserRole.cs`
- **Role Hierarchy**:
  - Customer (0)
  - Staff (1)
  - Admin (2)
  - **SuperAdmin (3)** ← New

### 2. SuperAdmin Seed Data
- **Default SuperAdmin Account**:
  - ⚠️ **Important**: SuperAdmin user must be registered manually
  - There is NO pre-seeded SuperAdmin account
  - You need to register one using the registration endpoint first

#### How to Create SuperAdmin Account:
Use the registration endpoint with SuperAdmin role:

```http
POST http://localhost:5150/api/Auth/register
Content-Type: application/json

{
  "name": "Super Administrator",
  "email": "superadmin@restaurant.com",
  "password": "SuperAdmin@123",
  "role": "SuperAdmin"
}
```

**Response:**
```json
{
  "userId": 1,
  "name": "Super Administrator",
  "email": "superadmin@restaurant.com",
  "role": "SuperAdmin",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "..."
}
```

After registration, you can use the login endpoint as shown below.

### 3. New API Endpoint - Get All Users
- **Endpoint**: `GET /api/Auth/users`
- **Authorization**: **SuperAdmin only**
- **Response**: List of all users with their details

#### Response Format:
```json
[
  {
    "id": 1,
    "name": "John Doe",
    "email": "john@example.com",
    "role": "Customer",
    "createdAt": "2024-11-02T10:30:00Z"
  },
  {
    "id": 2,
    "name": "Jane Smith",
    "email": "jane@example.com",
    "role": "Staff",
    "createdAt": "2024-11-02T11:15:00Z"
  }
]
```

## How to Use

### Step 0: Register SuperAdmin (First Time Only)
**Important**: You must register a SuperAdmin user first before using the features.

```http
POST http://localhost:5150/api/Auth/register
Content-Type: application/json

{
  "name": "Super Administrator",
  "email": "superadmin@restaurant.com",
  "password": "SuperAdmin@123",
  "role": "SuperAdmin"
}
```

### Step 1: Login as SuperAdmin
```http
POST http://localhost:5150/api/Auth/login
Content-Type: application/json

{
  "email": "superadmin@restaurant.com",
  "password": "SuperAdmin@123"
}
```

**Response:**
```json
{
  "userId": 999,
  "name": "Super Administrator",
  "email": "superadmin@restaurant.com",
  "role": "SuperAdmin",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "..."
}
```

### Step 2: Get All Users
```http
GET http://localhost:5150/api/Auth/users
Authorization: Bearer {token-from-step-1}
```

**Response:**
```json
[
  {
    "id": 999,
    "name": "Super Administrator",
    "email": "superadmin@restaurant.com",
    "role": "SuperAdmin",
    "createdAt": "2024-01-01T00:00:00Z"
  },
  {
    "id": 2,
    "name": "Priya Mehta",
    "email": "priya.mehta@example.com",
    "role": "Customer",
    "createdAt": "2024-11-02T11:50:00Z"
  }
  // ... more users
]
```

## Testing in Swagger

1. **Open Swagger UI**: http://localhost:5150

2. **Register SuperAdmin (First Time Only)**:
   - Expand `POST /api/Auth/register`
   - Click "Try it out"
   - Enter:
     ```json
     {
       "name": "Super Administrator",
       "email": "superadmin@restaurant.com",
       "password": "SuperAdmin@123",
       "role": "SuperAdmin"
     }
     ```
   - Click "Execute"
   - Copy the `token` from response (you can skip login and use this token directly)

3. **OR Login as SuperAdmin** (if already registered):
   - Expand `POST /api/Auth/login`
   - Click "Try it out"
   - Enter credentials:
     ```json
     {
       "email": "superadmin@restaurant.com",
       "password": "SuperAdmin@123"
     }
     ```
   - Copy the `token` from response

4. **Authorize in Swagger**:
   - Click the **"Authorize"** button (🔒) at the top
   - Enter: `Bearer {your-token}`
   - Click "Authorize"

5. **Get All Users**:
   - Expand `GET /api/Auth/users`
   - Click "Try it out"
   - Click "Execute"
   - View all users in the response

## Security Features

✅ **Role-Based Authorization**: Only SuperAdmin can access the endpoint
✅ **JWT Authentication**: Requires valid token
✅ **403 Forbidden**: Non-SuperAdmin users will receive 403 error
✅ **401 Unauthorized**: Requests without token will receive 401 error

## Error Responses

### Without Token:
```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.2",
  "title": "Unauthorized",
  "status": 401
}
```

### With Non-SuperAdmin Token:
```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.4",
  "title": "Forbidden",
  "status": 403
}
```

## Technical Implementation

### Files Modified:
1. **Domain Layer**:
   - `UserRole.cs` - Added SuperAdmin enum value

2. **Application Layer**:
   - `IAuthService.cs` - Added `GetAllUsersAsync()` method
   - `AuthService.cs` - Implemented `GetAllUsersAsync()`
   - `AuthDtos.cs` - Added `UserDto` class
   - `MappingProfile.cs` - Added User → UserDto mapping

3. **Infrastructure Layer**:
   - `ApplicationDbContext.cs` - Removed seed data (register manually instead)
   - Migration: `20251102115752_RemoveSuperAdminSeed`

4. **API Layer**:
   - `AuthController.cs` - Added `GetAllUsers()` endpoint with `[Authorize(Roles = "SuperAdmin")]`

## Database Migration Applied
```bash
Migrations:
1. 20251102115329_AddSuperAdminRoleAndSeedData - Added SuperAdmin enum value (3) with seed
2. 20251102115752_RemoveSuperAdminSeed - Removed seed data (register manually instead)
```

## Notes
- SuperAdmin has the highest privilege level in the system
- Only SuperAdmins can view all users
- **You must register a SuperAdmin user manually** - there is no default seeded account
- SuperAdmin can also register new users using the existing registration endpoints
- Use role "SuperAdmin" (case-sensitive) when registering

## Why No Seed Data?
The BCrypt password hashing generates different salts each time, making it impossible to pre-seed a working password hash in migrations. Therefore, you must register the SuperAdmin account through the API after the application starts.
