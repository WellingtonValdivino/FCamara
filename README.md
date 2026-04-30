# Task Management API

## Objetivo

API REST para gerenciamento simples de tarefas, permitindo criação, consulta, atualização e remoção de tarefas.

---

## Tecnologias utilizadas

- .NET 8 (ou .NET 6+)
- ASP.NET Core Web API
- Entity Framework Core (InMemory)
- Swagger
- xUnit
- FluentValidation
- Dependency Injection
- Logging (ILogger)

---

## Arquitetura

A aplicação foi estruturada seguindo separação de responsabilidades:

- **API** → Controllers responsáveis pela comunicação HTTP
- **Application** → Services, DTOs e regras de aplicação
- **Domain** → Entidades e enums
- **Infrastructure** → Persistência de dados (EF Core) e repositórios

Essa abordagem facilita manutenção, testes e escalabilidade, além de aplicar princípios do SOLID.

---

## Como rodar o projeto

```bash
dotnet restore
dotnet build
dotnet run
```

---

## Após subir a aplicação, acesse:

```bash
http://localhost:5177/swagger
```

---

## Como executar os testes

```bash
dotnet test
```

---

## Endpoints

- **POST /api/tasks** → Criar tarefa
- **GET /api/tasks** → Listar tarefas (com filtros)
- **GET /api/tasks/{id}** → Buscar tarefa por ID
- **PUT /api/tasks/{id}** → Atualizar tarefa
- **DELETE /api/tasks/{id}** → Remover tarefa