# Task Management API

## Objetivo
API REST para gerenciamento simples de tarefas.

## Tecnologias
- .NET 8 ou .NET 6
- ASP.NET Core Web API
- Entity Framework Core InMemory
- Swagger
- xUnit
- Dependency Injection
- Logging

## Arquitetura escolhida
Explicar camadas: API, Application, Domain, Infrastructure.

## Como rodar
dotnet restore
dotnet build
dotnet run --project TaskManagement.Api

## Swagger
https://localhost:xxxx/swagger

## Como testar
dotnet test

## Endpoints
POST /api/tasks
GET /api/tasks
GET /api/tasks/{id}
PUT /api/tasks/{id}
DELETE /api/tasks/{id}