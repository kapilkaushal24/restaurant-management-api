# 🚀 Quick Deploy Commands - Copy & Paste Ready

## Step 1: Initialize Git (1 minute)

```powershell
# Navigate to project
cd D:\Kapil\DotNet\MyRestaurant

# Initialize git
git init

# Add all files
git add .

# Commit
git commit -m "Initial commit: Restaurant Management API"
```

## Step 2: Create GitHub Repository

1. Open browser: https://github.com/new
2. Name: `restaurant-management-api`
3. Keep **Public**
4. Click **"Create repository"**

## Step 3: Push to GitHub (replace YOUR_USERNAME)

```powershell
# Add remote - REPLACE YOUR_USERNAME!
git remote add origin https://github.com/YOUR_USERNAME/restaurant-management-api.git

# Push to GitHub
git branch -M main
git push -u origin main
```

---

## Option A: 🚂 Deploy to Railway (Recommended - 5 minutes)

### Method 1: Using Railway Web UI (Easiest)

1. **Go to**: https://railway.app
2. **Click**: "Start a New Project"
3. **Sign in**: with GitHub
4. **Select**: "Deploy from GitHub repo"
5. **Choose**: your `restaurant-management-api` repo
6. **Wait**: 3-5 minutes for automatic deployment

### Add Environment Variables:

Click on your service → **Variables** tab → Add these:

```
ASPNETCORE_ENVIRONMENT = Production
ASPNETCORE_URLS = http://0.0.0.0:8080
PORT = 8080
JwtSettings__Key = your-super-secret-jwt-key-must-be-at-least-32-characters-long-please-change-this
JwtSettings__Issuer = RestaurantAPI
JwtSettings__Audience = RestaurantAPIUsers
JwtSettings__ExpirationInHours = 24
```

### Get Your URL:

1. Go to **Settings** tab
2. Scroll to **Networking**
3. Click **"Generate Domain"**
4. Copy your URL: `https://xxxxx.up.railway.app`

### Method 2: Using Railway CLI (Advanced)

```powershell
# Install Railway CLI
npm install -g @railway/cli

# Login
railway login

# Link to your project (in project directory)
cd D:\Kapil\DotNet\MyRestaurant
railway init

# Set environment variables
railway variables set ASPNETCORE_ENVIRONMENT=Production
railway variables set ASPNETCORE_URLS="http://0.0.0.0:8080"
railway variables set PORT=8080
railway variables set JwtSettings__Key="your-super-secret-jwt-key-must-be-at-least-32-characters-long"
railway variables set JwtSettings__Issuer="RestaurantAPI"
railway variables set JwtSettings__Audience="RestaurantAPIUsers"
railway variables set JwtSettings__ExpirationInHours=24

# Deploy
railway up

# Open your app
railway open
```

---

## Option B: 🎨 Deploy to Render (If Railway doesn't work)

### Web UI Method:

1. **Go to**: https://render.com
2. **Sign up**: with GitHub
3. **Click**: "New +" → "Web Service"
4. **Select**: your `restaurant-management-api` repo
5. **Configure**:
   - Name: `restaurant-api`
   - Environment: `Docker`
   - Branch: `main`
   - Dockerfile Path: `./Dockerfile`

6. **Add Environment Variables**:

```
ASPNETCORE_ENVIRONMENT = Production
ASPNETCORE_URLS = http://0.0.0.0:10000
PORT = 10000
JwtSettings__Key = your-super-secret-jwt-key-must-be-at-least-32-characters-long-please-change-this
JwtSettings__Issuer = RestaurantAPI
JwtSettings__Audience = RestaurantAPIUsers
JwtSettings__ExpirationInHours = 24
```

7. **Click**: "Create Web Service"
8. **Wait**: 5-10 minutes for deployment
9. **Your URL**: `https://restaurant-api.onrender.com`

---

## 🧪 Test Your Deployed API

Replace `YOUR_URL` with your actual Railway or Render URL:

```powershell
# Test 1: Health Check
curl https://YOUR_URL/health

# Test 2: Register SuperAdmin
curl -X POST https://YOUR_URL/api/Auth/register `
  -H "Content-Type: application/json" `
  -d '{
    "name": "Super Admin",
    "email": "admin@restaurant.com",
    "password": "Admin@123",
    "role": "SuperAdmin"
  }'

# Test 3: Login
curl -X POST https://YOUR_URL/api/Auth/login `
  -H "Content-Type: application/json" `
  -d '{
    "email": "admin@restaurant.com",
    "password": "Admin@123"
  }'

# Test 4: Open Swagger in Browser
start https://YOUR_URL
```

---

## 🔧 If Something Goes Wrong

### Railway Troubleshooting:

```powershell
# View logs
railway logs

# Check status
railway status

# Restart service
railway restart
```

**Or in Web UI:**
- Go to your project
- Click "Deployments" tab
- Check logs for errors

### Render Troubleshooting:

**In Web UI:**
- Go to your service
- Click "Logs" tab
- Check for build/runtime errors

**Common Issues:**
- **503 Error**: Service sleeping (free tier) - wait 30-60 seconds
- **Build Timeout**: Switch to Render paid tier or optimize build
- **Environment Variables**: Make sure all variables are added

---

## ✅ Success Checklist

- [ ] Git repository initialized
- [ ] Code pushed to GitHub
- [ ] Railway/Render account created
- [ ] Repository connected
- [ ] Environment variables added
- [ ] Deployment successful
- [ ] Public URL accessible
- [ ] Health endpoint returns 200
- [ ] Swagger UI loads
- [ ] Can register users
- [ ] Can login and get JWT
- [ ] Protected endpoints work with token

---

## 🎯 What You'll Get

✅ **Live API URL** (e.g., `https://xxxxx.up.railway.app`)
✅ **Auto HTTPS/SSL** (free)
✅ **Auto-deploy on git push**
✅ **Swagger UI** accessible
✅ **Database** persistence (with volumes)
✅ **Health monitoring**
✅ **Logs** for debugging

---

## 💰 Cost Breakdown

### Railway:
- **Free Tier**: $5 credit/month
- **Cost**: ~$0.10/hour when active
- **Expected**: $5-10/month for development
- **Estimate**: Free for first month, then $5-10/month

### Render:
- **Free Tier**: 750 hours/month
- **Cost**: $0 (with limitations)
- **Limitations**: Sleeps after 15 min inactivity
- **Paid**: $7/month for always-on

---

## 🚀 You're Ready!

**Start here:**

```powershell
# 1. Navigate to project
cd D:\Kapil\DotNet\MyRestaurant

# 2. Initialize git
git init
git add .
git commit -m "Initial commit"

# 3. Push to GitHub (after creating repo)
git remote add origin https://github.com/YOUR_USERNAME/restaurant-management-api.git
git branch -M main
git push -u origin main

# 4. Then go to Railway.app or Render.com and deploy!
```

**Need detailed steps? Check `DEPLOY-RAILWAY-RENDER.md`**

---

## 📞 Support

- **Railway Discord**: https://discord.gg/railway
- **Render Support**: https://render.com/docs/support
- **GitHub Issues**: Open an issue in your repo

---

**Ready? Let's deploy!** 🎉
