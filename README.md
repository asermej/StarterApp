# Starter Platform

**A production-ready application template** for building full-stack applications with Clean Architecture principles. This starter includes authentication, working API examples, AI gateway integration, comprehensive testing, and AI-assisted development tools.

Perfect for:
- 🎯 Training and learning Clean Architecture
- 🚀 Starting new initiatives with a solid foundation
- 📚 Reference implementation for best practices
- 🤖 Leveraging AI-powered development workflows

## 🏗️ Architecture Overview

```
┌─────────────────────────────────────────────────────────────┐
│                    Platform.Web                            │
│              (Next.js Frontend)                            │
└─────────────────────────────────────────────────────────────┘
                              │ HTTP
┌─────────────────────────────────────────────────────────────┐
│                    Platform.Api                            │
│              (.NET 9 API)                                  │
└─────────────────────────────────────────────────────────────┘
                              │
┌─────────────────────────────────────────────────────────────┐
│                Platform.DomainLayer                        │
│              (Business Logic)                              │
└─────────────────────────────────────────────────────────────┘
                              │
┌─────────────────────────────────────────────────────────────┐
│                    PostgreSQL                              │
│              (Database)                                    │
└─────────────────────────────────────────────────────────────┘
```

## 📁 Project Structure

```
StarterApp/
├── README.md                          # This file
├── justfile                           # Build automation
├── apps/
│   ├── Platform.sln                   # .NET Solution
│   ├── api/                          # Backend Services
│   │   ├── README.md                 # → API Documentation
│   │   ├── Platform.Api/             # HTTP API Layer
│   │   ├── Platform.DomainLayer/     # Business Logic
│   │   ├── Platform.AcceptanceTests/ # Integration Tests
│   │   └── Platform.Database/        # Database Migrations
│   └── web/                          # Frontend Application
│       ├── README.md                 # → Web Documentation
│       └── Platform.Web/             # Next.js Application
├── .github/
│   └── workflows/
│       └── build.yml                 # CI/CD Pipeline
└── infrastructure/                   # Infrastructure as Code
```

## 🚀 Quick Start

### Prerequisites

Install required Homebrew packages:

```bash
# Core dependencies
brew install postgresql@17
brew install node@18
brew install dotnet
brew install just
brew install liquibase
```

### Setup

1. **Clone and navigate to the project:**
   ```bash
   git clone <repository-url>
   cd StarterApp
   ```

2. **Start PostgreSQL:**
   ```bash
   brew services start postgresql@17
   ```

3. **Create database and user:**
   ```bash
   createdb starter
   createuser -s postgres  # If doesn't exist
   ```

4. **Run database migrations:**
   ```bash
   just db-update
   ```

5. **Build both applications:**
   ```bash
   just build
   ```

6. **Start development servers:**
   ```bash
   just start
   ```

This will open:
- 🌍 **API Swagger**: `https://localhost:5001/swagger`
- 🌐 **Web App**: `http://localhost:3000`

## ✨ What's Included

This starter application comes with working implementations of:

### Authentication & Authorization
- ✅ **Auth0 Integration** - Complete authentication setup with JWT tokens
- ✅ **User Management** - CRUD operations, profile images, and session handling
- ✅ **Protected Routes** - Role-based access control examples

### Core Features (Working Examples)
- ✅ **User Management** - Full CRUD with profile images
- ✅ **Persona System** - AI character management 
- ✅ **Chat Functionality** - Real-time messaging with AI integration
- ✅ **Image Storage** - File upload and retrieval patterns
- ✅ **OpenAI Gateway** - External API integration example

### Architecture & Testing
- ✅ **Clean Architecture** - Domain-driven design with clear boundaries
- ✅ **Domain Layer** - Business logic separation
- ✅ **Acceptance Tests** - 89 passing tests with custom test doubles
- ✅ **Exception Handling** - Centralized error management
- ✅ **Database Migrations** - Liquibase with version control

