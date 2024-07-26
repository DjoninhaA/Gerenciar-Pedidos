# Desafio Técnico - Gerenciamento de Pedidos
> Descrição

Este projeto é uma aplicação de gerenciamento de pedidos com funcionalidades de CRUD (Create, Read, Update, Delete) e suporte a filtros e paginação. O objetivo é demonstrar habilidades de desenvolvimento com ASP.NET Core, Entity Framework Core e práticas de design de APIs RESTful.

## Funcionalidades
>Listagem de Pedidos: Listagem paginada de pedidos com opção de filtro por status (aberto/fechado).

>Criação de Pedidos: Criação de novos pedidos com detalhes de produtos.

>Atualização de Pedidos: Atualização de pedidos existentes, incluindo a modificação de quantidades e remoção de produtos.

>Remoção de Pedidos: Exclusão de pedidos.

>Fechamento de Pedidos: Atualização do status do pedido para fechado.

## Tecnologias Utilizadas


* ASP.NET Core: Framework para desenvolvimento da API.

* Entity Framework Core: ORM para acesso ao banco de dados.

* SQL Server/MySQL: Banco de dados para armazenamento de dados.

* DTOs: Data Transfer Objects para transferência de dados entre camadas.
  
* Swagger: Para documentação da API.


## Endpoints 

>POST /orders

```
{
  "orderDate": "2024-07-24T17:30:00Z",
  "isClosed": false,
  "orderDetails": [
    {
      "productId": 1,
      "quantity": 3
    },
    {
      "productId": 2,
      "quantity": 5
    }
  ]
}
```
>PUT /orders/{id}

```
{
  "orderDate": "2024-07-24T17:30:00Z",
  "isClosed": false,
  "orderDetails": [
    {
      "productId": 1,
      "quantity": 10
    },
    {
      "productId": 2,
      "quantity": 0
    }
  ]
}
```

>PATCH /orders/{id}/close

```
{
  "isClosed": true
}
```

DELETE /orders/{id}

> basta passar o ID que deseja ser deletado
