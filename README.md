# 📞 Contacts Management API

نظام Web API آمن باستخدام ASP.NET Core لإدارة جهات الاتصال، مبني بأسلوب **Clean Architecture**، مع دعم المصادقة باستخدام JWT وتفويض الأدوار (Roles).

---

## 🔧 المميزات

- 🔐 تسجيل الدخول والتسجيل باستخدام JWT
- 👥 تفويض بالأدوار (Admin / User)
- 📇 عمليات CRUD على جهات الاتصال
- 🔍 فرز وتقسيم إلى صفحات (Sorting & Pagination)
- 🧱 تصميم Clean Architecture (Application / Infrastructure / Presentation)
- 🐳 دعم Docker و Docker Compose

---

## 🚀 كيف تبدأ

### ✅ المتطلبات

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Visual Studio](https://visualstudio.microsoft.com/) أو [VS Code](https://code.visualstudio.com/)

---

## ▶️ التشغيل

### 🔹 تشغيل عادي

```bash
# 1. كلون للمشروع
git clone https://github.com/your-username/contacts-api.git
cd contacts-api

# 2. تنفيذ المايجريشن
dotnet ef database update --project Infrastructure

# 3. تشغيل الـ API
dotnet run --project Presentation/ContactsApi
```

- Swagger UI: [https://localhost:5001/swagger](https://localhost:5001/swagger)

---

### 🐳 تشغيل باستخدام Docker

```bash
docker-compose up --build
```

- Swagger UI: [http://localhost:5000/swagger](http://localhost:5000/swagger)
- SQL Server:
  - المضيف: `localhost:1433`
  - المستخدم: `sa`
  - كلمة المرور: `Your_password123`

---

## 📂 هيكل المشروع

```plaintext
📁 Application/
│  ├── Service/                 # تنفيذ الخدمات (مثل ContactService)
│  └── Service.Abstraction/    # تعريفات الواجهات (Interfaces)
│
📁 Infrastructure/
│  ├── Core/                   # إعدادات أو مكونات أساسية
│  ├── Persistence/            # DbContext والمايجريشن
│  └── Shared.Dtos/            # ملفات DTOs
│
📁 Presentation/
│  └── ContactsApi/            # نقطة الدخول، الكنترولرز، Program.cs
│      ├── Controllers/
│      ├── appsettings.json
│      └── Dockerfile
```

---

## 🔑 المصادقة والتفويض

بعد التسجيل أو تسجيل الدخول، هتحصل على **JWT Token**.

استخدمه للوصول للنقاط المحمية.

```http
Authorization: Bearer <your_token>
```

---

## 📦 مجموعة Postman

- موجودة داخل مجلد `/Postman`.
- تقدر تجرب تسجيل الدخول، CRUD للـ Contacts، الخ...

---
