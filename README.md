# eEvent 🎉

**eEvent** is a platform where event organizers can post gigs and roles, and freelancers can browse and apply to them.  
Built using **Clean Architecture** with **.NET 8**.

---

## 🚀 Features

- Organizers: Create events and post roles  
- Freelancers: Browse events and apply to roles  
- Simple messaging/contact between organizer and freelancer  
- Dashboard to track applications  

---

## 🏗️ Project Structure (Clean Architecture)

- **Application Core**: Business logic, entities, use cases  
- **Infrastructure**: Data access, repositories, external services  
- **Web Layer**: ASP.NET MVC / Web API endpoints, controllers, UI integration  

This structure helps separate concerns and keeps the codebase maintainable and scalable.

---

## 🛠️ Tech Stack

- **Backend**: .NET 8 (Clean Architecture)  
- **Frontend**: React Native  
- **Database**: Azure SQL Database / SQL Server  
- **Hosting**: Azure App Service (optional)  
- **CI/CD**: GitHub Actions (optional)  

---

## 🔧 Local Setup

1. **Clone the repository**
```bash
git clone https://github.com/michaelokolo/eEventWeb.git
cd eevent
```

2. **Setup**
```bash
cd src/Web
dotnet restore
dotnet run
```

3. Open the web app at http://localhost:5000 and the mobile app in Expo. Needs correction.

## 🤝 Contributing

We welcome contributions! Please see [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines on:
- Forking the repository
- Branching and coding conventions
- Raising issues and submitting pull requests

## 📌 Roadmap
 - Add notifications
 - Add payments
 - Messaging & Chat
 - Public API for integrations
 - Admin dashboard

## 📜 License
This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.
