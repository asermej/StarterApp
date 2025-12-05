#!/bin/bash
# Generate database schema context in Markdown format
# Usage: ./generate-schema-context.sh > docs/technical/meta-data/database-schema-summary.md

set -e

# Use environment variables with defaults for local dev
PGHOST="${PGHOST:-localhost}"
PGUSER="${PGUSER:-postgres}"
PGDATABASE="${PGDATABASE:-starter}"
export PGPASSWORD="${PGPASSWORD:-postgres}"

# Helper function to run psql queries
run_query() {
    psql -h "$PGHOST" -U "$PGUSER" -d "$PGDATABASE" -t -A -c "$1"
}

# Header
echo "# Database Schema Summary"
echo ""
echo "Generated: $(date '+%Y-%m-%d %H:%M:%S %Z')"
echo ""
echo "> **Regenerate**: \`just update-db-context\`"
echo ""
echo "---"
echo ""

# Get list of tables
tables=$(run_query "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' AND table_type = 'BASE TABLE' ORDER BY table_name;")

# Table of Contents
echo "## Tables"
echo ""
echo "| Table | Columns |"
echo "|-------|---------|"

for table in $tables; do
    col_count=$(run_query "SELECT COUNT(*) FROM information_schema.columns WHERE table_name = '$table' AND table_schema = 'public';")
    echo "| [$table](#$table) | $col_count |"
done

echo ""
echo "---"
echo ""

# Detailed schema for each table
for table in $tables; do
    echo "## $table"
    echo ""
    echo "| Column | Type | Nullable | Default | Constraints |"
    echo "|--------|------|----------|---------|-------------|"
    
    # Get columns with their details
    run_query "
        SELECT 
            c.column_name,
            c.data_type || COALESCE('(' || c.character_maximum_length::text || ')', ''),
            c.is_nullable,
            COALESCE(SUBSTRING(c.column_default FROM 1 FOR 30), ''),
            COALESCE(
                (SELECT string_agg(tc.constraint_type, ', ')
                 FROM information_schema.key_column_usage kcu
                 JOIN information_schema.table_constraints tc 
                   ON kcu.constraint_name = tc.constraint_name 
                  AND kcu.table_schema = tc.table_schema
                 WHERE kcu.table_name = c.table_name 
                   AND kcu.column_name = c.column_name
                   AND kcu.table_schema = 'public'),
                ''
            )
        FROM information_schema.columns c
        WHERE c.table_name = '$table'
          AND c.table_schema = 'public'
        ORDER BY c.ordinal_position;
    " | while IFS='|' read -r col_name data_type nullable default_val constraints; do
        echo "| $col_name | $data_type | $nullable | $default_val | $constraints |"
    done
    
    echo ""
    
    # Foreign keys for this table
    fks=$(run_query "
        SELECT 
            kcu.column_name || ' → ' || ccu.table_name || '.' || ccu.column_name
        FROM information_schema.table_constraints tc
        JOIN information_schema.key_column_usage kcu 
          ON tc.constraint_name = kcu.constraint_name
        JOIN information_schema.constraint_column_usage ccu 
          ON tc.constraint_name = ccu.constraint_name
        WHERE tc.table_name = '$table' 
          AND tc.constraint_type = 'FOREIGN KEY'
          AND tc.table_schema = 'public';
    ")
    
    if [ -n "$fks" ]; then
        echo "**Foreign Keys:**"
        echo "$fks" | while read -r fk; do
            echo "- $fk"
        done
        echo ""
    fi
done

