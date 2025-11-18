# Platform API

The backend services for the Starter Platform, built with .NET 9 and Clean Architecture principles.

**[← Back to Main Documentation](../../README.md)**

## 🏗️ Architecture

The API follows Clean Architecture with clear separation of concerns:

```
┌─────────────────────────────────────────────────────────────┐
│                    Platform.Api                            │
│              (HTTP Controllers & API)                      │
└─────────────────────────────────────────────────────────────┘
                              │
┌─────────────────────────────────────────────────────────────┐
│                Platform.DomainLayer                        │
│              (Business Logic & Data Access)                │
└─────────────────────────────────────────────────────────────┘
                              │
┌─────────────────────────────────────────────────────────────┐
│                Platform.AcceptanceTests                    │
│              (Integration Testing)                         │
└─────────────────────────────────────────────────────────────┘
                              │
┌─────────────────────────────────────────────────────────────┐
│                Platform.Database                           │
│              (Liquibase Migrations)                        │
└─────────────────────────────────────────────────────────────┘
```

## 📁 Project Structure

```
apps/api/
├── README.md                          # This file
├── Platform.Api/                      # HTTP API Layer
│   ├── Controllers/                   # API Controllers
│   ├── Mappers/                      # DTO Mapping
│   ├── ResourcesModels/              # API Request/Response Models
│   ├── Middleware/                   # Custom Middleware
│   ├── Common/                       # Shared API Components
│   └── Program.cs                    # Application Entry Point
├── Platform.DomainLayer/             # Business Logic
│   ├── DomainFacade.cs              # Main Business Interface
│   ├── Managers/                     # Business Logic Managers
│   │   ├── Models/                   # Domain Models
│   │   ├── DataLayer/               # Data Access Layer
│   │   ├── ServiceLocators/         # Dependency Injection
│   │   └── Validators/              # Business Validation
│   └── Common/                       # Shared Domain Components
├── Platform.AcceptanceTests/         # Integration Tests
│   ├── DomainLayer/                 # Business Logic Tests
│   └── ServiceLocator/              # Test Infrastructure
└── Platform.Database/               # Database Management
    ├── changelog/                    # Liquibase Changesets
    └── liquibase/                   # Liquibase Configuration
```

## 🚀 Getting Started

### Prerequisites

From the main project directory:

```bash
# Install dependencies (from main README)
brew install postgresql@17 dotnet just openjdk@17
```

### Local Development

1. **Start PostgreSQL:**
   ```bash
   brew services start postgresql@17
   ```

2. **Run database migrations:**
   ```bash
   just db-update
   ```

3. **Build the API:**
   ```bash
   dotnet build apps/Platform.sln
   ```

4. **Run the API:**
   ```bash
   cd apps/api/Platform.Api
   dotnet run
   ```

5. **Access Swagger UI:**
   - Open: `https://localhost:5001/swagger`

### Quick Commands

From the main project directory:

```bash
# Build everything (API + Web)
just build

# Start both API and Web with browser launch
just start

# Run acceptance tests
just test

# Database operations
just db-update    # Run migrations
just db-connect   # Connect to database
```

## 🏛️ Clean Architecture Layers

### Platform.Api (Presentation Layer)
- **Controllers**: HTTP endpoints and request handling
- **Mappers**: Convert between API models and domain models
- **Middleware**: Cross-cutting concerns (exception handling, logging)
- **ResourceModels**: Request/response DTOs

**Key Files:**
- `Controllers/PersonaController.cs` - Persona API endpoints
- `Mappers/PersonaMapper.cs` - DTO mapping logic
- `Middleware/PlatformExceptionHandlingMiddleware.cs` - Global exception handling

### Platform.DomainLayer (Business Logic)
- **DomainFacade**: Main business interface
- **Managers**: Business logic implementation
- **Models**: Domain entities
- **DataLayer**: Data access abstraction
- **Validators**: Business rule validation

**Key Files:**
- `DomainFacade.cs` - Main business interface
- `Managers/PersonaManager.cs` - Persona business logic
- `Managers/DataLayer/DataFacade.cs` - Data access interface
- `Managers/Models/Persona.cs` - Domain model

