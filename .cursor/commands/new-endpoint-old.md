# Interactive API Endpoint Development Workflow

**MANDATORY PROCESS**: This workflow guides the development of a new API endpoint interactively. It prompts for information at each stage, generates code using `.hbs` templates, and waits for confirmation before proceeding.

**CRITICAL**: This is NOT optional guidance – it is the REQUIRED process for all new API features.

**Reference**: For all architectural decisions, refer to the [Backend Architecture Principles](docs/technical/reference/core/principles.md).

---

## Phase Overview (5-Phase Workflow)

When guiding the user, always frame progress in terms of these phases:

1. **Phase 1 – Intelligent Feature Scoping**
2. **Phase 2 – Database Layer**
3. **Phase 3 – Domain Layer**
4. **Phase 4 – Acceptance Tests**
5. **Phase 5 – API Layer**

At the **start of each phase**, begin your message with a boxed progress header like:

    > ---
    > **Phase X of 5 — Phase Name**
    > **Progress**
    > ✅ Phase 1 — Intelligent Feature Scoping  
    > ✅ Phase 2 — Database Layer  
    > ⏳ Phase 3 — Domain Layer  
    > ⬜ Phase 4 — Acceptance Tests  
    > ⬜ Phase 5 — API Layer
    > ---

Use:
- ✅ for completed phases  
- ⏳ for the current phase  
- ⬜ for phases not started yet

Update the icons and the phase name/number as you move through the workflow.

---

## Phase 1: Intelligent Feature Scoping

> ---
> **Phase 1 of 5 — Intelligent Feature Scoping**
> **Progress**
> ⏳ Phase 1 — Intelligent Feature Scoping  
> ⬜ Phase 2 — Database Layer  
> ⬜ Phase 3 — Domain Layer  
> ⬜ Phase 4 — Acceptance Tests  
> ⬜ Phase 5 — API Layer
> ---

**Goal**: Define the high-level requirements for the new feature through an interactive, step-by-step process that infers details and asks for confirmation.

When you start this phase, briefly orient the user:

> "We’ll start by scoping the feature (name, properties, operations, auth, etc.).  
> After this, we’ll move on to database, domain, tests, and API."

Then proceed with the prompts below, one at a time, waiting for user responses.

---

### Prompt 1: Infer Feature Name

> "To begin, I’ll infer the **Feature Name** from your request. For example, if you say *'I need to manage user profiles'*, I’ll infer `UserProfile`.
>
> It looks like you want to create a feature for `{{featureName}}`. Is that correct?"

**Wait for user confirmation before continuing.**

---

### Prompt 2: Properties

> "Great. Now, what are the **Properties** of your entity?
>
> Please provide them as a comma-separated list with their C# types, for example:  
> `Id:Guid, ProductName:string, Sku:string, CreatedAt:DateTime`"

**Wait for user response.**

---

### Prompt 3: Propose & Refine Constraints

After the user provides properties, propose constraints:

> "Understood. I’ve analyzed your properties and proposed some database constraints below. Please review and edit them as needed.
>
> **Suggested Constraints:**"

    - Id:Guid[primary_key]
    - ProductName:string[max_length:255, not_null]
    - Sku:string[max_length:100, not_null, unique]
    - CreatedAt:DateTime[not_null]

> "**Common Constraints You Can Add:**
> * `primary_key`: Marks the field as the primary key.
> * `not_null`: Ensures the field cannot be empty.
> * `unique`: Ensures all values in the column are unique.
> * `max_length:<number>`: Sets the maximum length for a string.
> * `default:<value>`: Sets a default value (e.g., `default:true`).
>
> You can copy the block above and edit it directly in your response.  
> If everything looks good, just say **'looks good'**."

**Wait for user response.**

---

### Prompt 4: Operations

> "Got it. Now, which **Operations** will be needed?
>
> You can choose any combination of:  
> `Create`, `GetById`, `Search`, `Update`, `Delete`.
>
> For example: `Create, GetById, Search`."

**Wait for user response.**

---

### Prompt 5: Infer Database Table Name

