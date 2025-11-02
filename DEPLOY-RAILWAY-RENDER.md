# 🚂 Deploy to Railway.app - Step by Step Guide

## Part 1: Prepare Your Project (5 minutes)

### Step 1: Initialize Git Repository

```powershell
# Navigate to your project root
cd D:\Kapil\DotNet\MyRestaurant

# Initialize git (if not already done)
git init

# Check status
git status

# Add all files
git add .

# Commit
git commit -m "Initial commit: Restaurant Management API with CI/CD"
```

### Step 2: Create GitHub Repository

1. Go to https://github.com/new
2. Repository name: `restaurant-management-api`
3. Description: `ASP.NET Core 9 Restaurant Management API`
4. **Public** or **Private** (your choice)
5. **DO NOT** initialize with README (we have files)
6. Click **"Create repository"**

### Step 3: Push to GitHub

```powershell
# Add remote (replace YOUR_USERNAME with your GitHub username)
git remote add origin https://github.com/YOUR_USERNAME/restaurant-management-api.git

# Push to GitHub
git branch -M main
git push -u origin main
```

---

## Part 2: Deploy to Railway.app (10 minutes)

### Step 1: Sign Up for Railway

1. Go to https://railway.app
2. Click **"Login"** or **"Start a New Project"**
3. Sign in with **GitHub** (easiest option)
4. Authorize Railway to access your GitHub account

### Step 2: Create New Project

1. Click **"New Project"**
2. Select **"Deploy from GitHub repo"**
3. Choose **"restaurant-management-api"** from the list
4. Railway will automatically detect it's a .NET project

### Step 3: Configure Environment Variables

1. In your Railway project, click on your service
2. Go to **"Variables"** tab
3. Click **"+ New Variable"** and add these:

```
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://0.0.0.0:8080
JwtSettings__Key=your-super-secret-jwt-key-must-be-at-least-32-characters-long-change-this-in-production
JwtSettings__Issuer=RestaurantAPI
JwtSettings__Audience=RestaurantAPIUsers
JwtSettings__ExpirationInHours=24
```

**Important**: Click **"Add"** after each variable!

### Step 4: Configure Build Settings (if needed)

Railway usually auto-detects, but if it doesn't:

1. Go to **"Settings"** tab
2. **Build Command**: `dotnet publish src/RestaurantManagement.API/RestaurantManagement.API.csproj -c Release -o out`
3. **Start Command**: `dotnet out/RestaurantManagement.API.dll`
4. **Port**: Leave as default (Railway auto-assigns)

### Step 5: Deploy!

1. Railway will automatically start deploying
2. Watch the **"Deployments"** tab for progress
3. Wait 3-5 minutes for build and deployment
4. Once done, you'll see a green **"Active"** status

### Step 6: Get Your Public URL

1. Go to **"Settings"** tab
2. Scroll to **"Networking"**
3. Click **"Generate Domain"**
4. You'll get a URL like: `https://restaurant-api-production-xxxx.up.railway.app`
5. Copy this URL!

### Step 7: Test Your Deployment

```powershell
# Replace YOUR_RAILWAY_URL with your actual URL

# 1. Test health endpoint
curl https://YOUR_RAILWAY_URL/health

# 2. Test Swagger UI
# Open in browser: https://YOUR_RAILWAY_URL

# 3. Register a SuperAdmin
curl -X POST https://YOUR_RAILWAY_URL/api/Auth/register `
  -H "Content-Type: application/json" `
  -d '{
    "name": "Super Admin",
    "email": "admin@restaurant.com",
    "password": "Admin@123",
    "role": "SuperAdmin"
  }'
```

---

## Part 3: If Railway Doesn't Work - Use Render.com (Alternative)

### Step 1: Sign Up for Render

1. Go to https://render.com
2. Click **"Get Started"**
3. Sign up with **GitHub**
4. Authorize Render

### Step 2: Create New Web Service

1. Click **"New +"** → **"Web Service"**
2. Connect your GitHub repo: **"restaurant-management-api"**
3. Click **"Connect"**

### Step 3: Configure Service

Fill in these settings:

**Basic Settings:**
- **Name**: `restaurant-api`
- **Region**: Choose closest to you
- **Branch**: `main`
- **Root Directory**: Leave empty
- **Runtime**: Docker

**Build & Deploy:**
- **Dockerfile Path**: `./Dockerfile`

**Or if you want to use Native Runtime:**
- **Build Command**: 
  ```
  dotnet restore && dotnet publish src/RestaurantManagement.API/RestaurantManagement.API.csproj -c Release -o out
  ```
