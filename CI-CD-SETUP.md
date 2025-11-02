# CI/CD Pipeline Setup Guide for Restaurant Management API

## Overview
This guide will help you set up a complete CI/CD pipeline using **GitHub Actions** (100% FREE, no Azure subscription needed).

## 🎯 What We'll Set Up

1. **Continuous Integration (CI)**: Automatically build and test on every push
2. **Continuous Deployment (CD)**: Automatically deploy to hosting platform
3. **Code Quality Checks**: Linting and security scanning
4. **Automated Testing**: Run unit tests on every commit

## 📋 Prerequisites

- ✅ GitHub account (free)
- ✅ Git installed on your machine
- ✅ Your API project (already done!)
- ⚠️ **NO Azure subscription required**

## 🏗️ Architecture

```
GitHub Repository
    ↓
GitHub Actions (CI)
    ↓ (build, test)
Deployment Options:
    → Railway.app (Free tier)
    → Render.com (Free tier)
    → Fly.io (Free tier)
    → Heroku (Free tier limited)
    → DigitalOcean App Platform ($5/month)
```

## 🚀 Step-by-Step Implementation

### Step 1: Initialize Git Repository (5 minutes)

```bash
# Navigate to your project root
cd D:\Kapil\DotNet\MyRestaurant

# Initialize git (if not already done)
git init

# Create .gitignore
# (already created - see .gitignore file)

# Add all files
git add .

# Commit
git commit -m "Initial commit: Restaurant Management API"
```

### Step 2: Create GitHub Repository (3 minutes)

1. Go to https://github.com
2. Click **"New repository"**
3. Repository name: `restaurant-management-api`
4. Description: `ASP.NET Core 9 Restaurant Management API with Clean Architecture`
5. **Keep it Public** (for free GitHub Actions) or Private (500MB storage free)
6. **DO NOT** initialize with README (we already have files)
7. Click **"Create repository"**

### Step 3: Push to GitHub (2 minutes)

```bash
# Add remote repository (replace YOUR_USERNAME)
git remote add origin https://github.com/YOUR_USERNAME/restaurant-management-api.git

# Push to GitHub
git branch -M main
git push -u origin main
```

### Step 4: Set Up GitHub Actions CI Pipeline (10 minutes)

The CI pipeline will:
- ✅ Build the solution
- ✅ Run tests (when you add them)
- ✅ Create release artifacts
- ✅ Check for security vulnerabilities

**GitHub Actions workflow is already created** at `.github/workflows/ci-cd.yml`

### Step 5: Choose Deployment Platform (FREE Options)

#### Option A: Railway.app (RECOMMENDED - Easiest)

**Why Railway?**
- ✅ $5 free credit per month
- ✅ Automatic HTTPS
- ✅ Easy database support (PostgreSQL/MySQL free)
- ✅ Auto-deploy from GitHub
- ✅ Environment variables support

**Setup Steps:**
1. Go to https://railway.app
2. Sign up with GitHub
3. Click **"New Project"**
4. Select **"Deploy from GitHub repo"**
5. Choose your `restaurant-management-api` repo
6. Railway auto-detects .NET
7. Add environment variables:
   ```
   ASPNETCORE_ENVIRONMENT=Production
   JwtSettings__Key=your-super-secret-key-here-min-32-chars
   JwtSettings__Issuer=RestaurantAPI
   JwtSettings__Audience=RestaurantAPIUsers
   ```
8. Click **"Deploy"**
9. Get your URL: `https://your-app.railway.app`

**Time to deploy**: ~5 minutes

#### Option B: Render.com (Good Free Tier)

**Why Render?**
- ✅ 750 hours/month free
- ✅ Automatic HTTPS
- ✅ PostgreSQL free tier
- ✅ GitHub integration

**Setup Steps:**
1. Go to https://render.com
2. Sign up with GitHub
3. Click **"New +"** → **"Web Service"**
4. Connect your GitHub repo
5. Configure:
   - **Name**: restaurant-api
   - **Environment**: Docker or .NET
   - **Build Command**: `dotnet publish -c Release -o out`
   - **Start Command**: `dotnet out/RestaurantManagement.API.dll`
