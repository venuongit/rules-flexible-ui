# Rules Flexible UI (ASP.NET Core 8 + SQLite + React/Vite)

## System checks (run these on your Mac)
dotnet --list-sdks       # should show 8.x
node -v && npm -v        # should show versions
sqlite3 --version        # optional, for CLI

## Backend
cd backend/RulesApi
dotnet restore
dotnet run
# API at http://localhost:5025
# SQLite file auto-created: rules.db

## Frontend
cd frontend/rules-client
npm install
npm run dev
# open http://localhost:5173

## Overwrites
- backend/RulesApi/Properties/launchSettings.json sets port 5025
- change if this port is already use
- Project Overview (Business Explanation)

“This project was designed to help business users dynamically manage and reprioritize rules used by automation bots or decision engines. Previously, any rule change required developer intervention. So, the goal was to build a flexible UI and API system where business users themselves could reorder or edit rules and instantly reflect those updates in the backend database.”

Key business value:

Eliminated manual IT dependency for rule updates

Improved business agility — users can reprioritize logic based on new compliance or operational needs

Ensured data consistency — all updates flow through controlled APIs into the database

⚙️ Technical Architecture Summary

“The solution consists of a full-stack web application built using .NET 8 (ASP.NET Core) for the backend API, SQLite as a lightweight database, and React (Vite) for the frontend UI. The two layers communicate through REST APIs.”

Architecture flow:

React (Frontend)  →  ASP.NET Core API  →  SQLite Database


Components:

Backend (.NET 8 Web API)

Exposes REST endpoints:

GET /api/rules → fetches list of rules

PUT /api/rules/reorder → updates rule priorities

Uses Entity Framework Core + SQLite for persistence

Seeds sample data automatically on first run

Supports CORS for frontend access and Swagger for testing

Fully portable (cross-platform .NET setup)

Frontend (React + Vite)

Displays list of rules with priorities

Uses @hello-pangea/dnd library for drag-and-drop reordering

Includes a “Save Changes” button that sends reordered priorities to the API

Responsive and intuitive — designed for business users (no technical skills needed)

Database (SQLite)

Stores rule definitions and priorities

Automatically created (rules.db) during app startup

Updates are persisted after each “Save” action from the UI

🧠 Key Features Demonstrated

Drag-and-Drop Reordering: Smoothly rearranges rules visually and reassigns priority numbers dynamically.

API-Driven Persistence: Any reorder action updates the SQLite database in real-time.

CORS-enabled Cross-Origin Communication: Frontend (port 5173) and backend (port 5025) communicate securely.

Auto-seeding: The system initializes with default rule sets on first run.

RESTful Design: Clean separation between frontend presentation and backend logic.

🧰 Tech Stack
Layer	Technology
Frontend	React (Vite) + @hello-pangea/dnd
Backend	ASP.NET Core 8 Web API
Database	SQLite with Entity Framework Core
Tools	Visual Studio Code, Node.js, .NET SDK 8
API Testing	Swagger (auto-generated docs)
🚀 Deployment / Run Steps

Backend:

cd backend/RulesApi
dotnet restore
dotnet run


Runs API on http://localhost:5025

Frontend:

cd frontend/rules-client
npm install
npm run dev


Runs UI on http://localhost:5173
