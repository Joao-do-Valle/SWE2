<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Livraria – <c:choose><c:when test="${book != null}">Editar</c:when><c:otherwise>Novo</c:otherwise></c:choose> Livro</title>
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
                    <a href="BookServlet" class="nav-link">Acervo</a>
                    <a href="credits.jsp" class="nav-link">Créditos</a>
                </nav>
            </div>
        </header>

        <main class="main-content">
            <div class="form-container">
                <div class="form-header">
                    <a href="BookServlet" class="back-link">← Voltar</a>
                    <h1 class="page-title">
                        <c:choose>
                            <c:when test="${book != null}">✏️ Editar Livro</c:when>
                            <c:otherwise>➕ Novo Livro</c:otherwise>
                        </c:choose>
                    </h1>
                </div>

                <div class="form-card">
                    <form action="BookServlet" method="post">
                        <c:choose>
                            <c:when test="${book != null}">
                                <input type="hidden" name="action" value="update"/>
                                <input type="hidden" name="id" value="${book.id}"/>
                            </c:when>
                            <c:otherwise>
                                <input type="hidden" name="action" value="insert"/>
                            </c:otherwise>
                        </c:choose>

                        <div class="form-group">
                            <label for="title">Título</label>
                            <input type="text" id="title" name="title"
                                   placeholder="Ex: Dom Casmurro"
                                   value="${book != null ? book.title : ''}"
                                   required autofocus/>
                        </div>

                        <div class="form-group">
                            <label for="author">Autor</label>
                            <input type="text" id="author" name="author"
                                   placeholder="Ex: Machado de Assis"
                                   value="${book != null ? book.author : ''}"
                                   required/>
                        </div>

                        <div class="form-group">
                            <label for="price">Preço (R$)</label>
                            <input type="number" id="price" name="price"
                                   placeholder="Ex: 39.90"
                                   step="0.01" min="0"
                                   value="${book != null ? book.price : ''}"
                                   required/>
                        </div>

                        <div class="form-actions">
                            <a href="BookServlet" class="btn btn-secondary">Cancelar</a>
                            <button type="submit" class="btn btn-primary">
                                <c:choose>
                                    <c:when test="${book != null}">Salvar Alterações</c:when>
                                    <c:otherwise>Cadastrar Livro</c:otherwise>
                                </c:choose>
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </main>

        <footer class="site-footer">
            <p>IFSP Campus Cubatão &mdash; CBTSWE ADS &mdash; Prof. Wellington Tuler Moraes</p>
        </footer>
    </div>
</body>
</html>
