# Sistema BL / Container — Trabalho Prático 02 CBTSWE2 (ADS 671)

**Dupla:** João do Valle Seixas Paula e André Luiz Nascimento de Andrade

Aplicação **ASP.NET Core MVC** que modela a relação entre **BL (Bill of Lading)** e
**Container**, atendendo aos itens obrigatórios do trabalho:

- Modelagem das entidades no banco de dados (chaves primárias e estrangeiras).
- Telas de cadastro de BL e Container (Container sempre associado a um BL).
- CRUD completo (Create, Read, Update, Delete) das duas entidades.
- Layout com Bootstrap 5.
- Menu de navegação (Início / BLs / Containers / Relatório).
- Relatório consolidado listando cada BL com os seus Containers.

## Estrutura do domínio

- **BL** (1) ── possui ──► (0..N) **Container**
  - `BL`: Id, Numero, Consignee, Navio
  - `Container`: Id, Numero, Tipo (`Dry`/`Reefer`), Tamanho (`20`/`40` pés), **BLId (FK obrigatória)**

A regra "container deve ser associado a um BL obrigatoriamente" é garantida por:
- `BLId` marcado como `[Required]` no model e como chave estrangeira não-nula no `BLContainerContext`.
- O formulário de cadastro do Container exige a seleção de um BL existente (não é possível salvar sem escolher um).
- Exclusão de um BL que ainda possua Containers associados é bloqueada (é preciso excluir/realocar os containers primeiro).

## Requisitos para rodar

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server LocalDB (já vem com o Visual Studio / "SQL Server Express LocalDB", padrão em instalações Windows com carga de trabalho ASP.NET)
- Alternativamente: Visual Studio 2022 (17.8+) com a carga de trabalho "Desenvolvimento ASP.NET e Web"

## Como executar

### Opção 1 — Visual Studio (mais simples)
1. Abra o arquivo `BLContainerApp.sln`.
2. Aguarde o Visual Studio restaurar os pacotes NuGet automaticamente.
3. Pressione **F5** (ou "Executar").
4. O banco `BLContainerDb` é criado automaticamente no LocalDB na primeira execução (não é necessário rodar migrations manualmente — o `Program.cs` chama `Database.EnsureCreated()`).

### Opção 2 — linha de comando (dotnet CLI)
```bash
cd BLContainerApp
dotnet restore
dotnet run
```
Depois acesse o endereço mostrado no terminal (ex.: `http://localhost:5000`).

## Não tenho SQL Server / LocalDB (Linux/Mac ou sem SQL Server instalado)

Troque o provedor para **SQLite**, que não exige instalação de servidor:

1. No arquivo `BLContainerApp.csproj`, troque o pacote:
   ```xml
   <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="8.0.8" />
   ```
   (no lugar de `Microsoft.EntityFrameworkCore.SqlServer`)
2. Em `appsettings.json`, troque a connection string por:
   ```json
   "BLContainerContext": "Data Source=BLContainer.db"
   ```
3. Em `Program.cs`, troque `options.UseSqlServer(...)` por `options.UseSqlite(...)`.
4. Rode `dotnet restore` e `dotnet run` normalmente.

## Se preferir usar Migrations (em vez de EnsureCreated)

O projeto já referencia `Microsoft.EntityFrameworkCore.Tools` e `Microsoft.EntityFrameworkCore.Design`,
então basta:
```bash
dotnet tool install --global dotnet-ef   # se ainda não tiver o dotnet-ef instalado
cd BLContainerApp
dotnet ef migrations add InitialCreate
dotnet ef database update
```
Nesse caso, remova (ou comente) a chamada `db.Database.EnsureCreated()` em `Program.cs`
para não conflitar com as migrations.

## Navegação da aplicação

- **Início**: apresentação e atalhos.
- **BLs**: lista, criar, editar, detalhar e excluir BLs.
- **Containers**: lista, criar (com seleção obrigatória do BL), editar, detalhar e excluir.
- **Relatório**: lista consolidada, agrupando cada BL com seus containers associados.

## Estrutura de pastas

```
BLContainerApp/
├── BLContainerApp.sln
├── README.md
└── BLContainerApp/
    ├── Controllers/       (BLsController, ContainersController, RelatorioController, HomeController)
    ├── Models/            (BL, Container, BLContainerContext, ErrorViewModel)
    ├── Views/             (Home, BLs, Containers, Relatorio, Shared/_Layout)
    ├── wwwroot/           (css/site.css)
    ├── Program.cs
    └── appsettings.json
```
