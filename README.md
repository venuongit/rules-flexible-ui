rules-flexible-ui/
├── backend/
│   └── RulesApi/
│       ├── Program.cs
│       ├── RulesApi.csproj          # targets net8.0
│       ├── Controllers/RulesController.cs  # GET + PUT /reorder
│       ├── Models/Rule.cs
│       ├── Data/RulesDbContext.cs   # EF Core + SQLite
│       └── Properties/launchSettings.json  # http://localhost:5025
└── frontend/
    └── rules-client/
        ├── package.json
        ├── vite.config.js
        ├── index.html
        └── src/
            ├── main.jsx
            └── App.jsx              # drag/drop UI calling the API
