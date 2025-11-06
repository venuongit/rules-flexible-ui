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
- change if this port is already used
