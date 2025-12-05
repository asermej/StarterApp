# CRUD Endpoint Workflow

**MANDATORY PROCESS**: Creates CRUD + Search endpoints for new or existing entities.

---

## Step 1 – New or Existing Entity?

Ask:

> **a. New entity** or **b. Existing entity**?

---

## Step 2 – Entity Selection

### If Existing Entity

1. Scan `apps/api/Platform.Domain/Managers/Models/*.cs`
2. Filter out files containing "Request", "Result", or "Response" in the name
3. Present the list:

> Select an entity:
> 1. User
> 2. Persona
> 3. Chat
> ...

**Wait for selection.**

Then scan `apps/api/Platform.Api/Controllers/{EntityName}Controller.cs` for existing endpoints:

- **Create**: `[HttpPost]` without route param
- **GetById**: `[HttpGet("{id}")]`
- **Search**: `[HttpGet]` with `[FromQuery]` parameter
- **Update**: `[HttpPut("{id}")]` or `[HttpPatch("{id}")]`
- **Delete**: `[HttpDelete("{id}")]`

Display:

> **{EntityName}** endpoints:
> - [x] Create
> - [x] GetById
> - [ ] Search
> - [x] Update
> - [ ] Delete
>
> Missing: Search, Delete
> Which would you like to create?

If all exist:

> All CRUD + Search endpoints exist for {EntityName}. Nothing to add.

**Wait for selection of endpoints to create.**

---

### If New Entity

Ask:

> Entity name? (e.g., `JobRequisition`, `TopicTag`)

**Wait for name.**

Then ask:

> Which endpoints? (1. Create, 2. GetById, 3. Search, 4. Update, 5. Delete, or 6. "All")

**Wait for selection.**

---

## Step 3 – Authentication

> Authentication for these endpoints?
> - 1. `Public`
> - 2. `Authenticated` (default)
> - 3. `Roles: Admin,Manager`

**Wait for selection.**

---

## Step 4 – Summary & Proceed

> **Entity**: {EntityName} ({New/Existing})
> **Endpoints**: {selectedEndpoints}
> **Auth**: {authentication}
>
> Ready to proceed?

**Wait for confirmation.**

---

## Phases

After confirmation, proceed through 4 phases in order:

1. Database / Liquibase – [Phase 1](mdc:docs/technical/workflows/crud/01-database.md)
2. Domain Layer – [Phase 2](mdc:docs/technical/workflows/crud/02-domain.md)
3. Acceptance Tests – [Phase 3](mdc:docs/technical/workflows/crud/03-acceptance-tests.md)
4. API Layer – [Phase 4](mdc:docs/technical/workflows/crud/04-api.md)

---

## Phase Progress Display

At each phase start, show:

> **Phase X of 4 — {Phase Name}**
> ✅ Phase 1 : ⏳ Phase 2 : ⬜ Phase 3 : ⬜ Phase 4

Use: ✅ completed | ⏳ current | ⬜ not started

---

## Rules

1. Follow each phase workflow **verbatim**.
2. Wait for user confirmation before moving to the next phase.
3. After completing a phase:
   > Phase X of 4 — {Name}: ✅ Complete.
   > Continue to Phase Y?

