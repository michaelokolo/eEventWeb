# eEvent

## Overview

**eEvent** is a modern event management platform built with ASP.NET Core (.NET 8), following Clean Architecture and Domain-Driven Design (DDD) principles.  
It enables organizers to create and manage events, and freelancers to browse, apply, and track their applications.

---

## Features

### For Organizers
- **Create Events:** Add new events with title, description, date, location, role, budget, and requirements.
- **Manage Events:** View, edit, and delete your events from a dashboard.
- **Review Applications:** See all freelancer applications for your events, view applicant details, and update application status (accept, reject, withdraw).

### For Freelancers
- **Browse Events:** View all available events.
- **Apply to Events:** Submit applications with a custom message to the organizer.
- **Track Applications:** View the status of all submitted applications (pending, accepted, rejected, withdrawn).

### Additional Features
- **Role-based Dashboards:** Separate dashboards for organizers and freelancers.
- **Email Notifications:** SMTP integration for application and event updates.
- **Code Coverage:** Configured for Visual Studio and CI.

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQLite
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [VS Code](https://code.visualstudio.com/)
- [Node.js](https://nodejs.org/) (if using frontend assets)

### Setup

1. **Clone the repository**
    ```bash
    git clone https://github.com/michaelokolo/eEventWeb.git
    cd eEventWeb
    ```

2. **Configure the database**
    - Update `appsettings.json` with your connection string.
    - Ensure your connection string in `appsettings.json` point to a local SQL Server instance.
    - Ensure the tool EF wa already installed.
      ```bash
      dotnet tool update --global dotnet-ef
      ```
    - Run migrations:
      ```bash
      dotnet restore
      dotnet tool restore
      dotnet ef database update --context AppIdentityDbContext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj
      dotnet ef database update --context EventAppContext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj
      ```
      These commands will set up two distinct databases: one dedicated to event-related data and another for storing the app’s user credentials and identity information.

3. **Configure SMTP (Email)/Login Password**
    - Use [User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) for development:
      ```bash
      dotnet user-secrets set "SmtpSettings:Username" "your_smtp_username"
      dotnet user-secrets set "SmtpSettings:Password" "your_smtp_password"
      dotnet user-secrets set "SmtpSettings:From" "your_from_address"
      dotnet user-secrets set "Authorization:DefaultPassword" "your_login_password"
      ```
    - For production, use environment variables or a secrets manager.

4. **Run the application**
    ```bash
    dotnet run --project src/Web
    ```
    On the first run, the application will automatically seed both databases. This means you’ll see events displayed on the home page, and you’ll be able to log in using either [organizer@eevent.com](organizer@eevent.com) or [freelancer@eevent.com](freelancer@eevent.com).

5. **Access the app**
    - Open [https://localhost:7290/](https://localhost:7290/) in your browser.

---
## Configuration

- **SMTP Settings:** Set via user secrets or environment variables.
- **Login Password:** Set via user secrets or environment variables.
- **Database:** Connection string in `appsettings.json`.

---

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/my-feature`)
3. Commit your changes (`git commit -am 'Add new feature'`)
4. Push to the branch (`git push origin feature/my-feature`)
5. Create a Pull Request

---

## License

This project is licensed under the [MIT License](LICENSE).

---

## Acknowledgements

- Inspired by [eShopOnWeb](https://github.com/dotnet-architecture/eShopOnWeb) by Microsoft.
- Uses [Ardalis.Specification](https://github.com/ardalis/specification) for repository patterns.

---

## Contact

For questions or support, open an issue or contact the maintainers.


