# 🥾 EG.Walks.API

A modern, modular ASP.NET Core Web API to manage walking trails in Egypt.
This project follows **Clean Architecture** principles and demonstrates professional API design with JWT Authentication, Repository Pattern, and an MVC frontend.

---

## 📸 Preview

> You can test the API endpoints using Swagger UI  
> `https://localhost:{port}/swagger`

![image](https://github.com/user-attachments/assets/f8c19d08-5ffc-4df6-885e-885518bbe45a)

![image](https://github.com/user-attachments/assets/78145c76-72f0-4eb1-86a1-d7bb38ff8089)

![image](https://github.com/user-attachments/assets/8cd64a87-9442-4ff1-b942-11456da38b32)

![image](https://github.com/user-attachments/assets/8f95f4ba-7102-41be-a5dd-482192e9044b)

---

## 🧠 Features

- ✅ Built with **ASP.NET Core Web API (.NET 8)**  
- ✅ Implements **Clean Architecture Structure**  
- ✅ **Repository Pattern** & **Unit of Work** for data access  
- ✅ 🔐 **JWT Authentication** (token-based login system)  
- ✅ 🌍 Full CRUD for **Walks**, **Regions**, and **Difficulties**  
- ✅ 🔎 **Filtering**, 🔃 **Sorting**, and 📄 **Pagination** for Walk listings  
- ✅ 🧼 Structured validation using **FluentValidation**  
- ✅ 📦 Code-First approach using **Entity Framework Core**  
- ✅ 🖥️ Separate **MVC UI project (EG.Walks.UI)** for frontend  
- ✅ 🧪 Swagger UI for testing & documentation  

---

## 🧱 Tech Stack

| Tech | Purpose |
|------|---------|
| .NET 8 | Backend framework |
| ASP.NET Core Web API | RESTful services |
| EF Core | ORM for database |
| SQL Server | Database backend |
| AutoMapper | DTO & object mapping |
| FluentValidation | Validation |
| JWT | Secure Authentication |
| MVC | UI for admin management |
| Swagger | API testing & docs |

---

## 🚀 Getting Started

### 1️⃣ Clone the Repository
```bash
git clone https://github.com/Alber-Ashraf/EG.Walks.API.git
cd EG.Walks.API
```
### 2️⃣ Update the Database
Make sure you have SQL Server running.
Configure the connection string in appsettings.json: 
```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=EGWalksDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```
Apply migrations and update the database:
```bash
dotnet ef database update
```
### 3️⃣ Run the Application
```bash
dotnet run
```
Then navigate to:
```bash
https://localhost:{port}/swagger

```

---

🔐 JWT Authentication
- Call POST /api/auth/login with valid credentials.
- Copy the returned accessToken.

- In Swagger UI, click Authorize, and enter:

```bash
Bearer YOUR_TOKEN_HERE
```
- Now, all protected endpoints will be accessible!
---

## 📁 Folder Structure
```
📦 EG.Walks.API
├── 📂 EG.Walks                                        # Main ASP.NET Core Web API project
│   ├── 📂 Contracts                                   # Contains JWT-related models (e.g., TokenRequest, TokenResponse)
│   ├── 📂 Controllers                                 # API controllers (e.g., MoviesController, AuthController)
│   ├── 📂 Filtters                                    # Endpoint group definitions, likely for Minimal APIs
│   ├── 📂 Images                                      # Handles image-related logic (e.g., upload, retrieval)
│   ├── 📂 Mapping                                     # AutoMapper configuration and profile classes
│   ├── 📂 Middlewares                                 # Custom middleware (e.g., error handling, logging)
│   ├── 📂 Properties                                  # Launch settings for local development
│   ├── EG.Walks.csproj                                 # Project file for the API project
│   ├── Movies.Api.http                                 # HTTP request collection for testing endpoints
│   ├── Program.cs                                      # Application entry point and dependency setup
│   ├── appsettings.json                                # General app configuration (connection strings, JWT, etc.)
│   └── appsettings.Development.json                    # Environment-specific configuration for development

├── 📂 EG.Walks.Application                            # Application layer (business logic)
│   ├── 📂 Interfaces                                  # Interfaces for repositories, services, and unit of work
│   └── EG.Walks.Application.csproj                     # Project file for the application layer

├── 📂 EG.Walks.Domain                                 # Domain layer (core models and entities)
│   ├── 📂 Entities                                    # Domain entities like Walk, Region, Difficulty
│   └── EG.Walks.Domain.csproj                          # Project file for the domain layer

├── 📂 EG.Walks.Infrastructure                         # Infrastructure layer (data access, database)
│   ├── 📂 Data                                        # EF Core DbContext and seeding logic
│   ├── 📂 Migrations                                  # EF Core migrations for database updates
│   ├── 📂 Repositories                                # Repository implementations
│   └── EG.Walks.Infrastructure.csproj                  # Project file for the infrastructure layer

├── 📂 EG.Walks.UI                                     # ASP.NET MVC UI project (frontend)
│   ├── 📂 Controllers                                 # MVC controllers for handling requests and returning views
│   ├── 📂 Models                                      # ViewModels used in views
│   ├── 📂 Properties                                  # Launch settings for the MVC app
│   ├── 📂 Views                                       # Razor views (HTML templates)
│   ├── 📂 wwwroot                                     # Static files (CSS, JS, images)
│   ├── AuthHeaderHandler.cs                            # Adds JWT token to outgoing HTTP requests
│   ├── AuthTokenProvider.cs                            # Retrieves and manages JWT tokens
│   ├── Movies.Api.Sdk.Consumer.csproj                  # Project file for the MVC/UI project
│   └── Program.cs                                      # Entry point for the MVC app

├── .gitattributes                                      # Git settings for line endings and file handling
├── .gitignore                                          # Specifies files and folders to exclude from version control
└── EG.Walks.API.sln                                    # Visual Studio solution file combining all projects

```
---

## 💡 Author
Developed with ❤️ by Alber Ashraf
