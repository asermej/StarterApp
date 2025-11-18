# Platform.Api

## Configuration & Secrets

This project uses .NET User Secrets for local development to keep sensitive information (like API keys) out of source control.

### Initial Setup for Developers

When you first clone this repository, you'll need to configure your local secrets:

#### 1. Initialize User Secrets (if not already done)

```bash
cd apps/api/Platform.Api
dotnet user-secrets init
```

#### 2. Set Required Secrets

```bash
# OpenAI API Key
dotnet user-secrets set "Gateways:OpenAI:ApiKey" "your-openai-api-key-here"
```

#### 3. Verify Your Secrets

```bash
dotnet user-secrets list
```

### How It Works

- **Development**: User Secrets are stored in your user profile directory (outside the project) and automatically override values from `appsettings.json`
  - **Windows**: `%APPDATA%\Microsoft\UserSecrets\<user_secrets_id>\secrets.json`
  - **macOS/Linux**: `~/.microsoft/usersecrets/<user_secrets_id>/secrets.json`

- **Production**: Use environment variables with double underscores for nested configuration:
  ```bash
  Gateways__OpenAI__ApiKey=your-production-key
  ```

### Aspire Compatibility

This configuration approach is compatible with .NET Aspire. When you adopt Aspire:
- User Secrets will continue to work in development
- Aspire's AppHost can manage these secrets automatically
- No changes needed to this project's configuration code

### Required Configuration Values

| Key | Description | Required |
|-----|-------------|----------|
| `Gateways:OpenAI:ApiKey` | OpenAI API key for AI features | Yes |

### Troubleshooting

**Secret not loading?**
- Ensure you're running in Development environment
- Verify secrets are set: `dotnet user-secrets list`
- Check your UserSecretsId in `Platform.Api.csproj`

**Need to remove a secret?**
```bash
dotnet user-secrets remove "Gateways:OpenAI:ApiKey"
```

**Need to clear all secrets?**
```bash
dotnet user-secrets clear
```