6. Add environment variables (same as Railway)
7. Click **"Create Web Service"**

**Time to deploy**: ~7 minutes

#### Option C: Fly.io (Developer Friendly)

**Why Fly.io?**
- ✅ 3 VMs free
- ✅ 160GB bandwidth/month
- ✅ Great for APIs
- ✅ Global deployment

**Setup Steps:**
1. Install Fly CLI:
   ```bash
   # Windows (PowerShell)
   iwr https://fly.io/install.ps1 -useb | iex
   ```
2. Sign up:
   ```bash
   fly auth signup
   ```
3. Launch app:
   ```bash
   cd src/RestaurantManagement.API
   fly launch
   ```
4. Follow prompts (Fly auto-configures)
5. Deploy:
   ```bash
   fly deploy
   ```

**Time to deploy**: ~10 minutes

#### Option D: DigitalOcean App Platform ($5/month)

**Why DigitalOcean?**
- ✅ Very reliable
- ✅ $5/month (first $200 free with credit)
- ✅ Easy to scale
- ✅ Great documentation

**Setup Steps:**
1. Go to https://www.digitalocean.com
2. Sign up (get $200 credit)
3. Create → App Platform
4. Connect GitHub repo
5. Configure build settings
6. Deploy

**Cost**: $5/month (covered by free credit initially)

### Step 6: Configure Deployment Secrets (5 minutes)

For GitHub Actions to deploy, add secrets:

1. Go to your GitHub repo
2. **Settings** → **Secrets and variables** → **Actions**
3. Click **"New repository secret"**
4. Add these secrets:

```
# For Railway
RAILWAY_TOKEN=your-railway-token-here

# For Render
RENDER_API_KEY=your-render-api-key

# For Fly.io
FLY_API_TOKEN=your-fly-token

# Common secrets
JWT_SECRET_KEY=your-super-secret-key-min-32-characters-long
DATABASE_CONNECTION_STRING=Data Source=RestaurantManagement.db
```

### Step 7: Update CI/CD Workflow (Already Done!)

The workflow file at `.github/workflows/ci-cd.yml` includes:
- ✅ Build on push to main
- ✅ Run tests
- ✅ Create artifacts
- ✅ Deploy to chosen platform

### Step 8: Test the Pipeline (5 minutes)

```bash
# Make a small change
echo "# CI/CD Test" >> README.md

# Commit and push
git add .
git commit -m "Test CI/CD pipeline"
git push origin main

# Watch the magic happen!
# Go to: https://github.com/YOUR_USERNAME/restaurant-management-api/actions
```

## 📊 Pipeline Stages Explained

### Stage 1: Build (2-3 minutes)
```yaml
- Restore NuGet packages
- Build solution
- Run tests
- Check for errors
```

### Stage 2: Test (1-2 minutes)
```yaml
- Run unit tests
- Generate coverage report
- Upload test results
```

### Stage 3: Security Scan (1 minute)
```yaml
- Check for vulnerable packages
- Scan for security issues
- Report findings
```

### Stage 4: Deploy (2-5 minutes)
```yaml
- Package application
- Push to hosting platform
- Run migrations
- Health check
```

## 🎨 Adding Badge to README

After setup, add status badge to your README:

```markdown
![CI/CD Pipeline](https://github.com/YOUR_USERNAME/restaurant-management-api/workflows/CI-CD/badge.svg)
```

## 🔒 Security Best Practices

### 1. Environment Variables
Never commit secrets! Use:
- GitHub Secrets for CI/CD
- Platform environment variables for deployment

### 2. Database Migrations
Auto-run migrations on deployment:
```bash
dotnet ef database update --project src/RestaurantManagement.Infrastructure
```

### 3. Health Checks
Add health check endpoint:
```csharp
// Program.cs
app.MapHealthChecks("/health");
```

## 📈 Monitoring & Logging

### Free Monitoring Tools:

