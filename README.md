# 🚀 MVC Starter Template

> A production-ready ASP.NET Core MVC foundation  
> built for secure, scalable and real-world business systems.

Stop rebuilding authentication, permissions, auditing and database setup  
every time you start a new project.

Start with a solid foundation.

Demo View:
https://mvc-starter-template-bootstrap.onrender.com/

---

## 🎯 Why this exists

Most MVC templates focus only on UI.

Real systems require:

- Authentication
- Fine-grained permissions
- Audit trail
- Log management
- Deterministic database evolution
- Clean architecture boundaries

This project delivers those structural components from day one.

---

## 🧱 What You Get

### 🔐 Enterprise-Grade Security

- Secure cookie-based authentication
- Optional two-factor verification (2FA)
- Policy-based authorization
- Fine-grained permission model
- Action-level access enforcement
- Secure password hashing (PBKDF2)

---

### 👥 User & Role Management

- User lifecycle management
- Role-based permission grouping
- Claim-based permission loading
- Explicit feature-to-permission mapping
- Declarative authorization via custom attributes

---

### 🧾 Auditing & Traceability

- Structured audit records (create / update / delete)
- User activity tracking
- Sensitive action logging
- Database-backed audit history
- Compliance-ready architecture

---

### 📄 Logging Infrastructure

- File-based logging
- Daily log rotation
- Built-in log viewer
- Global exception middleware
- Safe production behavior

---

### 🗄 Database Automation

- MySQL / MariaDB support
- Automatic database creation
- Version-controlled SQL scripts
- Transactional migration execution
- No ORM lock-in (ADO.NET by default)

---

### 🏗 Clean Layered Architecture

mvc.starter.template.sln

├── mvc.starter.template.Web  
│   → MVC layer (Controllers, Views, Filters, Middlewares)

├── mvc.starter.template.Application  
│   → Use cases and application services

├── mvc.starter.template.Domain  
│   → Business entities and rules

├── mvc.starter.template.Data  
│   → ADO.NET persistence layer

└── mvc.starter.template.Shared  
    → Cross-cutting utilities and abstractions

- Clear dependency direction
- Domain isolation
- Infrastructure separated from business rules
- Scalable foundation for long-term systems

---

## ⚙️ Quick Start

1. Clone the repository  
2. Configure `appsettings.json`  
3. Run the application  

On first startup, the system automatically:

- Creates the database (if missing)
- Executes pending migration scripts
- Initializes required schema structures

No manual SQL execution required.

---

## 💡 Who Is This For?

- Backend engineers building enterprise systems
- SaaS founders needing secure multi-user platforms
- Consulting companies delivering corporate software
- Teams tired of rebuilding authentication and permissions from scratch
- MVPs that must evolve into production systems

---

## 📊 What Makes This Different?

Most starter templates provide:

❌ Basic login only  
❌ No permission engine  
❌ No audit trail  
❌ No database version control  
❌ No architectural separation  

This project provides:

✔ Structured permission model  
✔ Declarative authorization  
✔ Deterministic database evolution  
✔ Logging and auditing infrastructure  
✔ Clean layered architecture  

---

## 🔥 Public vs Pro Version

This repository demonstrates the architecture and core structure.

The **Pro version** includes:

- Full permission synchronization engine
- Advanced auditing configuration
- Extended migration scripts
- Production deployment guide
- Continuous updates
- Commercial usage license

If you are building a serious product, the Pro version saves weeks of development time.

📩 Contact for Pro access:  
sistemas@desenvolvedorjonas.com.br

---

## 📈 Example Use Cases

You can build:

- Internal corporate systems
- Admin dashboards
- Multi-tenant SaaS platforms
- HR / ERP foundations
- Compliance & control systems
- Access-controlled business applications

---

## 🛡 Design Philosophy

This project is built around:

- Predictability
- Explicit security
- Deterministic behavior
- No hidden framework magic
- Long-term maintainability

It is not a demo.

It is a foundation.

---

## 📄 License

All rights reserved.

Commercial usage requires a valid license.
