# RoimaAssignment API

This repository contains the **RoimaAssignment API** project built with **ASP.NET Core** and **Entity Framework Core**.

---

## 🚀 Getting Started

### Prerequisites
Make sure you have installed:
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
  ```bash
  dotnet tool install --global dotnet-ef
  ```
## 📦 NuGet Packages

The project uses the following NuGet packages:

| Package                                | Version |
|----------------------------------------|---------|
| [Microsoft.EntityFrameworkCore.Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/) | 9.0.8   |
| [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) | 13.0.3  |
| [Pomelo.EntityFrameworkCore.MySql](https://www.nuget.org/packages/Pomelo.EntityFrameworkCore.MySql/) | 9.0.0   |
| [Swashbuckle.AspNetCore](https://www.nuget.org/packages/Swashbuckle.AspNetCore/) | 6.4.0   |

# Connection string
please change connection string in app-setting.json file in below format

"Server=<Serverpath>; User ID=<User_name>; Password=<password>; Database=<DATABASE_NAME>"
