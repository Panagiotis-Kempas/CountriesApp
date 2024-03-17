# .NET 8 Web API Project

## Description
This project contains a .NET 8 Web API implementation for solving the given tasks. It utilizes SQL Server as the database backend and localhost as the server. The user can easily set up the database by running a command in the Package Manager Console in Visual Studio. Postman can be used to send requests to the implemented endpoints.

## Tasks Implemented

### Task 1: Second Largest Integer Endpoint
- **Endpoint**: POST `https://{localhost}/api/collections`
- **Description**: Receives a JSON body containing an array of integers and returns the second largest integer in the array.

### Task 2: Countries Endpoint (Calling 3rd Party API)
- **Endpoint**: GET `https://{localhost}/api/countries`
- **Description**: Calls the restcountries API to retrieve information about all countries.
- **Response Object**:
  - Common Name of the Country
  - Capital of the Country
  - Borders of the Country

## Usage
- Ensure you have .NET SDK installed.
- Clone the repository.
- Set up the SQL Server database by running the `Update-Database` command in the Package Manager Console in Visual Studio.
- Run the application.
- Use Postman to send requests to the implemented endpoints:
  - For Task 1: Send a POST request to `https://{localhost}/api/collections`.
  - For Task 2: Send a GET request to `https://{localhost}/api/countries`.

## Requirements
- .NET SDK
- SQL Server
- Visual Studio 2022 (for Package Manager Console)
- Postman (for sending requests)

## Setup
1. Clone the repository.
2. Run the solution of the project with Visual Studio 2022.
3. Configure the SQL Server connection string in `appsettings.json` to use localhost (`Server=.;`).
4. Run the `Update-Database` command in the Package Manager Console in Visual Studio to create the necessary database schema.
5. Run the application.

## Technologies Used
- .NET 8
- Entity Framework Core
- SQL Server
- Postman

## Contributors
- [Panagiotis Kempas]


