# 🔐 Setting Up Auth0 and OpenAI Credentials

This guide will help you configure authentication and AI features for the Starter Platform.

## 📋 Prerequisites

Before starting, make sure you have:
- An Auth0 account (free tier works): https://auth0.com/signup
- An OpenAI API key: https://platform.openai.com/api-keys

---

## 🔧 Part 1: .NET API Configuration (OpenAI)

The .NET API uses **User Secrets** to store sensitive credentials locally.

### Step 1: Set OpenAI API Key

```bash
cd apps/api/Platform.Api
dotnet user-secrets set "Gateways:OpenAI:ApiKey" "your-openai-api-key-here"
```

### Step 2: Verify the Secret

```bash
dotnet user-secrets list
```

You should see:
```
Gateways:OpenAI:ApiKey = your-openai-api-key-here
```

---

## 🔧 Part 2: Next.js Frontend Configuration (Auth0)

The Next.js app uses environment variables stored in `.env.local`.

### Step 1: Create `.env.local` File

Create a file at `apps/web/Platform.Web/.env.local` with the following content:

```bash
# Auth0 Configuration
AUTH0_DOMAIN=your-auth0-domain.us.auth0.com
AUTH0_CLIENT_ID=your-client-id
AUTH0_CLIENT_SECRET=your-client-secret
AUTH0_SECRET=your-random-secret-at-least-32-characters-long
AUTH0_AUDIENCE=https://starterapp-api.dev
APP_BASE_URL=http://localhost:3000
NEXT_PUBLIC_API_URL=http://localhost:5000/api/v1
```

### Step 2: Get Auth0 Credentials

1. **Log in to Auth0 Dashboard**: https://manage.auth0.com/
2. **Create an Application** (if you don't have one):
   - Go to Applications → Create Application
   - Choose "Regular Web Application"
   - Name it "Starter Platform Web"
3. **Get Your Credentials**:
   - **Domain**: Found in the top-right corner (e.g., `dev-xxxxx.us.auth0.com`)
   - **Client ID**: Found in your Application settings
   - **Client Secret**: Found in your Application settings (click "Show")
4. **Create an API** (if you don't have one):
   - Go to Applications → APIs → Create API
   - Name: "Starter Platform API"
   - Identifier: `https://starterapp-api.dev` (must match `AUTH0_AUDIENCE`)
   - Signing Algorithm: RS256
5. **Generate AUTH0_SECRET**:
   ```bash
   openssl rand -hex 32
   ```
   Copy the output and use it as your `AUTH0_SECRET`

### Step 3: Configure Auth0 Application Settings

In your Auth0 Application settings:

1. **Allowed Callback URLs**:
   ```
   http://localhost:3000/api/auth/callback
   ```

2. **Allowed Logout URLs**:
   ```
   http://localhost:3000
   ```

3. **Allowed Web Origins**:
   ```
   http://localhost:3000
   ```

4. **Enable APIs**:
   - Go to APIs → Authorize Applications
   - Authorize your web application
   - Grant the following scopes: `openid`, `profile`, `email`, `offline_access`

---

## 🔧 Part 3: Update .NET API Auth0 Configuration (Optional)

If you're using your own Auth0 account, update `apps/api/Platform.Api/appsettings.json`:

```json
{
  "Auth0": {
    "Domain": "your-auth0-domain.us.auth0.com",
    "Audience": "https://starterapp-api.dev"
  }
}
```

**Note**: The `Audience` must match the API Identifier you created in Auth0.

---

## ✅ Verification

### Test .NET API Configuration

```bash
cd apps/api/Platform.Api
dotnet run
```

Check the logs - you should not see any OpenAI configuration errors.

### Test Next.js Configuration

```bash
cd apps/web/Platform.Web
npm run dev
```

1. Navigate to `http://localhost:3000/login`
2. Click "Sign In"
3. You should be redirected to Auth0 login page
4. After logging in, you should be redirected back to the app

---

## 🐛 Troubleshooting

### "Module not found" errors in Next.js
- Make sure you've created `tsconfig.json` (already done)
- Restart the dev server after creating `.env.local`

### Auth0 login not working
- Verify all environment variables are set correctly
- Check that callback URLs are configured in Auth0 Dashboard
- Ensure `AUTH0_AUDIENCE` matches the API Identifier in Auth0

### OpenAI API errors
- Verify the API key is set: `dotnet user-secrets list`
- Check that your OpenAI account has credits/quota
- Ensure you're running in Development environment (User Secrets only work in Development)

### "Invalid token" errors
- Make sure `AUTH0_AUDIENCE` in `.env.local` matches `Auth0:Audience` in `appsettings.json`
- Verify the API is authorized in Auth0 Dashboard

---

## 📝 Quick Reference

### .NET User Secrets Commands
```bash
# Set a secret
dotnet user-secrets set "Gateways:OpenAI:ApiKey" "your-key"

# List all secrets
dotnet user-secrets list

# Remove a secret
dotnet user-secrets remove "Gateways:OpenAI:ApiKey"

# Clear all secrets
dotnet user-secrets clear
```

### Environment Variables for Next.js
All variables in `.env.local` are loaded automatically by Next.js. No need to export them manually.

---

## 🔒 Security Notes

- **Never commit** `.env.local` to git (it's already in `.gitignore`)
- **Never commit** User Secrets (they're stored outside the project)
- Use different credentials for development and production
- Rotate secrets regularly

---

## 🚀 Next Steps

Once credentials are configured:
1. Restart both API and Web servers
2. Test login functionality
3. Test AI chat features (if OpenAI is configured)
4. Start building your features!