### AI-Assisted Development
- ✅ **Cursor Rules** - AI development guidelines and patterns
- ✅ **Code Templates** - Generate features with interactive workflows
- ✅ **MCP Integrations** - Feature flags, UI generation, documentation
- ✅ **Interactive Workflows** - Step-by-step feature creation

## 🎯 What to Build Next

Use this starter as a foundation for your own features:

1. **Follow the Patterns**: Use existing User, Persona, and Chat implementations as examples
2. **Use AI Tools**: Leverage the `.cursor` templates to generate new features
3. **Maintain Tests**: Write acceptance tests following the existing patterns
4. **Keep Clean Architecture**: Maintain separation between layers

## 🛠️ Available Commands

### Development
- `just build` - Build both API and web applications
- `just start` - Build and start both applications with browser launch
- `just test` - Run acceptance tests

### Database
- `just db-update` - Run database migrations
- `just db-connect` - Connect to PostgreSQL database

## 📚 Documentation

### Application-Specific Documentation
- **[API Documentation](apps/api/README.md)** - .NET API, Domain Layer, and Database
- **[Web Documentation](apps/web/Platform.Web/README.md)** - Next.js Frontend Application

### Architecture Documentation
- **Clean Architecture**: Domain-driven design with clear separation of concerns
- **Database**: PostgreSQL with Liquibase migrations
- **Testing**: Acceptance tests with custom test doubles (no external mocking frameworks)
- **CI/CD**: GitHub Actions with full integration testing

## 🔧 Technology Stack

### Backend
- **.NET 9** - API framework
- **PostgreSQL 17** - Database
- **Liquibase** - Database migrations
- **Clean Architecture** - Architectural pattern

### Frontend
- **Next.js 15** - React framework
- **TypeScript** - Type safety
- **Tailwind CSS** - Styling
- **Radix UI** - Component library

### Development Tools
- **just** - Command runner
- **GitHub Actions** - CI/CD pipeline

## 🚦 CI/CD Pipeline

The GitHub Actions pipeline automatically:
1. ✅ Sets up .NET 9 and Node.js 18
2. ✅ Installs dependencies for both applications
3. ✅ Runs database migrations
4. ✅ Builds both API and web applications
5. ✅ Runs acceptance tests with PostgreSQL

## 🤝 Contributing

1. Follow the Clean Architecture principles
2. Write acceptance tests for new features
3. Use the provided `just` commands for consistency
4. Update documentation when adding new features

### 📋 Cursor Rules & Templates

The project uses distributed Cursor rules and templates across different components:

#### 🏛️ Main Platform Rules (.cursor/rules/)

- **architecture-overview.mdc** - Complete Clean Architecture guidelines and patterns
  - Clean Architecture layer definitions and dependencies
  - InternalsVisibleTo patterns for testing
  - Result and pagination patterns
  - Test double strategies (no external mocking frameworks)
  - Data flow diagrams and communication patterns

- **dev_workflow.mdc** - Taskmaster development workflow integration
  - Basic development loop (list → next → show → expand → implement)
  - Multi-context workflows with tagged task lists
  - Git feature branching integration
  - PRD-driven feature development

- **taskmaster.mdc** - Comprehensive Taskmaster tool reference
  - MCP tools vs CLI commands
  - Task creation, modification, and status management
  - AI-powered task analysis and expansion
  - Project complexity analysis

- **self_improve.mdc** - Rule improvement and maintenance guidelines
  - Pattern recognition for new rules
  - Rule quality checks and continuous improvement
  - Documentation synchronization

- **cursor_rules.mdc** - Rule formatting and structure standards
  - Required rule structure with YAML frontmatter
  - File references and code example patterns
  - Rule maintenance best practices

#### 🔧 Component-Specific Rules & Templates

