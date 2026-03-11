# GlazerOps

GlazerOps is a field-focused job tracking app for commercial glazing teams.

It is built as a `Blazor WebAssembly` app with a Supabase backend so it can run in the browser and be deployed with low hosting overhead.

## Current Stack

- Frontend: `Blazor WebAssembly` (`.NET 10`)
- UI: `MudBlazor`
- Backend: `Supabase` (`PostgreSQL` + auth)
- Hosting target: static web hosting (for example GitHub Pages)

## Current Features

- View jobs with linked site details
- View job contacts (resolved from `jobs.contact_ids`)
- View scheduled dates per job
- View pinned and recent job notes
- Search/filter jobs by key fields

## Project Structure

```text
GlazerOps/
├─ Components/        # Reusable Blazor UI components
├─ Models/
│  ├─ Data/           # Supabase/PostgREST data models
│  └─ Domain/         # Domain/business models
├─ Pages/             # Routeable Blazor pages
├─ Services/          # Mapping and integration services
├─ wwwroot/           # Static files and app settings
└─ Program.cs         # App bootstrap and DI setup
```

## Supabase Schema (Current)

Main tables in active schema:

- `jobs`
- `sites`
- `job_contacts`
- `job_notes`
- `job_schedule_dates`
- `expected_buckets`
- `user_file_jobs`

### Key relationships and notes

- `jobs.site_id -> sites.id`
- `job_notes.job_id -> jobs.id`
- `job_schedule_dates.job_id -> jobs.id`
- Job contacts are associated through `jobs.contact_ids` (array of contact IDs).

## Local Development

### Prerequisites

- `.NET SDK 10`
- Supabase project with required tables

### Configure settings

Set Supabase values in `GlazerOps/wwwroot/appsettings.json`:

- `Supabase:Url`
- `Supabase:AnonKey`

### Run

```bash
dotnet restore
dotnet run --project GlazerOps/GlazerOps.csproj
```

## Status

Active development.

## License

MIT
