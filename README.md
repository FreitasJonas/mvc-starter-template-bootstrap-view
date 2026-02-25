# MVC Starter Template

Enterprise-grade ASP.NET Core MVC foundation designed to accelerate  
the development of secure, scalable and maintainable business systems.

> Not just a UI template.  
> A structured **application backbone** ready for production.

---

## 🚀 Overview

**MVC Starter Template** is a production-ready base solution built with:

- .NET 8
- Layered architecture
- Policy-based authorization
- Deterministic database versioning
- Built-in auditing and logging

It provides the structural components typically required in corporate systems from day one — eliminating boilerplate and architectural uncertainty.

---

## 🎯 Who is this for?

This template is ideal for:

- Backend engineers building enterprise systems
- Consulting teams delivering corporate platforms
- SaaS founders building secure multi-user systems
- Teams that require traceability and permission control
- MVPs that must scale into production environments

---

## 🧩 Core Capabilities

### 🔐 Security & Authentication

- Secure cookie-based authentication
- Optional two-factor verification (2FA)
- Policy-based permission system
- Fine-grained action-level authorization
- Secure password hashing (PBKDF2)

---

### 👥 Users & Access Control

- User lifecycle management
- Role-based grouping of permissions
- Claim-based permission loading
- Explicit feature-to-permission mapping
- Declarative authorization via custom attributes

---

### 🧾 Auditing & Logging

- Structured audit records stored in database
- Create / Update / Delete tracking
- File-based logging with daily rotation
- Built-in log viewer
- Global exception middleware

---

### 🗄 Database & Versioning

- MySQL / MariaDB support
- Automatic database creation (if missing)
- Version-controlled SQL scripts
- Transactional migration execution
- No ORM lock-in (ADO.NET by default)

---

### 🏗 Architecture

- Layered solution structure
- Clear dependency direction
- Domain isolation
- Explicit SQL control
- Infrastructure separated from business rules

Designed to support long-term maintenance and safe feature expansion.

---

### 🎨 Front-End

- Bootstrap 5 (local)
- Bootstrap Icons
- Tabulator.js integration
- Modular CSS structure
- Vanilla JavaScript (no jQuery)
- Responsive and corporate-ready layout

---

## 🧱 Project Structure

```text
mvc.starter.template.sln
│
├── mvc.starter.template.Web
│   → MVC layer (Controllers, Views, Filters, Middlewares)
│
├── mvc.starter.template.Application
│   → Application services and use cases
│
├── mvc.starter.template.Domain
│   → Business entities and rules
│
├── mvc.starter.template.Data
│   → Data access using MySql.Data (ADO.NET)
│
└── mvc.starter.template.Shared
    → Cross-cutting utilities and abstractions