-- Script de criação do banco de dados
-- Livraria IFSP – CBTSWE ADS 571
-- Autores: [Nome1] e [Nome2]

CREATE DATABASE IF NOT EXISTS livraria
    DEFAULT CHARACTER SET utf8mb4
    DEFAULT COLLATE utf8mb4_unicode_ci;

USE livraria;

CREATE TABLE IF NOT EXISTS books (
    id      BIGINT       NOT NULL AUTO_INCREMENT,
    title   VARCHAR(255) NOT NULL,
    author  VARCHAR(255) NOT NULL,
    price   DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Dados de exemplo
INSERT INTO books (title, author, price) VALUES
('Dom Casmurro',              'Machado de Assis',  34.90),
('O Cortiço',                 'Aluísio Azevedo',   28.50),
('Grande Sertão: Veredas',    'Guimarães Rosa',    59.90),
('Memórias Póstumas de Brás Cubas', 'Machado de Assis', 32.00),
('A Hora da Estrela',         'Clarice Lispector', 39.90);
