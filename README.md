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
  "orderId": 0,
  "orderDate": "2024-07-26T13:10:11.820Z",
  "isClosed": true,
  "orderDetails": [
    {
      "productId": 0, //deixar vazio
      "productName": "string", 
      "quantity": 0, 
      "unitPrice": 0 //deixar vazio
    }
  ],
  "totalPrice": 0 //deixar vazio
}
```
>PUT /orders/{id}

```
{
  "orderDetails": [
    {
      "productId": 0,  //deixar vazio
      "productName": "string",
      "quantity": 0,
      "unitPrice": 0 //deixar vazio
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

```
> Basta passar o ID que deseja ser deletado
```

## Para criar e listar Produtos:

>POST /products/create

```
{
  "productName": "Nome do Produto",
  "price": 10,
  "orderDetails": []
}
```
>GET /products

```
Basta fazer a requisição a URL e ela vai retornar uma listagem dos produtos cadastrados.

```

