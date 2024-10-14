
# API de Tarefas - C# .NET

Esta API de Tarefas foi desenvolvida em C# utilizando o .NET 8. O propósito desta API é gerenciar tarefas com status, datas de criação, e filtragem. A aplicação utiliza o MongoDB como banco de dados e implementa funcionalidade para filtrar tarefas por status e ordenar por datas de criação.

## Tecnologias Utilizadas

- C# (.NET 8).
- MongoDB.
- ASP.NET Core.

## Funcionalidades

- CRUD completo de tarefas (Create, Read, Update, Delete).
- Filtragem por status (Pending, InProgress, Done).
- Ordenação por data de criação (ascendente e descendente).
- Validação automática de entradas.
- Suporte para timestamps e geração automática de IDs.
- Resposta de endpoints de errors.

## Instalação e Execução Local

### Pré-requisitos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MongoDB](https://www.mongodb.com/try/download/community) instalado e em execução localmente.

### Clonando o Repositório

```
git clone https://github.com/EnzoLavieri/CrudTarefaCSharp.git
cd AvaliacaoAtak
```

### Configuração

1. **Configurar o MongoDB**: Defina a string de conexão no arquivo `appsettings.json`. Exemplo:
   
```json
{
 "ApiTaskDatabase": {
   "ConnectionString": "mongodb://localhost:27017",
   "DatabaseName": "ApiTasks",
   "TaskCollectionName": "Tasks"
 }
}
```

### Executando a Aplicação

1. No terminal, execute o comando para restaurar as dependências e rodar a aplicação:

```bash
dotnet restore
dotnet run
```

2. A aplicação estará disponível em `https://localhost:7101` ou `http://localhost:5241`.

### Testando a API

Você pode testar a API utilizando o [Postman](https://www.postman.com/downloads/), `curl` ou o próprio Swagger. Abaixo estão alguns exemplos de endpoints:

#### Criar uma nova tarefa

```bash
POST /api/Tasks
Content-Type: application/json
--> O id sera gerado automaticamente caso deixe vazio a string ou caso queira inserir uma voce mesmo, a string deve ser hexadecimal de 24 digitos.
{
  "id": "", 
  "title": "Criacao",
  "description": "Criacao da tarefa",
  "status": 0,
  "createdAt": "2024-10-14T04:30:12.293Z"
}
```

#### Listar todas as tarefas

```bash
GET /api/Tasks
```

#### Listar todas as tarefas em ordem crescente

```bash
GET /api/Tasks/DataAsc
```

#### Listar todas as tarefas em ordem decrescente

```bash
GET /api/Tasks/DataDesc
```

#### Filtrar tarefas por status

```bash
GET /api/Tasks/status/2
Anterne entre: 0 = Pending, 1  = InProgress, 2 = Done.
```

#### Atualizar uma tarefa

```bash
PUT /api/tasks/{id}
Content-Type: application/json
Necessario colocar o id da tarefa novamente quando for atualizar.
{
  "id": "67099e2bb5104756e97a161b", 
  "title": "Update",
  "description": "Update da tarefa",
  "status": 0,
  "createdAt": "2024-10-14T04:30:12.293Z"
}
```

#### Deletar uma tarefa

```bash
DELETE /api/tasks/{id}
```