> "Based on the feature name, I suggest the database table name `{{tableName}}` (using Postgres naming conventions).
>
> Should we use `{{tableName}}` as the table name?"

**Wait for user confirmation.**

---

### Prompt 6: Searchable Fields

> "Which of the properties will be **Searchable Fields**?
>
> For example: `Username, Email`."

**Wait for user response.**

---

### Prompt 7: Authentication

> "What **Authentication** requirements does this feature have?
>
> * `Public` – No authentication required  
> * `Authenticated` – Requires login (**default**)  
> * `Roles: Admin,Manager` – Specific role(s) required  
> * `Policy: CanManageFeatures` – Custom policy required
>
> For example: `Authenticated` or `Roles: Admin`."

**Wait for user response.**

---

### Summarize & Confirm Phase 1

> "Thank you. Here is the plan based on your input:
>
> * **Feature**: `{{featureName}}`  
> * **Operations**: `{{operations}}`  
> * **Table**: `{{tableName}}`  
> * **Properties**: `{{properties}}`  
> * **Searchable Fields**: `{{searchableFields}}`  
> * **Authentication**: `{{authentication}}`
>
> **Phase 1 of 5 — Intelligent Feature Scoping: ✅ Complete.**
>
> Shall I proceed to **Phase 2 of 5 — Database Layer**?"

**Wait for explicit confirmation before moving to Phase 2.**

---

## Phase 2: Database Layer

> ---
> **Phase 2 of 5 — Database Layer**
> **Progress**
> ✅ Phase 1 — Intelligent Feature Scoping  
> ⏳ Phase 2 — Database Layer  
> ⬜ Phase 3 — Domain Layer  
> ⬜ Phase 4 — Acceptance Tests  
> ⬜ Phase 5 — API Layer
> ---

**Goal**: Create and register the database migration script, strictly following the specified naming convention and interactive flow.

**Reference**: For detailed database rules, see:  
[liquibase-best-practices.mdc](mdc:.cursor/rules/database-rules/liquibase-best-practices.mdc)  
[database-workflow.mdc](mdc:.cursor/rules/database-rules/database-workflow.mdc)

---

### Liquibase Changeset Flow

Explain the steps clearly:

> "Next, I’ll create and register the **Liquibase Changeset** for this feature.
>
> **Step 1: Generate Filename**  
> I’ll generate a filename using the required format:  
> `<timestamp>-{{featureName}}.xml`  
> For example: `$(date +%Y%m%d%H%M%S)-{{featureName}}.xml`.
>
> **Step 2: Propose Changeset Location**  
> I’ll propose creating the file at:  
> `Platform.Database/changelog/changes/`.
>
> **Step 3: Register Changeset**  
> After the file is created, I’ll update:  
> `Platform.Database/changelog/db.changelog-master.xml` to include it.
>
> **IMPORTANT RULES**:
> * The filename MUST be `<timestamp>-{{featureName}}.xml`.
> * The `<changeSet>` `id` attribute inside the XML MUST be unique, using the format:  
>   `<timestamp>-<description>` (e.g., `id="20250621224920-create-users-table"`).
> * DO NOT use sequential numbering (e.g., `002-feature.xml`).
>
> Ready to proceed with the **database changeset**?"

**Wait for confirmation, then perform the steps in order.**  
Once finished, say explicitly:

> "**Phase 2 of 5 — Database Layer: ✅ Complete.**
>
> Ready to move to **Phase 3 of 5 — Domain Layer**?"

---

## Phase 3: Domain Layer (File by File)

> ---
> **Phase 3 of 5 — Domain Layer**
> **Progress**
> ✅ Phase 1 — Intelligent Feature Scoping  
> ✅ Phase 2 — Database Layer  
> ⏳ Phase 3 — Domain Layer  
> ⬜ Phase 4 — Acceptance Tests  
> ⬜ Phase 5 — API Layer
> ---

**Goal**: Implement the core business logic, one class at a time, with strict verification against the source templates.

