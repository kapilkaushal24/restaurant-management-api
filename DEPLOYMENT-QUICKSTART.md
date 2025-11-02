# Quick Start: Deploy Your API in 10 Minutes

## Choose Your Platform

### 🚂 Option 1: Railway.app (FASTEST - Recommended)

```bash
# 1. Install Railway CLI
npm install -g @railway/cli

# 2. Login to Railway
railway login

# 3. Initialize project
railway init

# 4. Add environment variables
railway variables set ASPNETCORE_ENVIRONMENT=Production
railway variables set JwtSettings__Key="your-secret-key-min-32-chars"
railway variables set JwtSettings__Issuer="RestaurantAPI"
railway variables set JwtSettings__Audience="RestaurantAPIUsers"

# 5. Deploy!
railway up

# 6. Get your URL
railway open
```

**Done! Your API is live in 5 minutes!** 🎉

---

### 🎨 Option 2: Render.com

```bash
# 1. Go to https://render.com
# 2. Sign up with GitHub
# 3. Click "New +" → "Web Service"
# 4. Connect your GitHub repo
# 5. Configure:
#    - Name: restaurant-api
#    - Build Command: dotnet publish -c Release -o out
#    - Start Command: dotnet out/RestaurantManagement.API.dll
# 6. Add environment variables (see Railway example)
# 7. Click "Create Web Service"
```

**Deploy time: ~7 minutes**

---

### ✈️ Option 3: Fly.io

```bash
# 1. Install Fly CLI
# Windows PowerShell:
iwr https://fly.io/install.ps1 -useb | iex

# 2. Sign up
fly auth signup

# 3. Navigate to your project
cd D:\Kapil\DotNet\MyRestaurant\src\RestaurantManagement.API

# 4. Launch (fly.toml already created!)
fly launch --no-deploy

# 5. Set secrets
fly secrets set JwtSettings__Key="your-secret-key-min-32-chars"
fly secrets set ASPNETCORE_ENVIRONMENT=Production

# 6. Deploy
fly deploy

# 7. Open your app
fly open
```

**Deploy time: ~8 minutes**

---

## 🔐 Environment Variables You Need

Copy these for any platform:

```bash
ASPNETCORE_ENVIRONMENT=Production
JwtSettings__Key=your-super-secret-jwt-key-must-be-at-least-32-characters-long-for-security
JwtSettings__Issuer=RestaurantAPI
JwtSettings__Audience=RestaurantAPIUsers
JwtSettings__ExpirationInHours=24
```

---

## 🧪 Test Your Deployed API

Once deployed, test these endpoints:

```bash
# Replace YOUR_URL with your actual deployment URL

# 1. Health Check
curl https://YOUR_URL/health

# 2. Register SuperAdmin
curl -X POST https://YOUR_URL/api/Auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Super Admin",
    "email": "admin@restaurant.com",
    "password": "Admin@123",
    "role": "SuperAdmin"
  }'

# 3. Login
curl -X POST https://YOUR_URL/api/Auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "admin@restaurant.com",
    "password": "Admin@123"
  }'

# 4. Access Swagger
# Open in browser: https://YOUR_URL
```

---

## ✅ Success Checklist

- [ ] Platform account created
- [ ] Repository connected
- [ ] Environment variables set
- [ ] Build successful
- [ ] Deployment successful
- [ ] API accessible via URL
- [ ] Health check passing
- [ ] Swagger UI working
- [ ] Can register users
- [ ] Can login and get JWT
- [ ] Authorized endpoints working

---

## 🚨 Common Issues & Fixes

### Build Fails?
```bash
# Test locally first
dotnet restore
dotnet build -c Release
dotnet publish -c Release -o ./publish
```

### Can't Access API?
- Check if port 8080 is exposed
- Verify ASPNETCORE_URLS is set to `http://+:8080`
- Check platform logs for errors

### JWT Not Working?
- Ensure JWT key is at least 32 characters
- Check all JWT settings are configured
- Verify ASPNETCORE_ENVIRONMENT is set

### Database Issues?
- SQLite file should be in `/app/data` directory
- For production, consider PostgreSQL
- Check write permissions

---

## 📊 Platform Comparison

| Platform | Free Tier | Setup Time | Difficulty | Best For |
|----------|-----------|------------|------------|----------|
| **Railway** | $5/month credit | 5 min | Easy ⭐ | Quick deployment |
| **Render** | 750 hrs/month | 7 min | Easy ⭐ | Long-running apps |
| **Fly.io** | 3 VMs free | 8 min | Medium ⭐⭐ | Global distribution |
| **DigitalOcean** | $200 credit | 10 min | Medium ⭐⭐ | Production apps |

---

## 🎯 Next Steps

1. ✅ Deploy to your chosen platform
2. 📝 Test all endpoints
3. 🔒 Add custom domain (optional)
4. 📊 Set up monitoring
5. 🎨 Build a frontend
6. 🚀 Share your project!

---

**Need help? Check CI-CD-SETUP.md for detailed instructions!**