- **Start Command**:
  ```
  dotnet out/RestaurantManagement.API.dll
  ```

### Step 4: Add Environment Variables

Scroll down to **"Environment Variables"**, click **"Add Environment Variable"**:

```
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://0.0.0.0:10000
PORT=10000
JwtSettings__Key=your-super-secret-jwt-key-must-be-at-least-32-characters-long-change-this
JwtSettings__Issuer=RestaurantAPI
JwtSettings__Audience=RestaurantAPIUsers
JwtSettings__ExpirationInHours=24
```

### Step 5: Deploy

1. Scroll to bottom and click **"Create Web Service"**
2. Render will start building (5-10 minutes)
3. Watch the logs in real-time
4. Once complete, you'll see **"Live"** status

### Step 6: Get Your URL

- Your URL will be: `https://restaurant-api.onrender.com`
- Test it the same way as Railway

---

## 🎯 Quick Comparison

| Feature | Railway | Render |
|---------|---------|--------|
| **Free Tier** | $5/month credit | 750 hours/month |
| **Deploy Time** | 3-5 minutes | 5-10 minutes |
| **Auto-Deploy** | ✅ Yes | ✅ Yes |
| **Custom Domain** | ✅ Free | ✅ Free |
| **Database** | ✅ Add-on | ✅ Free PostgreSQL |
| **Ease** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ |

---

## ✅ Post-Deployment Checklist

After deployment on either platform:

- [ ] Public URL is accessible
- [ ] Swagger UI loads at `https://YOUR_URL/`
- [ ] Health endpoint works: `https://YOUR_URL/health`
- [ ] Can register a SuperAdmin user
- [ ] Can login and receive JWT token
- [ ] Can access protected endpoints with token
- [ ] All CRUD operations work
- [ ] Database persists data

---

## 🚨 Troubleshooting

### Railway Issues:

**Build fails?**
```powershell
# Test locally first
cd D:\Kapil\DotNet\MyRestaurant
dotnet restore
dotnet build -c Release
dotnet publish src/RestaurantManagement.API/RestaurantManagement.API.csproj -c Release -o out
dotnet out/RestaurantManagement.API.dll
```

**Can't access URL?**
- Check Railway logs in "Deployments" tab
- Verify environment variables are set
- Make sure domain is generated
- Check if service is "Active"

**Database issues?**
- Railway supports persistent volumes
- Go to "Data" tab to add PostgreSQL if needed
- For now, SQLite will work but data resets on redeploy

### Render Issues:

**Build timing out?**
- Render free tier has 15 min build limit
- Try using Docker instead of native build
- Check Dockerfile is correct

**503 Service Unavailable?**
- Service might be sleeping (free tier)
- First request takes 30-60 seconds to wake up
- This is normal for free tier

**Environment variables not working?**
- Make sure you clicked "Add" for each variable
- No quotes needed around values
- Use double underscore `__` for nested config

---

## 🎉 Success! What's Next?

Once deployed:

1. **Update README.md** with your live API URL
2. **Test all endpoints** in production
3. **Set up monitoring** (UptimeRobot - free)
4. **Add custom domain** (optional)
5. **Set up CI/CD** with GitHub Actions (already configured!)
6. **Build a frontend** to consume your API

---

## 📊 Expected Timeline

| Task | Railway | Render |
|------|---------|--------|
| Sign up | 2 min | 2 min |
| Connect repo | 1 min | 2 min |
| Configure | 3 min | 5 min |
| Deploy | 5 min | 8 min |
| **Total** | **~11 min** | **~17 min** |

---

## 💡 Pro Tips

### For Railway:
- Use `railway logs` CLI to view real-time logs
- Add PostgreSQL from marketplace if you need production DB
- Railway CLI makes deployment super fast
- Auto-deploys on every git push

### For Render:
- Free tier sleeps after 15 min of inactivity
- First request wakes it up (takes ~30-60 sec)
- Upgrade to paid tier ($7/month) for always-on
- Use Render Disks for persistent storage

---

## 🔗 Useful Links

**Railway:**
- Dashboard: https://railway.app/dashboard
- Docs: https://docs.railway.app
- CLI: https://docs.railway.app/develop/cli

**Render:**
- Dashboard: https://dashboard.render.com
- Docs: https://render.com/docs
- Support: https://render.com/docs/support

---

## 🎯 Next Command

Start here:

```powershell
# Initialize and push to GitHub
cd D:\Kapil\DotNet\MyRestaurant
git init
git add .
git commit -m "Initial commit"

# Then create GitHub repo and push
# Then go to Railway.app or Render.com
```

**Let's deploy your API!** 🚀
