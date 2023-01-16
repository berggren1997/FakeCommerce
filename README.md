# e-commerce api dotnet

###  tech stack:
> - C#
> - .NET Core Web API
> - Microsoft SQL Server
> - Entity Framework Core

### concepts and information about what I used in this project:
> - Dependency injection
> - Repository design pattern
> - Data Transfer Objects (DTOs)
> - Identity for auth
> - Integrated Stripe for session checkouts
> - Middleware for global error handling

The API is consumed by a React Client. [(see project here)](https://github.com/berggren1997/fakecommerce.client) 

To set the project up, follow these steps:

1. Clone the project to your local machine
2. Make sure to has SQL Server installed
3. Navigate to the API project in Package Manager Console, and create the database by using the command "update-database"
4. Make sure to set up your own stripe account to get your key, and paste it in appsettings

# Api endpoints

## Product endpoints

### Get Single Product Request
    
```js
GET /api/product/{productId}
```
### Get Single Product Response

```json
{
  "id": 1,
  "name": "product name",
  "price": 1337,
  "description": "product description",
  "category": "shirt",
  "pictureUrl": "http:exampleimage.com"
}
```

### Get Products Request
```js
GET /api/product
```

### Get Products Response
```js
200 Ok
```
```json
{
  [
    {
      "id": 1,
      "name": "product name",
      "price": 1337,
      "description": "product description",
      "category": "shirt",
      "pictureUrl": "http:exampleimage.com"
    },
    {
      "id": 2,
      "name": "product name",
      "price": 1337,
      "description": "product description",
      "category": "shirt",
      "pictureUrl": "http:exampleimage.com"
    }
  ]
}
```
    
## Shoppingcart endpoints

### Get Shoppingcart Request

```js
GET /api/shoppingcart
```

### Get Shoppingcart Response

```js
200 Ok
```
```json
{
  "id": 1,
  "buyerId": "username or randomly generated string",
  "basketItems": [
    "productId": 1,
    "name": "product name",
    "price": 1337,
    "pictureUrl": "https://example123.com",
    "description": "product description",
    "quantity": 1
  ]
}
```

### Add Item To Shoppingcart Request

```js
POST /api/shoppingcart?productId=1&quantity=1
```
### Add Item To Shoppingcart Response

```js
200 Ok(updated shoppingcart)
```
```json
{
  "id": 1,
  "buyerId": "username or randomly generated string",
  "basketItems": [
    "productId": 1,
    "name": "product name",
    "price": 1337,
    "pictureUrl": "https://example123.com",
    "description": "product description",
    "quantity": 2
  ]
}
```

### Remove Item From Shoppingcart Request
```js
DELETE /api/shoppingcart?productId&quantity=1
```

### Remove Item From Shoppingcart Response
```js
204 No Content
```

## Auth Endpoints

### Register Request

```js 
POST /api/auth/register
```
```json
{
  "username": "bob",
  "email": "bob@example.com",
  "password": "password123",
  "confirmPassword": "password123"
}
```

### Register Response
```js
201 Created
```

### Login Request

```js
POST /api/auth/login
```
```json
{
  "username": "bob",
  "password": "password123"
}
```

### Login Response
```json
{
  "jwt": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
}
```

## Payment Endpoint (Stripe. For more information on how I created the checkout session, see the PaymentService class)

### Create Checkout Session Request
```js
POST /api/payment/create-checkout-session
```

### Create Checkout Session Response
```js
200 Ok
```
```json
{
  "sessionId": "stripeSessionId",
  "sessionUrl": "stripeSessionUrl"
}
```
