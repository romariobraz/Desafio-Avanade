Entendendo Desafio Técnico - Microserviços
Descrição do Desafio
Desenvolver uma aplicação com arquitetura de microserviços para gerenciamento de estoque de produtos e vendas em uma plataforma de e-commerce. O sistema será composto por dois microserviços: um para gerenciar o estoque de produtos e outro para gerenciar as vendas, com comunicação entre os serviços via API Gateway. 

Tecnologias: .NET Core, C#, Entity Framework, RESTful API, RabbitMQ, JWT e banco de dados relacional (Na ocasião usei PostgreSQL).
<img width="587" height="564" alt="image" src="https://github.com/user-attachments/assets/0fe3c2db-9b1a-40d6-b375-491aeeb4cf64" />
Arquitetura Proposta 
Microserviço 1 (Gestão de Estoque): 
Responsável por cadastrar produtos, controlar o estoque e fornecer informações sobre a quantidade disponível. 

Microserviço 2 (Gestão de Vendas): 
Responsável por gerenciar os pedidos e interagir com o serviço de estoque para verificar a disponibilidade de produtos ao realizar uma venda. 

API Gateway: 
Roteamento das requisições para os microserviços adequados. Este serviço atua como o ponto de entrada para todas as chamadas de API. 

RabbitMQ: 
Usado para comunicação assíncrona entre os microserviços, como notificações de vendas que impactam o estoque. 

Autenticação com JWT: 
Garantir que somente usuários autenticados possam realizar ações de vendas ou consultar o estoque.

Funcionalidades Requeridas
Microserviço 1 (Gestão de Estoque): 

Cadastro de Produtos: Adicionar novos produtos com nome, descrição, preço e quantidade em estoque. 

Consulta de Produtos: Permitir que o usuário consulte o catálogo de produtos e a quantidade disponível em estoque. 

Atualização de Estoque: O estoque deve ser atualizado quando ocorrer uma venda (integração com o Microserviço de Vendas). 

Microserviço 2 (Gestão de Vendas): 

Criação de Pedidos: Permitir que o cliente faça um pedido de venda, com a validação do estoque antes de confirmar a compra. 

Consulta de Pedidos: Permitir que o usuário consulte o status dos pedidos realizados. 

Notificação de Venda: Quando um pedido for confirmado, o serviço de vendas deve notificar o serviço de estoque sobre a redução do estoque. 

Comum aos dois microserviços: 

Autenticação via JWT: Apenas usuários autenticados podem interagir com os sistemas de vendas ou consultar o estoque. 

API Gateway: Usar um gateway para centralizar o acesso à API, garantindo que as requisições sejam direcionadas ao microserviço correto

Status Atual:

Projeto finalizado com sucesso.

Sintaxe do projeto sendo melhorada.

Testes em desenvolvimento.