- **[API Rules & Templates](apps/api/README.md#cursor-rules--templates)** - .NET API development patterns
- **[Database Rules & Templates](apps/api/README.md#database-rules--templates)** - Liquibase and database patterns
- **[Web Rules](apps/web/Platform.Web/README.md#cursor-rules)** - Next.js and React development patterns

Each component maintains its own specific rules and code generation templates for consistent development patterns.

## 🔌 MCP Servers

The project integrates with several Model Context Protocol (MCP) servers to enhance development capabilities through AI-powered tools and integrations.

### 🚀 LaunchDarkly MCP Server
- **Package**: `@launchdarkly/mcp-server`
- **Purpose**: Feature flag management and configuration
- **Capabilities**:
  - Create and manage feature flags programmatically
  - Configure targeting rules and rollout strategies
  - Manage AI configurations for LLM experimentation
  - List and update feature flag variations
  - Delete deprecated feature flags
- **Authentication**: Requires LaunchDarkly API key
- **Use Cases**:
  - A/B testing for new features
  - Gradual feature rollouts
  - AI model configuration management
  - Environment-specific feature control

### ✨ Magic MCP (21st.dev)
- **Package**: `@21st-dev/magic@latest`
- **Purpose**: UI component generation and design assistance
- **Capabilities**:
  - Generate React/Next.js components from descriptions
  - Search and retrieve UI component inspiration
  - Refine and improve existing UI components
  - Logo search and integration
  - Component library integration
- **Authentication**: Requires 21st.dev API key
- **Use Cases**:
  - Rapid UI prototyping
  - Component library expansion
  - Design system consistency
  - Logo and branding integration

### 📚 Context7
- **Type**: Remote MCP server
- **URL**: `https://mcp.context7.com/mcp`
- **Purpose**: Documentation and library reference
- **Capabilities**:
  - Resolve library names to Context7-compatible IDs
  - Fetch up-to-date documentation for libraries
  - Provide context-aware code examples
  - Library version-specific documentation
- **Authentication**: No API key required
- **Use Cases**:
  - Quick library documentation lookup
  - API reference during development
  - Version-specific implementation guidance
  - Code example generation

### 🔧 Configuration

MCP servers are configured in `.cursor/mcp.json`:

```json
{
  "mcpServers": {
    "LaunchDarkly": {
      "command": "npx",
      "args": [
        "-y", "--package", "@launchdarkly/mcp-server", "--", "mcp", "start",
        "--api-key", "your-launchdarkly-api-key"
      ]
    },
    "Magic MCP": {
      "command": "npx",
      "args": [
        "-y", "@21st-dev/magic@latest",
        "API_KEY=\"your-21st-dev-api-key\""
      ]
    },
    "context7": {
      "url": "https://mcp.context7.com/mcp"
    }
  }
}
```

### 🔑 Setup Instructions

1. **Copy the template**:
   ```bash
   cp .cursor/mcp.json.template .cursor/mcp.json
   ```

2. **Add your API keys**:
   - **LaunchDarkly**: Replace `<fill in>` with your LaunchDarkly API key
   - **Magic MCP**: Replace `<fill in>` with your 21st.dev API key
   - **Context7**: No setup required (remote server)

3. **Restart Cursor** to load the MCP servers

### 🎯 Integration Benefits

- **Feature Management**: Control feature rollouts and A/B testing through LaunchDarkly
- **Rapid Development**: Generate UI components quickly with Magic MCP
- **Documentation Access**: Instant access to library documentation via Context7
- **AI-Enhanced Workflow**: Leverage AI tools for code generation and improvement
- **Consistent Patterns**: Maintain design system consistency across components

## 📋 Requirements

- **macOS** (for Homebrew packages)
- **Node.js 18+** (for Next.js 15 compatibility)
- **.NET 9 SDK**
- **PostgreSQL 17**
- **Liquibase** (for database migrations)

---

**Quick Navigation:**
- [API Documentation →](apps/api/README.md)
- [Web Documentation →](apps/web/Platform.Web/README.md)
- [GitHub Actions →](.github/workflows/build.yml) 