**CRITICAL NOTE ON JOIN TABLES**: If you are creating a junction/join table entity (e.g., `PersonaTopic`, `TopicTag`), do **NOT** expose it directly in the `DomainFacade`. Instead:

1. Create the join table manager internally.  
2. Add relationship management methods to the **primary entity’s manager** (e.g., `PersonaManager` or `TopicManager`).  
3. Expose these methods through the primary entity’s `DomainFacade` extension.  
4. Keep the join table manager internal and never reference it in `DomainFacade.cs`.

See [Join Table Encapsulation](docs/technical/reference/core/principles.md#join-table-encapsulation).

---

### Verification Process for Domain Layer Files

For **every** domain file:

1. **Generate**: Create the file by populating the relevant `.hbs` template with the feature details.  
2. **Verify**: Re-populate the same template in memory to create an "expected" version. Read the generated file from disk and compare.  
3. **Notify**: Report whether the generated file perfectly matches the template’s expected output. If there are discrepancies, highlight them.

Always tell the user, for example:

> "File `X` generated and verified: ✅ matches template"  
> or  
> "File `X` has differences from the template: …"

---

### 3.1 Domain Model

> "Now for the **Domain Model**.
>
> I’ll generate and then verify the code based on:
> * **File**: `{{featureName}}.cs`
> * **Location**: `Platform.Domain/Managers/Models/`
> * **Purpose**: Defines the core C# entity for `{{featureName}}`.
> * **Template**: `docs/technical/templates/domain-templates/DomainModel.hbs`
> * **Best Practices**: See [DomainModel.mdc](mdc:.cursor/rules/class-rules/domain-class-rules/DomainModel.mdc)
>
> Proceed with generating the **Domain Model**?"

---

### 3.2 Exceptions

> "Next, I’ll create the **Custom Exceptions** for this feature:
>
> * `{{featureName}}ValidationException.cs`
> * `{{featureName}}NotFoundException.cs`
> * `{{featureName}}DuplicateException.cs`
>
> **Location**: `Platform.Domain/Managers/Exceptions/`  
> **Templates**:
> * `docs/technical/templates/domain-templates/exceptions/ValidationException.hbs`
> * `docs/technical/templates/domain-templates/exceptions/NotFoundException.hbs`
> * `docs/technical/templates/domain-templates/exceptions/DuplicateException.hbs`
> **Best Practices**: See [Exception.mdc](mdc:.cursor/rules/class-rules/domain-class-rules/Exception.mdc)
>
> I’ll generate and verify each file against its template and report the results.
>
> Proceed with the **Exceptions**?"

---

### 3.3 Data Manager

> "Now, the **Data Manager**.
>
> * **File**: `{{featureName}}DataManager.cs`
> * **Location**: `Platform.Domain/Managers/DataLayer/DataManagers/`
> * **Purpose**: Handles all SQL queries for the `{{featureName}}` entity (including search and pagination where applicable).
> * **Template**: `docs/technical/templates/domain-templates/database/DataManager.hbs`
> * **Best Practices**: See [DataManager.mdc](mdc:.cursor/rules/class-rules/domain-class-rules/DataManager.mdc)
> * **Related Guides**:
>   * [Column Mapping](mdc:docs/technical/reference/guides/domain-guides/database/column-mapping.md)
>   * [Dapper Configuration](mdc:docs/technical/reference/guides/domain-guides/database/dapper-configuration.md)
>   * [Timezone Handling](mdc:docs/technical/reference/guides/domain-guides/database/timezone-handling.md)
>   * [Pagination](mdc:docs/technical/reference/guides/common-guides/pagination.md)
>
> Proceed with the **Data Manager**?"

---

### 3.4 Data Facades

> "Now I’ll create and verify the **Data Facade Extensions**:
>
> * **Files**: `DataFacade.{{featureName}}.cs`
> * **Purpose**: `partial` classes that expose the new data and domain logic.
> * **Template**: `docs/technical/templates/domain-templates/database/DataFacade.hbs`
> * **Best Practices**: See [DataFacade.mdc](mdc:.cursor/rules/class-rules/domain-class-rules/DataFacade.mdc)
>
> Proceed with the **Data Facade extensions**?"

---

### 3.5 Validator

> "Next, the **Validator**:
>
> * **File**: `{{featureName}}Validator.cs`
> * **Location**: `Platform.Domain/Managers/Validators/`
> * **Purpose**: Validation logic for the `{{featureName}}` entity.
> * **Template**: `docs/technical/templates/domain-templates/Validator.hbs`
> * **Best Practices**: See [Validator.mdc](mdc:.cursor/rules/class-rules/domain-class-rules/Validator.mdc)
> * **Related Guides**: [Validation Rules](mdc:docs/technical/reference/guides/domain-guides/validation-rules.md)
>
> Proceed with the **Validator**?"

---

### 3.6 Domain Manager

> "Now, the **Domain Manager**:
>
> * **File**: `{{featureName}}Manager.cs`
> * **Location**: `Platform.Domain/Managers/`
> * **Purpose**: Orchestrates validation and calls the `DataFacade`.
> * **Template**: `docs/technical/templates/domain-templates/Manager.hbs`
> * **Best Practices**: See [Manager.mdc](mdc:.cursor/rules/class-rules/domain-class-rules/Manager.mdc)
>
> Proceed with the **Domain Manager**?"

---

### 3.7 Domain Facade

> "Next, I’ll update the **Domain Facade**:
>
> * **File**: `DomainFacade.cs`
> * **Location**: `Platform.Domain/`
> * **Purpose**: Register the new manager with the main `DomainFacade`.
> * **Best Practices**: See [DomainFacade.mdc](mdc:.cursor/rules/class-rules/domain-class-rules/DomainFacade.mdc)
>
> I’ll insert the new manager’s private field, property, and `Dispose()` call directly.
>
> Proceed with updating `DomainFacade.cs`?"

---

### 3.8 Domain Facade Extension

> "Finally for the domain layer, I’ll create the **Domain Facade Extension**:
>
> * **Files**: `DomainFacade.{{featureName}}.cs`
> * **Purpose**: `partial` classes exposing the new domain operations via `DomainFacade`.
> * **Template**: `docs/technical/templates/domain-templates/DomainFacade.Base.hbs`
>
> Proceed with the **Domain Facade extension**?"

After all domain files are generated and verified, say:

> "**Phase 3 of 5 — Domain Layer: ✅ Complete.**
>
> Ready to move to **Phase 4 of 5 — Acceptance Tests**?"

---

## Phase 4: Acceptance Tests

> ---
> **Phase 4 of 5 — Acceptance Tests**
> **Progress**
> ✅ Phase 1 — Intelligent Feature Scoping  
> ✅ Phase 2 — Database Layer  
> ✅ Phase 3 — Domain Layer  
> ⏳ Phase 4 — Acceptance Tests  
> ⬜ Phase 5 — API Layer
> ---

**Goal**: Create and verify acceptance tests for the new domain logic, ensuring it works correctly before building the API.

**CRITICAL TESTING BOUNDARY**: Tests MUST ONLY interact with the `DomainFacade` interface.  
Do **NOT** create tests for individual managers, data managers, or validators.

---

### 4.1 Generate Test File

> "Now for the **Acceptance Test**.
>
> I’ll generate the test code with:
> * **File**: `DomainFacadeTests.{{featureName}}.cs`
> * **Location**: `Platform.AcceptanceTests/Domain/`
> * **Purpose**: Test the entire `{{featureName}}` feature via `DomainFacade` ONLY, against a real test database (including CRUD and relevant search/pagination where applicable).
> * **Template**: `docs/technical/templates/acceptance-test-templates/DomainFacadeTests.Base.hbs`
> * **Best Practices**: See [DomainFacadeTests.mdc](mdc:.cursor/rules/class-rules/acceptance-tests-class-rules/DomainFacadeTests.mdc)
> * **Related Guides**: [Test Cleanup](mdc:docs/technical/reference/guides/acceptance-test-guides/test-cleanup.md)
>
> Ready to generate the **acceptance test file**?"

---

### 4.2 Run & Report Tests

> "After generating the test file, I’ll run the acceptance tests (`dotnet test`) to verify the domain logic.
>
> I’ll report:
> * Whether the tests passed or failed.
> * Any failing test names and their error messages.
>
> Proceed with running the acceptance tests now?"

After running, summarize:

> "**Phase 4 of 5 — Acceptance Tests: ✅ Complete** (all tests passing).  
> Ready to move to **Phase 5 of 5 — API Layer**?"

(If tests fail, report and pause until the user addresses issues.)

---

## Phase 5: API Layer (File by File)

> ---
> **Phase 5 of 5 — API Layer**
> **Progress**
> ✅ Phase 1 — Intelligent Feature Scoping  
> ✅ Phase 2 — Database Layer  
> ✅ Phase 3 — Domain Layer  
> ✅ Phase 4 — Acceptance Tests  
> ⏳ Phase 5 — API Layer
> ---

**Goal**: Expose the verified domain logic via a public HTTP endpoint, strictly using the provided templates.

For each file, generate code **only** by populating the specified `.hbs` template.  
Do **not** use existing files as informal examples.

---

### 5.1 Resource Model

> "First, the **Resource Model**:
>
> * **File**: `{{featureName}}Resource.cs`
> * **Location**: `Platform.Api/ResourcesModels/`
> * **Purpose**: Defines the JSON shape for API requests/responses.
> * **Template**: `docs/technical/templates/api-templates/ResourceModel.hbs`
> * **Best Practices**: See [ResourceModel.mdc](mdc:.cursor/rules/class-rules/api-class-rules/ResourceModel.mdc)
>
> Proceed with the **Resource Model**?"

---

### 5.2 Mapper

> "Next, the **Mapper**:
>
> * **File**: `{{featureName}}Mapper.cs`
> * **Location**: `Platform.Api/Mappers/`
> * **Purpose**: Maps between the Domain Model and the Resource Model.
> * **Template**: `docs/technical/templates/api-templates/Mapper.hbs`
> * **Best Practices**: See [Mapper.mdc](mdc:.cursor/rules/class-rules/api-class-rules/Mapper.mdc)
>
> Proceed with the **Mapper**?"

---

### 5.3 Controller

> "Finally, the **Controller**:
>
> * **File**: `{{featureName}}Controller.cs`
> * **Location**: `Platform.Api/Controllers/`
> * **Purpose**: Public HTTP entry point for the `{{featureName}}` feature.
> * **Template**: `docs/technical/templates/api-templates/Controller.hbs`
> * **Best Practices**:
>   * [Controller.mdc](mdc:.cursor/rules/class-rules/api-class-rules/Controller.mdc)
>   * [Error Handling](mdc:docs/technical/reference/guides/api-guides/error-handling.md)
>   * [Pagination](mdc:docs/technical/reference/guides/common-guides/pagination.md)
>
> **Authentication will be applied as:**
> * `Public` → No `[Authorize]` attribute (use `[AllowAnonymous]` if other controllers require auth).
> * `Authenticated` → `[Authorize]` at controller level.
> * `Roles: X,Y` → `[Authorize(Roles = "X,Y")]` at controller level.
> * `Policy: X` → `[Authorize(Policy = "X")]` at controller level.
>
> I’ll generate the controller strictly from the template, wiring up CRUD, search, and pagination operations based on the earlier choices.
>
> Proceed with the **Controller**?"

---

## Workflow Complete

At the end, explicitly announce completion:

> "**Workflow Complete ✅**
>
> All files for the `{{featureName}}` feature have been created and verified:
> * Phase 1 — Intelligent Feature Scoping: ✅
> * Phase 2 — Database Layer: ✅
> * Phase 3 — Domain Layer: ✅
> * Phase 4 — Acceptance Tests: ✅
> * Phase 5 — API Layer: ✅
>
> Next typical actions:
> * Open a PR.
> * Request review from the appropriate owner.
> * Run any additional integration / smoke tests if required by the team’s checklist."
