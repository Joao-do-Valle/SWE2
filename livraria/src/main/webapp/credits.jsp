<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Livraria – Créditos</title>
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
                    <a href="credits.jsp" class="nav-link active">Créditos</a>
                </nav>
            </div>
        </header>

        <main class="main-content">
            <div class="credits-container">
                <h1 class="page-title credits-title">🏆 Créditos do Projeto</h1>

                <div class="credits-grid">
                    <div class="credit-card">
                        <div class="credit-avatar">👨‍🎓</div>
                        <h2>André Luiz Nascimento de Andrade</h2>
                        <p class="credit-role">Desenvolvedor</p>
                        <p class="credit-info">RA: CB3032256</p>
                    </div>
                    <div class="credit-card">
                        <div class="credit-avatar">👨‍🎓</div>
                        <h2>João do Valle Seixas Paula</h2>
                        <p class="credit-role">Desenvolvedor</p>
                        <p class="credit-info">RA: CB3030784</p>
                    </div>
                </div>

                <div class="credits-info-card">
                    <h3>Sobre o Projeto</h3>
                    <ul>
                        <li><strong>Disciplina:</strong> Sistemas Web I (CBTSWE – ADS 571)</li>
                        <li><strong>Professor:</strong> Me. Wellington Tuler Moraes</li>
                        <li><strong>Instituição:</strong> IFSP – Campus Cubatão</li>
                        <li><strong>Tecnologias:</strong> Java, JSP, Servlet, JDBC, MySQL, CSS3</li>
                        <li><strong>Padrão:</strong> MVC + DAO</li>
                    </ul>
                </div>
            </div>
        </main>

        <footer class="site-footer">
            <p>IFSP Campus Cubatão &mdash; CBTSWE ADS &mdash; Prof. Wellington Tuler Moraes</p>
        </footer>
    </div>
</body>
</html>
