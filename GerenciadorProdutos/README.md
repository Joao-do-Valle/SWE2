# Gerenciador de Produtos

**Trabalho Prático 03 — CBTSWE2 (ADS 671) — IFSP Campus Cubatão**
Professor: Wellington Tuler Moraes

**Dupla:**
- André Luiz Nascimento de Andrade
- João do Valle Seixas Paula

Sistema de gerenciamento de produtos (listar, cadastrar, editar, excluir e ver detalhes) feito com **ASP.NET Core MVC** e **Entity Framework Core**.

## Como executar

Pré-requisito: [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) (ou superior).

```bash
git clone <endereço-do-repositório>
cd GerenciadorProdutos
dotnet run
```

Abra http://localhost:5080 no navegador. Na primeira execução o banco SQLite (`produtos.db`) é criado sozinho, já com quatro produtos de exemplo. Não é preciso instalar servidor de banco de dados.

> No Visual Studio 2022: abra a pasta/projeto e pressione F5. O navegador carrega Bootstrap, fontes e jQuery por CDN, então é preciso estar conectado à internet.

## Requisitos do trabalho e onde estão atendidos

| Requisito | Onde está |
|---|---|
| Página inicial com a lista de produtos | `Views/Produtos/Index.cshtml` (rota `/`) |
| Cadastro com nome, descrição, preço e estoque | `Criar` em `ProdutosController` + `Views/Produtos/Criar.cshtml` |
| Edição de produto | `Editar` + `Views/Produtos/Editar.cshtml` |
| Exclusão de produto (com confirmação) | `Excluir` + `Views/Produtos/Excluir.cshtml` |
| Página de detalhes | `Detalhes` + `Views/Produtos/Detalhes.cshtml` |
| Validação de entrada | Data Annotations em `Models/Produto.cs`, validação no servidor (`ModelState.IsValid`) e no navegador (jQuery Validation) |
| Entity Framework mapeando o domínio | `Models/Produto.cs`, `Data/AppDbContext.cs` |
| Camada de controle | `Controllers/ProdutosController.cs` |
| Estilos | `wwwroot/css/site.css` + Bootstrap |
| Roteamento de URL | `Program.cs` (rota padrão e rota `/produto/{id}`) |

## Estrutura

```
GerenciadorProdutos/
├── Controllers/    ProdutosController, HomeController
├── Data/           AppDbContext (Entity Framework)
├── Models/         Produto
├── Views/          Produtos (Index, Criar, Editar, Excluir, Detalhes) e Shared
├── wwwroot/css/    site.css
├── Program.cs      Configuração, cultura pt-BR e rotas
└── appsettings.json
```

## Detalhes de implementação

- **Validação:** nome (2 a 100 caracteres), descrição (obrigatória, até 500), preço (R$ 0,01 a R$ 999.999,99) e estoque (inteiro de 0 a 1.000.000). Mensagens em português.
- **Cultura pt-BR:** preço digitado com vírgula (`10,50`) e exibido como moeda (`R$ 10,50`).
- **Segurança:** formulários com token anti-forgery e `[Bind]` para evitar overposting.
- **Extras:** busca por nome na lista, resumo do estoque no topo e selos de "baixo" (5 unidades ou menos) e "esgotado".
- **Banco:** o projeto usa `EnsureCreated()` para criar o banco automaticamente. Para trocar para SQL Server, altere o pacote `Microsoft.EntityFrameworkCore.Sqlite` para `Microsoft.EntityFrameworkCore.SqlServer`, o `UseSqlite` em `Program.cs` e a string de conexão.
