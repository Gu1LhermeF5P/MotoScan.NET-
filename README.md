#  MotoScan - Sistema de Gerenciamento de Frota

Sistema de gerenciamento de frota de motos da Mottu, desenvolvido com **Clean Architecture**, **Domain-Driven Design (DDD)** e **Clean Code** em .NET 8.

## 📋 Descrição do Domínio

O MotoScan é um sistema que permite o controle completo de entrada e saída de motos em uma frota, incluindo:

- **Gestão de Motos**: CRUD completo com informações detalhadas
- **Check-in/Check-out**: Controle de entrada e saída com registro fotográfico
- **Rastreamento**: Localização e estado das motos em tempo real
- **Histórico**: Auditoria completa de movimentações

##  Arquitetura do Projeto

```
 src
┣  MotoScan.Api          # Controllers, validações de entrada, configurações
┣  MotoScan.Application  # Casos de uso, DTOs, Services
┣  MotoScan.Domain       # Entidades, Value Objects, Interfaces, Regras de Negócio
┗  MotoScan.Infrastructure # Acesso a dados, serviços externos, repositórios
```

### Camadas e Responsabilidades

- **Domain**: Core business logic, entidades ricas, value objects, interfaces
- **Application**: Casos de uso, DTOs, orquestração
- **Infrastructure**: Implementação de repositórios, acesso a dados, serviços externos
- **Api**: Controllers, validações de entrada, configurações

##  Aplicação dos Conceitos

### Domain-Driven Design (DDD)

**Entidades Ricas:**
- `Moto` (Agregado Raiz): Comportamentos como RealizarCheckIn, RealizarCheckOut
- `RegistroMovimentacao`: Controla check-ins e check-outs

**Value Objects:**
- `Placa`: Validação e formatação de placas
- `EstadoMoto`: Estados válidos com regras de transição
- `Localizacao`: Informações de localização

**Agregados:**
- `Moto` como Agregado Raiz que controla suas movimentações

### Clean Architecture

- **Baixo acoplamento**: Dependências apontam sempre para o núcleo
- **Inversão de dependência**: Interfaces definidas no Domain
- **Separação de responsabilidades**: Cada camada tem papel específico

### Clean Code

- **SRP**: Classes com responsabilidade única
- **DRY**: Reutilização via services e value objects  
- **KISS**: Soluções simples e diretas
- **YAGNI**: Implementação apenas do necessário

##  Como Executar

### Pré-requisitos
- .NET 8 SDK
- SQL Server ou Oracle Database
- Visual Studio 2022 ou VS Code

### Passos para execução

1. **Clone o repositório**
```bash
git clone https://github.com/seu-usuario/motoscan-clean-architecture.git
cd motoscan-clean-architecture
```

2. **Configure a string de conexão**
```json
// appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MotoScanDB;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

3. **Execute as migrações**
```bash
cd src/MotoScan.Api
dotnet ef database update
```

4. **Execute o projeto**
```bash
dotnet run
```

5. **Acesse o Swagger**
```
https://localhost:5001/swagger
```

##  Endpoints da API

###  Gestão de Motos

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/motos` | Lista todas as motos |
| GET | `/api/motos/{id}` | Busca moto por ID |
| GET | `/api/motos/placa/{placa}` | Busca por placa |
| POST | `/api/motos` | Cadastra nova moto |
| PUT | `/api/motos/{id}` | Atualiza moto |
| DELETE | `/api/motos/{id}` | Remove moto |

### 🏁 Check-in / Check-out

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/api/motos/{id}/checkin` | Realiza check-in |
| POST | `/api/motos/{id}/checkout` | Realiza check-out |
| GET | `/api/motos/{id}/historico` | Histórico de movimentações |

## 📄 Exemplos de Requisições

### Cadastrar Nova Moto
```json
POST /api/motos
{
  "modelo": "Honda CG 160",
  "placa": "ABC1234",
  "estado": "Excelente",
  "localizacao": "Pátio A"
}
```

### Realizar Check-in
```json
POST /api/motos/1/checkin
{
  "localizacao": "Entrada Principal",
  "observacoes": "Moto em perfeito estado"
}
```

### Realizar Check-out
```json
POST /api/motos/1/checkout
{
  "localizacao": "Saída Zona Sul",
  "entregadorId": "ENT001",
  "observacoes": "Entrega programada"
}
```

##  Modelo de Dados

### Moto (Agregado Raiz)
- Id, Modelo, Placa, Estado, Localizacao
- Métodos: RealizarCheckIn(), RealizarCheckOut(), AlterarEstado()

### RegistroMovimentacao
- Id, MotoId, TipoMovimentacao, DataHora, Localizacao, Observacoes

### Value Objects
- **Placa**: Validação de formato brasileiro
- **EstadoMoto**: Enum com estados válidos (Excelente, Bom, Regular, Ruim)

##  Tecnologias Utilizadas

- **.NET 8**: Framework principal
- **Entity Framework Core**: ORM para acesso a dados
- **FluentValidation**: Validações de entrada
- **AutoMapper**: Mapeamento entre DTOs e entidades
- **Swagger/OpenAPI**: Documentação da API
- **SQL Server**: Banco de dados principal

##  Estrutura Detalhada

```
MotoScan/
├── src/
│   ├── MotoScan.Api/
│   │   ├── Controllers/
│   │   ├── Configuration/
│   │   ├── Middleware/
│   │   └── Program.cs
│   ├── MotoScan.Application/
│   │   ├── UseCases/
│   │   ├── DTOs/
│   │   ├── Mappings/
│   │   └── Validators/
│   ├── MotoScan.Domain/
│   │   ├── Entities/
│   │   ├── ValueObjects/
│   │   ├── Interfaces/
│   │   └── Enums/
│   └── MotoScan.Infrastructure/
│       ├── Data/
│       ├── Repositories/
│       └── Services/
├── tests/
└── docs/
```

##  Funcionalidades Implementadas

**Clean Architecture** com separação de camadas  
**DDD** com entidades ricas e value objects  
**Clean Code** aplicando princípios SOLID  
**Entity Framework Core** com migrations  
**Swagger** com documentação completa  
**Validações** com FluentValidation  
**Testes unitários** para domínio  
**Logs estruturados** com Serilog  

##  Contribuidores

Larissa de Freitas Moura - rm555136
Guilherme Francisco -rm557648

##  Licença

Este projeto está licenciado sob a Licença MIT - veja o arquivo [LICENSE](LICENSE) para detalhes.
