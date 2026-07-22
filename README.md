# 🚀 Employee Request Management System (ERMS) - API

A robust, enterprise-grade RESTful API built with **.NET 10** following **Onion Architecture (Clean Architecture)** principles. This system manages employee requests, multi-tier approval workflows, user authentication, and comprehensive audit logging.

---

## 🌟 Key Features

- **Onion Architecture**: Clear separation of concerns with Domain, Application, Infrastructure, and API layers.
- **JWT Authentication & Authorization**: Secure identity management with role-based access control and token-based authentication.
- **Request & Approval Workflows**: End-to-end management for employee requests and approval/rejection operations.
- **Audit Logging**: Automatic tracking of system activities, actions, and decision histories for compliance and accountability.
- **Entity Framework Core**: Code-First approach with SQL Server integration and automatic database migrations.
- **Swagger / OpenAPI**: Interactive API documentation for seamless testing and frontend integration.

---

## 🏗️ Tech Stack & Architecture

- **Framework**: .NET 10 Web API
- **ORM**: Entity Framework Core 10
- **Database**: Microsoft SQL Server
- **Authentication**: JSON Web Tokens (JWT) & ASP.NET Core Identity / Bearer Schemes
- **Documentation**: Swagger UI / OpenAPI (Swashbuckle)
- **Design Pattern**: Onion / Clean Architecture, Repository & Service Patterns, Dependency Injection

🚀 Getting Started
Prerequisites
- .NET 10 SDK

- SQL Server (LocalDB, SSMS, or Docker Instance)

- Visual Studio 2022+ or VS Code


Installation & Setup
1 - Clone the Repository

git clone [https://github.com/onlystappen/ERMS.git](https://github.com/onlystappen/ERMS.git)
cd ERMS

2- Configure Database Connection
Update the connection string in ERMS.Api/appsettings.json:


"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=ERMSDb;Trusted_Connection=True;TrustServerCertificate=True;"
}

3- Apply Database Migrations
Open Package Manager Console or Terminal and run:


dotnet ef database update --project ERMS.Infrastructure --startup-project ERMS.Api

4- Run the Application

dotnet run --project ERMS.Api

5 - Access Swagger UI
Open your browser and navigate to:


http://localhost:5118/swagger


🛡️ License
Distributed under the MIT License. See LICENSE for more information.

👨‍💻 Created by Yahya Arda Sandıkçı (@onlystappen)

---

## 📁 Project Structure

```text
ERMS/
├── ERMS.Domain/          # Core Domain Entities, Enums, and Value Objects
├── ERMS.Application/     # Interfaces, DTOs, Business Logic, and Services
├── ERMS.Infrastructure/  # DbContext, Migrations, JWT Generator, External Services
└── ERMS.Api/             # Controllers, Middlewares, Configuration, and Program.cs
