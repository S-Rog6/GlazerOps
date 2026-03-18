# Supabase + GitHub Pages Operational Readiness Plan

## 1) Current-state evaluation

### What is already in place
- The app is already a Blazor WebAssembly SPA with Supabase client registration and session persistence wiring (`SupabaseLocalStorageSessionHandler`).
- Google OAuth sign-in and callback routes are implemented (`/login` and `/auth/callback`).
- The layout checks auth state and redirects unauthenticated users to `/login`.
- Supabase-backed reads are already used for jobs and related views.
- The repository includes starter SQL scripts and explicit intent to host the app as static content (including `404.html` for SPA fallback).

### Gaps and risks to address before production
1. **Schema naming drift / contract drift**  
   There are inconsistent table names between models, SQL scripts, and README references (for example `po_*`, `job_*`, `owner_file_jobs` vs `user_file_jobs`, `inspected_buckets` vs `expected_buckets`). This can break queries and RLS assumptions.
2. **RLS policy implementation is incomplete**  
   RLS is enabled in scripts, but concrete `CREATE POLICY` statements and storage policies are not represented in repo scripts.
3. **Auth callback robustness**  
   Callback currently swallows all exceptions and always redirects to `/`, which makes diagnosing auth and token issues hard.
4. **Storage feature not operationalized**  
   There are data models for bucket metadata, but no end-to-end file upload/download path, bucket bootstrap, or storage policy scripts.
5. **GitHub Pages deployment automation is missing**  
   No workflow currently builds/publishes to Pages, and there is no documented base-path strategy for OAuth redirects in project/repo pages mode.
6. **Secrets/config strategy needs hardening**  
   `wwwroot/appsettings.json` currently contains Supabase URL + anon key. Even if anon key is expected to be public, production should define explicit environment handling and domain allowlists.

---

## 2) Target operational architecture

### Auth
- Supabase Auth with Google provider.
- Redirects must include:
  - local dev callback (`http://localhost:<port>/auth/callback`)
  - production callback for Pages (`https://<org>.github.io/<repo>/auth/callback` if project pages)
- Session persisted in browser localStorage (already present) and refreshed on app load.

### Database
- Canonical schema versioned as SQL migrations in repo.
- All app tables RLS-enabled with least-privilege policies tied to `auth.uid()`.
- Optionally expose read-focused `vw_*` views used by UI cards.

### Storage
- Named buckets (private by default) created via SQL/migration.
- Path convention: `<user_id>/<job_id>/<filename>` to simplify policy checks.
- Signed URL strategy for temporary download access from browser.

### Hosting (GitHub Pages)
- Build as static WebAssembly publish output.
- Deploy with GitHub Actions to Pages.
- SPA fallback through `404.html` copy of `index.html`.
- Correct `<base href>` and auth callback URL for repo pages path.

---

## 3) Delivery plan (phased)

## Phase 0 — Baseline and environment inventory (0.5 day)
1. Confirm hosting mode: **user/org pages** (`/<root>`) vs **project pages** (`/<repo>/`).
2. Freeze canonical Supabase project/environment names: `dev`, `staging`, `prod`.
3. Document allowed redirect URLs and current Supabase auth settings.

**Exit criteria**
- Written environment matrix (URLs, project refs, callback URLs).
- Team agreement on pages base path.

## Phase 1 — Schema normalization + migrations (1–2 days)
1. Pick canonical table names and align all C# models + SQL scripts.
2. Create migration set for:
   - core tables (`jobs`, `sites`, `job_contacts`, `job_notes`, `job_schedule_dates`)
   - auxiliary tables (`expected_buckets`, `user_file_jobs`) or revised canonical names
   - views required by current UI (`vw_job_card`, `vw_job_card_drawer`)
3. Add constraints/indexes and backfill scripts for renamed objects if needed.

**Exit criteria**
- Fresh Supabase project can be bootstrapped from repo migrations only.
- App loads Jobs page without schema/table-name runtime errors.

## Phase 2 — Auth hardening (1 day)
1. Keep current OAuth flow but improve callback handling:
   - explicit error reporting/logging
   - preserve/restore intended return route
2. Add minimal auth service wrapper for `GetCurrentUser`, `SignOut`, and session refresh checks.
3. Add route guard behavior for unauthorized API responses.

**Exit criteria**
- Login/logout works on localhost and Pages URL.
- Invalid callback/token flow shows actionable UI error.

## Phase 3 — RLS policies (1–2 days)
1. Define ownership model per table (`owner_user_id` or join-based ownership).
2. Create explicit policy SQL for `select/insert/update/delete`.
3. Add test SQL snippets (or smoke checks) validating:
   - authorized user can read/write own rows
   - unauthorized user cannot access other rows

**Exit criteria**
- Policies committed and reproducible.
- Manual verification with two test users passes ownership checks.

## Phase 4 — Storage operationalization (1–2 days)
1. Create buckets and storage policies.
2. Implement upload/download/list primitives in app service layer.
3. Link files to jobs via metadata table (`user_file_jobs` or canonical replacement).
4. Add basic UI actions for upload + open/download.

**Exit criteria**
- User can upload file to a job and retrieve it with policy-compliant access.
- Non-owner access is denied by storage policy.

## Phase 5 — GitHub Pages CI/CD (0.5–1 day)
1. Add workflow to:
   - restore/build/publish WASM app
   - set correct base path
   - deploy `wwwroot` publish output to Pages
2. Ensure `404.html` fallback is present in deployed output.
3. Verify Supabase auth allowlist includes deployed Pages callback.

**Exit criteria**
- Push to main auto-deploys.
- Reload on deep link routes works.
- OAuth login works on deployed site.

## Phase 6 — Observability + runbook (0.5 day)
1. Add operational runbook for common issues:
   - redirect mismatch
   - RLS denies
   - storage access denied
2. Capture key diagnostics in UI/dev console for auth + API failures.

**Exit criteria**
- On-call/dev can triage top failure modes in <15 minutes.

---

## 4) Definition of done (production-ready)
- Deterministic schema migrations in repo.
- All app-used tables/buckets protected by reviewed RLS/storage policies.
- Auth login/callback/logout verified for local + Pages production URL.
- CI-driven GitHub Pages deployment with SPA fallback.
- Documented environment variables, allowlists, and rollback steps.

---

## 5) Recommended immediate next actions (this week)
1. Resolve schema naming drift first (highest leverage blocker).
2. Add migration folder structure and move current ad-hoc SQL into ordered migrations.
3. Implement Pages deploy workflow and verify callback URL pathing.
4. Then complete RLS + storage policies and hook up file flows.

This sequence minimizes rework by stabilizing data contracts before expanding auth/storage surface area.
