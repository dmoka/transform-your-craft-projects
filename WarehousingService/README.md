# Warehousing Service - Product Management System

### Description

We need an API endpoint to create products in a data store.

### Endpoint

```
POST /product
```

### Product fields
- id (uuid): The unique identifier of the product.
- name (string): The name of the product.
- price (number): The price of the product.


### Request

The request should include the following parameters in the request body:
- name (string): The name of the product.
- price (number): The price of the product.


### Response
- If the product is created successfully, the API returns 201 Created.
- If the product name is empty or not provided, the API returns 400 Bad Request
- If a product with the same name already exists, the API returns 409 Conflict
