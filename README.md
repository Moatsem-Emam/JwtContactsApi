# 📞 Contacts Management API

A secure Web API built with **ASP.NET Core** for managing contacts, following the **Clean Architecture** pattern.  
Includes support for **JWT authentication** and **role-based authorization**.

---

## 🔧 Features

- 🔐 JWT-based login and registration
- 👥 Role-based authorization (Admin / User)
- 📇 Full CRUD operations for contacts
- 🔍 Sorting and pagination
- 🧱 Clean Architecture (Application / Infrastructure / Presentation)
- 🐳 Docker & Docker Compose support

---

## 🚀 Getting Started

### ✅ Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

---

## ▶️ Run the Project

### 🔹 Standard Run

```bash
# 1. Clone the repository
git clone https://github.com/Moatsem-Emam/JwtContactsApi.git
cd contacts-api

# 2. Apply migrations
dotnet ef database update --project Infrastructure

# 3. Run the API
dotnet run --project Presentation/ContactsApi
```

- **Swagger UI:** [https://localhost:5001/swagger](https://localhost:5001/swagger)

---

### 🐳 Run with Docker

```bash
docker-compose up --build
```

- **Swagger UI**: [http://localhost:5000/swagger](http://localhost:5000/swagger)
- **SQL Server**:
  - **Host:** `localhost:1433`
  - **User:** `sa`
  - **Password:** `Your_password123`

---

## 📂 Project Structure

```plaintext
📁 Application/
│  ├── Service/                 # Service implementations (e.g., ContactService)
│  └── Service.Abstraction/    # Interface definitions
│
📁 Infrastructure/
│  ├── Core/                   # Core configurations and components
│  ├── Persistence/            # DbContext and migrations
│  └── Shared.Dtos/            # DTO files
│
📁 Presentation/
│  └── ContactsApi/            # Entry point, controllers, Program.cs
│      ├── Controllers/
│      ├── appsettings.json
│      └── Dockerfile
```

---

## 🔑 Authentication & Authorization

After registration or login, a **JWT Token** is returned.

Use it to access protected endpoints by including it in the `Authorization` header:

```http
Authorization: Bearer <your_token>
```

---

## 📦 Postman Collection

- Available in the `/Postman` directory.
- Includes examples for login, contact CRUD operations, and more.

---
