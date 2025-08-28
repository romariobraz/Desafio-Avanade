# 🚀 Desafio Técnico - Microserviços

## 📌 Descrição do Desafio

Desenvolver uma aplicação com **arquitetura de microserviços** para
gerenciamento de **estoque de produtos** e **vendas** em uma plataforma
de e-commerce.

O sistema será composto por **dois microserviços**, com comunicação via
**API Gateway** e mensageria com **RabbitMQ**.

------------------------------------------------------------------------

## 🛠️ Tecnologias Utilizadas

-   ⚙️ **.NET Core / C#**
-   🗄️ **Entity Framework**
-   🌐 **RESTful API**
-   📩 **RabbitMQ**
-   🔑 **JWT (JSON Web Token)**
-   🐘 **PostgreSQL** (banco relacional)
-   🚪 **API Gateway**

------------------------------------------------------------------------

## 🏗️ Arquitetura Proposta

### 📦 Microserviço 1 - Gestão de Estoque

-   📝 **Cadastro de Produtos**: nome, descrição, preço e quantidade.
-   🔍 **Consulta de Produtos**: catálogo de produtos + quantidade
    disponível.
-   🔄 **Atualização de Estoque**: redução do estoque ao ocorrer uma
    venda.

### 🛒 Microserviço 2 - Gestão de Vendas

-   🆕 **Criação de Pedidos**: validação do estoque antes de confirmar a
    compra.
-   📜 **Consulta de Pedidos**: status dos pedidos realizados.
-   📢 **Notificação de Venda**: envio de evento ao estoque para reduzir
    a quantidade.

### 🌉 API Gateway

-   Único ponto de entrada da aplicação.
-   Roteia as requisições para o microserviço correto.

### 📬 RabbitMQ

-   Comunicação **assíncrona** entre os microserviços.
-   Usado para envio de notificações de vendas que impactam o estoque.

### 🔒 Autenticação com JWT

-   Apenas usuários **autenticados** podem:
    -   Realizar vendas 🛍️\
    -   Consultar estoque 📦

------------------------------------------------------------------------

## ✅ Status Atual

-   🎯 **Projeto finalizado com sucesso**\
-   ✨ **Sintaxe do projeto sendo melhorada**\
-   🧪 **Testes em desenvolvimento**

------------------------------------------------------------------------

## 📂 Estrutura Básica

``` bash
📦 Avanade
 ┣ 📂 BD
 ┣ 📂 Contexto
 ┣ 📂 Controllers
 ┣ 📂 DTOs
 ┣ 📂 Migrations
 ┣ 📂 RabbitMQ
 ┣ 📜 Program.cs
 ┗ 📜 appsettings.json
```
