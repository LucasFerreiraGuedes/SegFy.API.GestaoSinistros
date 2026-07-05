# Gestao de Sinistros

API REST desenvolvida em .NET 8.0 como solução para o teste técnico de Desenvolvedor(a) Pleno. O projeto é responsável pelo gerenciamento de sinistros de seguros, permitindo a abertura, consulta, atualização de status e consulta do histórico de alterações, seguindo as regras de negócio propostas no desafio.
 
---

# Tecnologias Utilizadas

- .NET 8 
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger/OpenAPI
- xUnit 

---

# Estrutura do Projeto

```text
Projeto
│
├── SegFy.API.GestaoSinistros
│   ├── SegFy.API.GestaoSinistros
│   ├── SegFy.API.GestaoSinistros.Application
│   ├── SegFy.API.GestaoSinistros.Domain
│   ├── SegFy.API.GestaoSinistros.Infrastructure
│   ├── SegFy.API.GestaoSinistros.Test
│
```

---

# Pré-requisitos

Antes de executar o projeto, é necessário possuir instalado:

- .NET SDK 8.0
- SQL Server 
- Visual Studio ou VS Code

---

# Clonando o projeto
Abra o terminal e execute:
```bash
git clone https://github.com/seu-usuario/SegFy.API.GestaoSinistros.git

cd SegFy.API.GestaoSinistros
```

---

# Configuração

## Banco de Dados

Configure a **Connection String** no arquivo:

```text
appsettings.json
```

Exemplo:

```json
"ConnectionStrings": {
  "DbConnectionString": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GestaoSinistros;Integrated Security=True;TrustServerCertificate=True;"
}
```
Deve-se mudar o campo "Data Source", colocando o nome do seu servidor SQL Server instalado. É necessário também que seu servidor esteja com a Autenticação do Windows ativada possibilitando seu acesso por meio das credenciais do Windows.

<img width="481" height="588" alt="image" src="https://github.com/user-attachments/assets/590b2c73-48ff-44b2-a87d-055f7a106220" />


## Criando o banco de dados

Após configurar a conexão, execute o comando abaixo para criar o banco de dados e aplicar todas as migrations:

<img width="782" height="673" alt="image" src="https://github.com/user-attachments/assets/faa02541-cde6-4262-bdf7-73298ffbbdbe" />

```cmd
update-database
```

---

# Executando a aplicação

Na pasta da API:

```bash
dotnet build

dotnet run
```

---

# Swagger

Após iniciar a aplicação, acesse:

```
https://localhost:7000/swagger
```

Lá estarão disponíveis todos os endpoints para teste.

---

# Executando os testes

```bash
dotnet test
```

---

# Funcionalidades

- Abertura de sinistros;
- Consulta de sinistros por ID;
- Listagem de sinistros com filtros e paginação;
- Atualização do status dos sinistros;
- Consulta do histórico de alterações;

---

# Arquitetura

Este projeto foi desenvolvido utilizando uma abordagem baseada em **Arquitetura em Camadas (Layered Architecture)**, com princípios inspirados em **Clean Architecture**, visando separação de responsabilidades, testabilidade e baixo acoplamento entre os módulos.

A solução é organizada em camadas bem definidas:

- **Domain**: contém as entidades de negócio e regras centrais do sistema, sem dependência de frameworks ou camadas externas.
- **Application**: concentra os serviços da aplicação, regras de orquestração e contratos (interfaces), além de DTOs e exceções de domínio.
- **Infrastructure**: responsável pela implementação de acesso a dados, incluindo repositórios e configuração do Entity Framework Core.
- **API (Presentation)**: camada de entrada do sistema, responsável por expor os endpoints e realizar a comunicação com a camada de aplicação.

A comunicação entre as camadas é feita principalmente através de **injeção de dependência (Dependency Injection)**, garantindo baixo acoplamento e facilitando a testabilidade do sistema.

---

## Qualidade e Testes

O projeto prioriza a criação de **testes unitários na camada de Application (Services)**, com foco na validação das regras de negócio e cenários críticos, seguindo boas práticas de testes automatizados.

---

# Consultas SQL

O arquivo **`queries.sql`** contém as consultas SQL solicitadas para análise dos dados armazenados no banco.

As consultas implementadas são:

- Ranking dos ramos com maior percentual de sinistros negados nos últimos 6 meses;
- Top 10 clientes com maior soma de valor estimado em sinistros em análise ou aprovados;
- Tempo médio de resolução (em dias) de sinistros encerrados, agrupado por ramo.

O arquivo pode ser executado diretamente no SQL Server Management Studio (SSMS) ou em outra ferramenta compatível com SQL Server após a criação e população do banco de dados.

# Autor

Desenvolvido por Lucas Ferreira Guedes.