### Platform.AcceptanceTests (Testing Layer)
- **Integration Tests**: Test complete business workflows
- **Custom Test Doubles**: No external mocking frameworks
- **Database Testing**: Real database integration

**Key Files:**
- `DomainLayer/DomainFacadeTest.cs` - Main business tests
- `ServiceLocator/ServiceLocatorForAcceptanceTesting.cs` - Test configuration

### Platform.Database (Data Layer)
- **Liquibase Migrations**: Version-controlled schema changes
- **Configuration**: Database connection settings

**Key Files:**
- `changelog/db.changelog-master.xml` - Migration master file
- `liquibase/config/liquibase.properties` - Database configuration

## 🔧 Configuration

### Database Connection

The API uses PostgreSQL with connection strings configured in:
- `Platform.Api/appsettings.json` - Default configuration
- `Platform.Api/appsettings.Development.json` - Development overrides

### Environment Variables

For CI/CD and different environments:
```bash
ConnectionStrings__DbConnectionString="Host=localhost;Port=5432;Database=starter;Username=postgres;Password=postgres"
```

## 🧪 Testing

### Acceptance Tests

Run comprehensive integration tests:

```bash
# From main directory
just test

# Or directly
dotnet test apps/api/Platform.AcceptanceTests/Platform.AcceptanceTests.csproj --verbosity normal
```

### Test Philosophy

- **No External Mocking**: Uses custom test doubles and stubs
- **Real Database**: Tests against actual PostgreSQL instance
- **Complete Workflows**: Tests entire business processes
- **Clean Architecture**: Tests through DomainFacade interface only

## 📊 Database Management

### Migrations

```bash
# Run all pending migrations
just db-update

# Connect to database for manual inspection
just db-connect
```

### Adding New Migrations

1. Create new changeset in `Platform.Database/changelog/changes/`
2. Update `Platform.Database/changelog/db.changelog-master.xml`
3. Run `just db-update` to apply

Example changeset structure:
```xml
<changeSet id="003-add-new-table" author="developer">
    <createTable tableName="new_table">
        <column name="id" type="BIGSERIAL">
            <constraints primaryKey="true"/>
        </column>
        <!-- Additional columns -->
    </createTable>
</changeSet>
```

## 🚦 CI/CD Integration

The API is automatically built and tested in GitHub Actions:

1. ✅ .NET 9 setup and restore
2. ✅ PostgreSQL service startup
3. ✅ Database migration execution
4. ✅ API build and compilation
5. ✅ Acceptance test execution

See: [GitHub Actions Configuration](../../.github/workflows/build.yml)

## 🔍 API Endpoints

### Swagger Documentation

When running locally, comprehensive API documentation is available at:
- **Development**: `https://localhost:5001/swagger`

### Current Endpoints

- **Personas**: CRUD operations for persona management
  - `GET /api/personas` - List personas
  - `POST /api/personas` - Create persona
  - `GET /api/personas/{id}` - Get persona details
  - `PUT /api/personas/{id}` - Update persona
  - `DELETE /api/personas/{id}` - Delete persona

## 🛠️ Development Guidelines

### Adding New Features

1. **Domain Model**: Add/update models in `Platform.DomainLayer/Managers/Models/`
2. **Business Logic**: Implement in appropriate manager class
3. **Data Access**: Add methods to DataFacade and DataManager
4. **API Layer**: Create/update controller and resource models
5. **Tests**: Add acceptance tests covering the complete workflow
6. **Database**: Create migration if schema changes needed

### Code Standards

- Follow Clean Architecture principles
- Use dependency injection through ServiceLocator
- Implement proper exception handling
- Write acceptance tests for all new features
- Use async/await for all I/O operations

## 📋 Cursor Rules & Templates

The API layer uses comprehensive Cursor rules and Handlebars templates for consistent code generation and development patterns.

### 🔧 API Rules (.cursor/rules/)

