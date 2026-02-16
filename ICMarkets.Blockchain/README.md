# Blockchain Data Tracker

Blockchain Tracker
Simple Web API to pull and store blockchain metadata from BlockCypher. Built with a clean architecture to keep the code organized and easy to test.

Getting Started

## How to Run the Project

### Using Visual Studio
1. Open the `.sln` file.
2. Ensure the **API** project is set as the Startup Project.
3. Press **F5**. The database will be created automatically on startup.
4 The app will automatically create and migrate the BlockchainData.db file on startup so you don't have to run manual EF commands.
5. Swagger will open at `/swagger/index.html`.

### Using Docker
1. Open a terminal in the root folder.
2.Build: docker build -t blockchain-app .
3.Run: docker run -d -p 8080:8080 --name crypto-tracker blockchain-app
4.Swagger: http://localhost:8080/swagger
5.Health: http://localhost:8080/health

## API Endpoints
* `GET /api/blockchain/fetch/{path}`: Fetches data from BlockCypher (e.g., `btc/main`, `dash/main`) and saves it for example /api/blockchain/fetch/btc/main.
* `GET /api/blockchain/history`: Returns all stored records, newest first.
* `GET /health`: Basic health check for system monitoring.

## Design Decisions
* **SQLite:** Chosen for simplicity and ease of review (no external DB setup required).
* **Manual Validation:** Kept validation logic inside the service to maintain a lightweight codebase.
* **AsNoTracking:** Used for history queries to improve read performance.
* **Logging:** Injected ILogger into the services and controllers. Useful for tracking what’s happening inside the container during API fetches.
* **Tests:** Included xUnit tests with Moq to verify the service logic without hitting the real BlockCypher API.