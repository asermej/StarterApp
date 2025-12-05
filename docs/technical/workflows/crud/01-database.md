# Phase 1 – Database Schema

**Goal**: Define your entity's fields and create the database migration.

**Reference**: [liquibase-best-practices.mdc](mdc:.cursor/rules/database-rules/liquibase-best-practices.mdc) | [database-workflow.mdc](mdc:.cursor/rules/database-rules/database-workflow.mdc)

---

## Before You Begin

Review existing tables in [`database-schema-summary.md`](mdc:docs/technical/meta-data/database-schema-summary.md) for patterns and potential relationships.

> Standard audit fields (id, created_at, updated_at, is_deleted, etc.) are included automatically.

---

### Step 1 – Define Fields

Skip if adding endpoints to an existing entity.

Propose business fields using simple names:

> For **{{entityName}}**, I'd suggest these fields:
> - {{guessedField1}}
> - {{guessedField2}}
> - {{guessedField3}}
>
> What fields does your entity need? (Just list names, I'll handle the types)

**Wait for user's field list. Store as `{{fieldNames}}`.**

---

### Step 2 – Propose Types

Convert field names to database types:

> Here are my type guesses for **{{entityName}}**:
>
> | Field | Type | Notes |
> |-------|------|-------|
> | {{field1}} | {{guessedType1}} | {{notes1}} |
> | {{field2}} | {{guessedType2}} | {{notes2}} |
>
> Any adjustments?

**Type inference patterns:**
- `name`, `title`, `label` → `varchar(100)`
- `description`, `content` → `varchar(500)` or `text`
- `email` → `varchar(255)`
- `phone` → `varchar(20)`
- `url`, `image_url` → `varchar(500)`
- `price`, `amount`, `cost` → `decimal(10,2)`
- `count`, `quantity` → `integer`
- `is_*`, `has_*` → `boolean`
- `*_at`, `*_date` → `timestamp`

**Wait for user confirmation. Store as `{{columns}}`.**

---

### Step 3 – Propose Relationships

Check existing tables and ask about relationships in plain language.

**Belongs-to (FK on this table):**

> Does a **{{entityName}}** belong to a single:
> - User?
> - {{existingTable1}}?
> - {{existingTable2}}?
>
> (Adds a foreign key column to your table)

**Has-many (FK on other table):**

> Can a **{{entityName}}** have multiple:
> - Addresses?
> - Items?
>
> (Creates a separate table with a {{entity_name}}_id foreign key)

**Many-to-many:**

> Can a **{{entityName}}** have multiple **{{otherEntity}}s**, AND can a **{{otherEntity}}** have multiple **{{entityName}}s**?
>
> (Creates a linking table: `{{entity_name}}_{{other_entity}}s`)

**Wait for user's relationship choices. Store as `{{relationships}}`.**

---

### Step 4 – Propose Constraints

Suggest which fields should be required, unique, or have defaults:

> Here's what I'm thinking for constraints:
>
> | Field | Required | Unique | Default |
> |-------|----------|--------|---------|
> | {{field1}} | yes | no | — |
> | {{field2}} | no | no | — |
> | {{field3}} | yes | yes | — |
>
> Any changes?

**Wait for user confirmation. Store as `{{constraints}}`.**

---

### Step 5 – Generate Migration

1. Generate timestamp-based filename: `<timestamp>-{{entityName}}.xml`
2. Create changeset in `Platform.Database/changelog/changes/`
3. Register in `db.changelog-master.xml`

---

### Step 6 – Apply Migration

```bash
just db-update
just update-db-context
```

> **Phase 1 of 4 — Database Schema: ✅ Complete.**

Return to orchestrator for Phase 2.
