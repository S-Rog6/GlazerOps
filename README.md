# GlazerOps


GlazerOps is a lightweight field-focused job tracking application built for commercial glazing operations.
The goal is to give installers, foremen, and office staff a simple way to track jobs, notes, contacts, and site information from any device.

The application is designed to run as a Blazor WebAssembly Progressive Web App (PWA) with a Supabase backend, allowing it to be installed on phones, tablets, and desktops while remaining inexpensive to host.


---

Core Goals

GlazerOps focuses on the everyday workflow of a glazing crew:

• Track jobs and job sites
• Store job notes and updates
• Attach photos from the field
• Track schedules and assigned installers
• Store site contacts and phone numbers
• Quickly access active jobs from a mobile device

The design prioritizes:

• Speed in the field
• Minimal typing
• Mobile usability
• Low hosting cost


---

Tech Stack

Frontend

• Blazor WebAssembly (.NET)
• Progressive Web App (installable on mobile and desktop)

Backend

• Supabase

PostgreSQL database

Authentication

Row Level Security


Hosting

• GitHub Pages (static hosting for the Blazor client)

Authentication

• Google OAuth
• Apple OAuth (planned)


---

Application Features

Jobs

Stores active job information.

Typical fields:

Job Name

PO Number

Location

Assigned Crew

Scheduled Dates



---

Sites

A normalized table storing address and location information so multiple jobs can reference the same site.

Typical fields:

Site Name

Full Address

City / State / Zip



---

Job Notes

Field notes tied to a job.

Features:

timestamped notes

pinned notes

quick updates from the field

photo support (planned)



---

Job Scheduling

Jobs can contain multiple scheduled work dates using a separate schedule table.

This allows:

• multiple visits
• installation phases
• punch list days


---

Contacts

Each site can contain multiple contacts.

Examples:

General Contractor

Site Superintendent

Property Manager

Security Desk



---

Project Structure

GlazerOps
│
├─ Data
│   database models
│
├─ Models
│   shared object models
│
├─ Services
│   Supabase access services
│
├─ Pages
│   application UI pages
│
├─ Components
│   reusable UI components
│
└─ wwwroot
    static assets


---

Database Tables

Main tables currently used:

sites
jobs
job_notes
job_schedule_dates
site_contacts

Relationships:

sites
  └── jobs
        ├── job_notes
        └── job_schedule_dates

sites
  └── site_contacts


---

Running Locally

Requirements:

• .NET SDK (8 or newer)
• Visual Studio / VS Code

Run the app:

dotnet restore
dotnet run

Then open:

https://localhost:xxxx


---

Deployment

The application deploys automatically using GitHub Actions.

Deployment target:

GitHub Pages

Published URL example:

https://username.github.io/GlazerOps

The workflow builds the Blazor WebAssembly project and publishes the static output to the Pages branch.


---

Future Features

Planned improvements:

• Photo attachments
• Job progress tracking
• Crew assignment
• Offline data caching
• Push notifications
• Mobile-first dashboard
• Site map integration
• Time tracking


---

Project Status

This project is currently in active development.

The initial version focuses on:

• authentication
• database structure
• job tracking core features


---

License

MIT License


---

If you want, I can also give you a much better README that makes the repo look like a serious production project (badges, architecture diagram, screenshots, install instructions, etc.). It makes a huge difference when people view the repo.