#### endpoint-workflow.mdc
- **Type**: Interactive Development Workflow
- **Purpose**: Guides step-by-step creation of new API endpoints
- **Scope**: Complete feature development from database to API
- **Key Features**:
  - Interactive prompts for feature definition
  - Automatic code generation using templates
  - Verification against template expectations
  - Phase-based development (Database → Domain → API)

#### principles.mdc
- **Type**: Architectural Guidelines
- **Purpose**: Core principles for Clean Architecture implementation
- **Scope**: All backend development
- **Key Areas**:
  - Clean Architecture boundaries and dependencies
  - Facade and Manager patterns
  - Validation strategy (API vs Domain layer)
  - Exception handling and error responses
  - Result and pagination patterns
  - Database and data access rules

### 🏗️ API Templates (.cursor/templates/)

#### Create Templates (create/)
- **Controller.hbs** - Complete API controller with all CRUD operations
- **Manager.hbs** - Domain manager with business logic
- **DataManager.hbs** - Data access layer with SQL queries
- **DataFacade.hbs** - Data facade extensions
- **DomainFacade.Base.hbs** - Domain facade extensions
- **ResourceModel.hbs** - API request/response models
- **Mapper.hbs** - Mapping between API and domain models
- **Mapping.hbs** - AutoMapper configuration
- **DomainModel.hbs** - Core domain entity
- **Validator.hbs** - Business validation logic
- **Test.hbs** - Comprehensive acceptance tests
- **ValidationException.hbs** - Custom validation exceptions
- **NotFoundException.hbs** - Not found exceptions
- **DuplicateException.hbs** - Duplicate entity exceptions

#### Update Templates (update/)
- **AddMethods.hbs** - Add new methods to existing classes

#### Common Templates (common/)
- **Mapping.hbs** - Shared mapping configurations

### 🎯 Template Features

- **Handlebars-based**: Uses `.hbs` syntax for dynamic code generation
- **Feature-driven**: Templates generate complete features, not just individual files
- **Consistent patterns**: Ensures all generated code follows architectural principles
- **Validation included**: Generated code includes proper validation and error handling
- **Test coverage**: Automatic generation of acceptance tests

## 🗄️ Database Rules & Templates

The database layer maintains separate rules and templates for Liquibase migrations.

### 📋 Database Rules (Platform.Database/.cursor/rules/)

#### database-workflow.mdc
- **Type**: Database Development Workflow
- **Purpose**: Standard procedures for creating and updating database tables
- **Scope**: All Liquibase migrations
- **Key Features**:
  - Interactive prompts for table creation/updates
  - Changeset naming conventions
  - Required table structure (audit fields, constraints)
  - Performance guidelines (indexing)

#### liquibase-best-practices.mdc
- **Type**: Best Practices Guide
- **Purpose**: Liquibase-specific guidelines and patterns
- **Scope**: Database schema management
- **Key Areas**:
  - Changeset structure and naming
  - Constraint definitions
  - Index creation strategies
  - Migration rollback considerations

### 🏗️ Database Templates (Platform.Database/.cursor/templates/)

#### Create Templates (create/)
- **Table.hbs** - Complete table creation with constraints and indexes
- **Index.hbs** - Database index creation
- **Function.hbs** - PostgreSQL function definitions
- **Trigger.hbs** - Database trigger creation

#### Update Templates (update/)
- **AlterTable.hbs** - Table modification changesets
- **AddColumn.hbs** - Column addition
- **AddConstraint.hbs** - Constraint addition

### 🎯 Database Template Features

- **Liquibase XML**: Generates proper Liquibase changeset XML
- **Timestamp-based naming**: Automatic timestamp generation for changesets
- **Constraint compliance**: Ensures proper constraint definitions
- **Audit field inclusion**: Automatic inclusion of created_at/updated_at fields
- **Index generation**: Automatic index creation for foreign keys

---

**Navigation:**
- [← Main Documentation](../../README.md)
- [Web Documentation →](../web/Platform.Web/README.md)
- [GitHub Actions →](../../.github/workflows/build.yml) 