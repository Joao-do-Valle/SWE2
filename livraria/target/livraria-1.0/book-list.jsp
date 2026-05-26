<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/fmt" prefix="fmt"%>
<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Livraria – Lista de Livros</title>
    <link rel="stylesheet" href="css/style.css">
</head>
<body>
    <div class="wrapper">
        <header class="site-header">
            <div class="header-inner">
                <div class="logo">
                    <span class="logo-icon">📚</span>
                    <span class="logo-text">Livraria IFSP</span>
                </div>
                <nav>
                    <a href="BookServlet" class="nav-link active">Acervo</a>
                    <a href="credits.jsp" class="nav-link">Créditos</a>
                </nav>
            </div>
        </header>

        <main class="main-content">
            <div class="page-title-row">
                <h1 class="page-title">Acervo de Livros</h1>
                <a href="BookServlet?action=new" class="btn btn-primary">
                    <span>+</span> Novo Livro
                </a>
            </div>

            <c:if test="${empty listBook}">
                <div class="empty-state">
                    <span class="empty-icon">📖</span>
                    <p>Nenhum livro cadastrado ainda.</p>
                    <a href="BookServlet?action=new" class="btn btn-primary">Cadastrar primeiro livro</a>
                </div>
            </c:if>

            <c:if test="${not empty listBook}">
                <div class="table-card">
                    <table class="data-table">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Título</th>
                                <th>Autor</th>
                                <th>Preço</th>
                                <th>Ações</th>
                            </tr>
                        </thead>
                        <tbody>
                            <c:forEach var="book" items="${listBook}">
                                <tr>
                                    <td class="id-cell">${book.id}</td>
                                    <td class="title-cell">${book.title}</td>
                                    <td>${book.author}</td>
                                    <td class="price-cell">
                                        R$ <fmt:formatNumber value="${book.price}" minFractionDigits="2" maxFractionDigits="2"/>
                                    </td>
                                    <td class="actions-cell">
                                        <a href="BookServlet?action=edit&id=${book.id}" class="btn btn-edit">Editar</a>
                                        <a href="BookServlet?action=delete&id=${book.id}"
                                           class="btn btn-delete"
                                           onclick="return confirm('Deseja excluir o livro \'${book.title}\'?')">Excluir</a>
                                    </td>
                                </tr>
                            </c:forEach>
                        </tbody>
                    </table>
                </div>
            </c:if>
        </main>

        <footer class="site-footer">
            <p>IFSP Campus Cubatão &mdash; CBTSWE ADS &mdash; Prof. Wellington Tuler Moraes</p>
        </footer>
    </div>
</body>
</html>
