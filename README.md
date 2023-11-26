# Users Web API

### Sections

[Description](#description)

[Features](#features)

[Tools required](#tools-required)

[Testing the application](#testing-the-application)

[Libraries and technologies](#libraries-and-technologies)


## Description
🇬🇧
This repository contains a study project of a Web API using .NET 7  with the goal of implementing CRUD (Create, Read, Update, Delete) functionalities for users, including JWT Bearer authentication and secure password storage.


🇧🇷
Este repositório contém um projeto de estudo da criação de uma API web em .NET 7 com o objetivo de implementar um CRUD completo (Create, Read, Update, Delete) de usuários, incluindo autenticação com JWT Bearer Authentication e armazenamento seguro de senhas.

## Features

| Method | Route                       | JSON Body                | Action                          |
| ------ | --------------------------- | -------------------      | ------------------------------- |
| POST   | /Login                      | Email + Password         | Generates the access token      |
| POST   | /User                       | Name + Email + Password  | Creates the user                |
| GET    | /User?offset=5&limit=5      |                          | Returns the list of users       |
| GET    | /User/Id/{id}               |                          | Returns the user of the id      |
| GET    | /User/Email/{email}         |                          | Returns the user of the email   |
| PUT    | /User/UpdateName            | Id + Name                | Updates the user's name         |
| PUT    | /User/UpdateEmail           | Id + Email               | Updates the user's email        |
| PUT    | /User/UpdatePassword        | Id + Password            | Updates the user's password     |
| DELETE | /User/Delete/{id}           |                          | Deletes the user                |

### Request body example
```json
{
  "name": "string",
  "email": "string",
  "password": "string"
}
```

### Response body example
```json
{
  "data": {
    "id": 0,
    "name": "string",
    "email": "string"
  },
  "message": "string",
  "statusCode": 100,
  "error": "string"
}
```


## Tools required

To run the application, you must have these tools installed:

- [.NET 7.0](https://dotnet.microsoft.com/pt-br/download/dotnet/7.0)
- [Microsoft SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)


## Testing the application

Download or clone the project from: [https://github.com/MatheusMGrassano/ApiFilmes](https://github.com/MatheusMGrassano/WebApiUsers.git)

### Connection String

Change the database connection credentials in the appsettings.json file.

Set the "Data Source" of the connection string using your SQL Server's name and in the "DataBase" choose a name for the database that will be created.

**ConnectionString example:**
```json
  "ConnectionStrings": {
    "Default": "Data Source=YourServer;Database=YourDatabase;Trusted_connection=true;Encrypt=false;TrustServerCertificate=true"
  }
```

### Generating the migration for the database creation 

At your terminal, access the project's root folder and type the following commands:
```bash
dotnet tool install --global dotnet-ef
```

```bash
dotnet ef migrations add DataBaseCreation
```


```bash
dotnet ef database update
```

### Running the application

At your terminal, access the project's root folder and type the following command:

```bash
dotnet watch run
```

Then, a new browser tab will open with the Swagger UI, showing all the features of the API.

## Libraries and technologies

- [Entity Framework Core](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/)
- [Entity Framework Core SQL Server](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer)
- [Entity Framework Core Tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools)
- [Entity Framework Core Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design)
- [ASP.NET Core JWT Bearer Authentication](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/)
- [Swagger UI](https://swagger.io/tools/swagger-ui/)