1. **UptimeRobot** (https://uptimerobot.com)
   - Free: 50 monitors
   - Check API every 5 minutes
   - Email alerts

2. **Better Uptime** (https://betteruptime.com)
   - Free tier available
   - Status pages
   - Incident management

3. **Sentry** (https://sentry.io)
   - Free: 5K errors/month
   - Error tracking
   - Performance monitoring

## 🚀 Advanced: Multi-Environment Setup

### Development → Staging → Production

Create separate workflows:
```
.github/workflows/
  ├── ci.yml (runs on all branches)
  ├── deploy-staging.yml (runs on develop branch)
  └── deploy-production.yml (runs on main branch)
```

## 📝 Recommended: Add Tests First

Before deploying, add unit tests:

```bash
# Create test project
dotnet new xunit -n RestaurantManagement.Tests
cd RestaurantManagement.Tests
dotnet add reference ../src/RestaurantManagement.Application
dotnet add package Moq
dotnet add package FluentAssertions
```

## 🎯 Complete Setup Timeline

| Step | Time | Difficulty |
|------|------|------------|
| 1. Initialize Git | 5 min | Easy |
| 2. Create GitHub Repo | 3 min | Easy |
| 3. Push to GitHub | 2 min | Easy |
| 4. Setup GitHub Actions | 10 min | Medium |
| 5. Choose Platform | 5 min | Easy |
| 6. Configure Secrets | 5 min | Easy |
| 7. Update Workflow | 5 min | Medium |
| 8. Test Pipeline | 5 min | Easy |
| **TOTAL** | **40 min** | **Medium** |

## ✅ Success Checklist

After completing setup:
- [ ] Code pushed to GitHub
- [ ] GitHub Actions workflow running
- [ ] Build passing (green check ✓)
- [ ] API deployed to platform
- [ ] Can access API at public URL
- [ ] Environment variables configured
- [ ] Database migrations applied
- [ ] Health check endpoint working
- [ ] Swagger accessible in production
- [ ] CI/CD badge added to README

## 🎉 What You Get

✅ **Automated builds** on every push  
✅ **Automated tests** (when you add them)  
✅ **Automated deployments** to production  
✅ **Zero downtime** deployments  
✅ **Rollback capability** (via Git)  
✅ **Build status** visibility  
✅ **Free hosting** (with free tiers)  

## 🔄 Deployment Flow

```
Developer pushes code
        ↓
GitHub receives push
        ↓
GitHub Actions triggered
        ↓
Build & Test (2-3 min)
        ↓
Create Docker image / Build artifact
        ↓
Deploy to Platform (2-5 min)
        ↓
Health Check
        ↓
✅ Live in Production!
```

## 💡 Pro Tips

1. **Start Simple**: Deploy to one platform first
2. **Test Locally**: Use `dotnet publish` to test build
3. **Monitor Costs**: Check platform usage regularly
4. **Use Staging**: Test in staging before production
5. **Database Backups**: Set up automated backups
6. **SSL/HTTPS**: All platforms provide free SSL
7. **Custom Domain**: Add your domain (optional)

## 🆘 Troubleshooting

### Build Fails?
```bash
# Test locally first
dotnet restore
dotnet build
dotnet test
```

### Deploy Fails?
- Check environment variables
- Verify connection strings
- Check platform logs
- Ensure database is accessible

### API Not Responding?
- Check health endpoint: `/health`
- Review application logs
- Verify port configuration
- Check firewall rules

## 📚 Next Steps

1. ✅ Complete basic CI/CD setup
2. 📝 Add unit tests
3. 🔒 Add integration tests
4. 📊 Set up monitoring
5. 🚀 Add performance testing
6. 📧 Configure email notifications
7. 🌐 Add custom domain
8. 💾 Set up database backups

## 🎓 Learning Resources

- GitHub Actions Docs: https://docs.github.com/en/actions
- Railway Docs: https://docs.railway.app
- Render Docs: https://render.com/docs
- Fly.io Docs: https://fly.io/docs
- .NET Deployment: https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/

---

**Ready to automate your deployments? Let's get started!** 🚀

Choose your preferred platform and follow the steps above. You'll have a fully automated CI/CD pipeline in under an hour!
