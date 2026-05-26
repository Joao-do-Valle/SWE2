# 📚 Livraria IFSP – TP03 CBTSWE ADS 571

Sistema CRUD de livros desenvolvido com **JSP + Servlet + JDBC + MySQL**.

## 👥 Dupla

- **Aluno 1:** _[André Luiz Nascimento de Andrade]_ — RA: _[CB3032256]_
- **Aluno 2:** _[João do Valle Seixas Paula]_ — RA: _[CB3030784]_

---

## 🛠️ Tecnologias

| Camada | Tecnologia |
|--------|-----------|
| View | JSP + JSTL + CSS3 |
| Controller | Java Servlet |
| Model | POJO (Book) |
| Persistência | JDBC + DAO Pattern |
| Banco | MySQL 8 |
| Build | Maven |
| Servidor | Apache Tomcat 9+ |

## 🗂️ Estrutura do Projeto

```
livraria/
├── pom.xml
├── sql/
│   └── livraria.sql          ← Script do banco
└── src/main/
    ├── java/br/edu/ifsp/cbtswe/
    │   ├── model/Book.java       ← Entidade
    │   ├── dao/BookDAO.java      ← Acesso ao banco
    │   └── servlet/BookServlet.java ← Controller
    └── webapp/
        ├── index.jsp             ← Redireciona para lista
        ├── book-list.jsp         ← Lista de livros
        ├── book-form.jsp         ← Formulário inserir/editar
        ├── credits.jsp           ← Página de créditos (diferencial)
        ├── css/style.css
        └── WEB-INF/web.xml
```

## ⚙️ Como configurar e executar

### 1. Banco de dados

```sql
-- Execute no MySQL:
source livraria/sql/livraria.sql
```

### 2. Configurar credenciais do banco

Abra `BookDAO.java` e ajuste as constantes:

```java
private static final String JDBC_URL      = "jdbc:mysql://localhost:3306/livraria?useSSL=false&serverTimezone=UTC";
private static final String JDBC_USER     = "root";      // seu usuário
private static final String JDBC_PASSWORD = "root";      // sua senha
```

### 3. Build com Maven

```bash
cd livraria
mvn clean package
```

O arquivo `livraria.war` será gerado em `target/`.

### 4. Deploy no Tomcat

- Copie `target/livraria.war` para a pasta `webapps/` do Tomcat.
- Inicie o Tomcat.
- Acesse: `http://localhost:8080/livraria`

---

## 📋 Funcionalidades

- ✅ Listar todos os livros
- ✅ Cadastrar novo livro
- ✅ Editar livro existente
- ✅ Excluir livro (com confirmação)
- ✅ Página de créditos da dupla _(diferencial)_

## 📐 Padrão MVC + DAO

```
BookServlet (Controller) ──► BookDAO (DAO) ──► MySQL
       │
       ▼
 book-list.jsp / book-form.jsp (View)
       │
       ▼
    Book.java (Model)
```
