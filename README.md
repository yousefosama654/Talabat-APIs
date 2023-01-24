
![Logo](https://images.wuzzuf-data.net/files/company_logo/Otlob-Egypt-7940-1599059524-og.png)

 

# Talabat APIs
This is a Talabat Clone Project built in in Onion Architecture Based on the following Design Patterns:
<blockquote>
 
- Repository Design Pattern.
- Specification Design Pattern.
- UnitOfWork Design Pattern.
- Builder Design Pattern.
</blockquote>


<!-- ## Authors

- [@Yousef Osama](https://www.linkedin.com/in/yousef-osama-120033216/)
 -->

## Features
<blockquote>
 
- Work on Mac,Linux and Windows.
- Support other version Control Systems.
- Implements open for extension closed for modification Principle (OCP).
- Handling with all error types and validations(400-401-404-500).
- Using Lazy and Eager Loading Techniques.
- Adding Custom Middlewares for Authentication and Authoriziation.
 
</blockquote>
 
  ## Libraries Used And Packages
<blockquote>
 
- StackExchange.Redis.
- Microsoft.EntityFrameworkCore.Tools.
- Microsoft.EntityFrameworkCore.SqlServer.
- Microsoft.AspNetCore.Identity.EntityFrameworkCore.
- AutoMapper.Extensions.Microsoft.DependencyInjection. 
- Microsoft.AspNetCore.Authentication.JwtBearer.
- Stripe.net.
</blockquote>
 
 ## Databases
 
 <blockquote>
 
 - TalabatAPIs.
 - TalabatIdentityAPIs.
 - Redis (open source, in-memory database).
 
 </blockquote>
 
 ## Installation

```bash
  Just clone the project
  Run on Visual Studio
  Test endpoints on Postman 
```    
 
<h2 href="#Structure">Project Structure</h2>
 <div> 
  <pre>
├── Talabat APIs
|    ├──Controllers
|    |   ├──AccountController.cs
|    |   ├──BaseAPIController.cs
|    |   ├──BasketController.cs
|    |   ├──ProductsController.cs
|    |   ├──OrdersController.cs
|    |   └──PaymentsController.cs
|    ├──Dtos
|    |   ├──LoginDto.cs
|    |   ├──ProductDto.cs
|    |   ├──RegisterDto.cs
|    |   ├──UserDto.cs
|    |   ├──AddressDto.cs
|    |   ├──BasketItemDto.cs
|    |   ├──CustomerBasketDto.cs
|    |   ├──OrderDto.cs
|    |   ├──OrderToReturnDto.cs
|    |   └──OrderItemDto.cs
|    ├──Errors
|    |   ├──ApiExceptionError.cs
|    |   ├──ApiResponse.cs
|    |   └──ApiValidationErrorResponse.cs
|    ├──Extensions
|    |   ├──AppServicesExtension.cs
|    |   ├──IdentityServicesExtension.cs
|    |   ├──SwaggerServicesExtension.cs
|    |   └──UserManagerExtensions
|    ├──Helpers
|    |   ├──MappingProfiles.cs
|    |   ├──Pagination.cs
|    |   ├──ProductPictureUrlResolver.cs
|    |   └──CachedAttribute.cs
|    ├──Middlewares
|    |   └──ExceptionMiddleware.cs
|    ├──appsettings.json
|    ├──Program.cs
|    └──Startup.cs
├── Talabat.Core
|    ├──Entities
|    |   ├──Identity
|    |   |   ├──Address.cs
|    |   |   └──AppUser.cs
|    |   ├──BaseEntity.cs
|    |   ├──BasketItem.cs
|    |   ├──CustomerBasket.cs
|    |   ├──Product.cs
|    |   ├──ProductBrand.cs
|    |   ├──ProductType.cs
|    |   ├──Order Aggregate
|    |   |   ├──Address.cs
|    |   |   ├──DeliveryMethod.cs
|    |   |   ├──Order.cs
|    |   |   ├──OrderItem.cs
|    |   |   ├──OrderStatus.cs
|    |   |   └──ProductItemOrdered.cs
|    ├──Repositories
|    |   ├──IBasketRepository.cs
|    |   ├──IGenericRepository.cs
|    |   └──IUnitOfWork.cs
|    ├──Servicecs
|    |   ├──ITokenService.cs
|    |   ├──IOrderService.cs
|    |   ├──IPaymentService.cs
|    |   └──IResponseCacheService.cs
|    ├──Specification
|    |   ├──BaseSpecification.cs
|    |   ├──ISpecification.cs
|    |   ├──ProductSpecificationFilterCount.cs
|    |   ├──ProductsSpecParams.cs
|    |   ├──ProductWithBrandAndTypeSpecification.cs
|    |   ├──OrderWithItemsAndDeliveryMethodsSpecification.cs
|    |   └──OrderByPaymentIntentIdSpecification
├── Talabat.Repository
|    ├──Data
|    |   ├──Config
|    |   |   ├──ProductConfiguration.cs
|    |   |   ├──OrderConfiguration.cs
|    |   |   ├──OrderItemConfiguration.cs
|    |   |   └──DeliveryMethodConfiguration.cs
|    |   ├──Migrations
|    |   ├──SpecificationEvaluator.cs
|    |   ├──StoreContext.cs
|    |   ├──StoreContextSeed.cs
|    ├──DataSeed
|    |   ├──brands.json
|    |   ├──products.json
|    |   ├──types.json
|    |   └──delivery.json
|    ├──Identity
|    |   ├──Migrations
|    |   ├──AppIdentityContext.cs
|    |   └──AppIdentityContextSeed.cs
|    ├──BasketRepository.cs
|    ├──GenericRepository.cs
|    ├──UnitOfWork.cs
├── Talabat.Service
|    ├──TokenService.cs
|    ├──OrderService.cs
|    ├──PaymentService.cs
|    └──ResponseCacheService.cs
    </pre>
</div>


## What are the Layers of the Onion Architecture?
 - Onion Architecture uses the concept of layers, but they are different from 3-tier and n-tier architecture layers. Let’s see what each of these layers represents and should contain.


 - `Domain Layer`
   - At the center part of the Onion Architecture, the domain layer exists; this layer represents the business and behavior objects. The idea is to have all of your domain objects at this core. It holds all application domain objects. Besides the domain objects, you also could have domain interfaces. These domain entities don’t have any dependencies. Domain objects are also flat as they should be, without any heavy code or dependencies
 - `Repository Layer`
   - This layer creates an abstraction between the domain entities and business logic of an application. In this layer, we typically add interfaces that provide object saving and retrieving behavior typically by involving a database. This layer consists of the data access pattern, which is a more loosely coupled approach to data access. We also create a generic repository, and add queries to retrieve data from the source, map the data from data source to a business entity, and persist changes in the business entity to the data source.
 - `Service Layer`
   - The Service layer holds interfaces with common operations, such as Add, Save, Edit, and Delete. Also, this layer is used to communicate between the UI layer and repository layer. The Service layer also could hold business logic for an entity. In this layer, service interfaces are kept separate from its implementation, keeping loose coupling and separation of concerns in mind.
- `Presentation Layer`
   - It’s the outer-most layer, and keeps peripheral concerns like UI and tests. For a Web application, it represents the Web API or Unit Test project. This layer has an implementation of the dependency injection principle so that the application builds a loosely coupled structure and can communicate to the internal layer via interfaces.

# API Documnetation


## ProductsController

#### Get All Products Brands

```http
GET /api/Products/Brands
```

#### Get All Products Types  


```http
GET /api/Products/Types
```

#### Get All Products

```http
GET /api/Products?sort=${price}&TypeId=${TypeId}&BrandId=${BrandId}&PageSize=${PageSize}&PageIndex=${PageIndex}&search=${Name}
```

| Parameter | Type     | Status     | Description                       |
| :-------- | :------- | :-------   | :-------------------------------- |
| `sort`    | `string` | **Not Required**| Sort the products by price or name descending or ascending  order|
| `TypeId`  | `int` | **Not Required**| Id of Type to fetch.|
| `BrandId` | `int` | **Not Required**| Id of Brand to fetch.|
| `PageSize`| `int` | **Not Required**| To determine tge page size of products (Pagination).|
| `PageIndex`| `int` | **Not Required**| To get specific page index. |
| `Name`    | `string` | **Not Required**|To search for all prducts that include this word in their Name. |



## BasketController
#### Get Basket By Id
 
```http
GET /api/Basket?Id=${id}
```

| Parameter | Type     |  Status   | Description                       |
| :-------- | :------- | :-------  | :-------------------------------- |
| `Id`    | `string` | **Required**|.Id of Basket to fetch.|

#### Create OR Update Basket

```http
POST /api/Basket
```


```json
{
    "id": "basket1",
    "items": [
        {
            "id": 4,
            "Name": ".NET Black & White Mug",
            "price": 10,
            "quantity": 40,
            "pictureUrl": "https://localhost:5001/images/products/2.png",
            "brand": ".NET",
            "type": "USB Memory Stick"
        }
    ],
        "deliveryMethodId": 1
}

```



#### Delete Basket By Id

```http
DELETE /api/Basket?Id=${id}
```

| Parameter | Type     | Status   |     Description                       |
| :-------- | :------- | :------- | :--------------------------------     |
| `Id`    | `string` | **Required**| Id of Basket to be deleted.|






## AccountController

#### Login
 
```http
POST /api/Account/login
```

```json
{
    "Email": "yousef@gmail.com",
    "Password": "Yousef2*"
}
```

#### Register
 
```http
POST /api/Account/register
```

```json
{
    "DisplayName":"yousef",
    "Email": "yousef@gmail.com",
    "Password": "Yousef2*",
    "PhoneNumber": "01552851677"
}
```

#### Get Current User
 
```http
GET /api/Account/GetCurrentUser
```


#### Get Current User Address
 
```http
GET /api/Account/Address
```

#### Update Current User Address
 
```http
PUT /api/Account/UpdateCurrentUserAddress
```

```json
{
    "FirstName": "yousef new",
    "LastName": "osama new",
    "street": "42-EltalStreet new",
    "city": "Warraq-Giza new",
    "country": "Egypt new"
}
```


## OrdersController

#### Get All Avaliable Delivery Methods


```http
GET /api/Orders/DeliveryMethods
```

#### Get All Orders For Current User
```http
GET /api/Orders
```

#### Get Order By ID For Current User
```http
GET /api/Orders/1
```


#### Create an Order For Current User

```http
POST /api/Orders
```
```json
{
    "basketId": "basket1",
    "deliveryMethodId": 1,
    "shippingaddress": {
        "FirstName": "yousef osama",
        "LastName": "mohamed",
        "street": "42-EltalStreet new",
        "city": "Warraq-Giza new",
        "country": "Egypt new"
    }
}
```


## PaymentsController

### Confirm Payment

- To Confirm Payment with the Gateway and handling stripe Events with success or fail.

```http
POST /Payments/webhook
```

### Create Or Update PaymentIntent

```http
POST /api/Payments?basketId=${basketId}
```

| Parameter | Type     | Status   |     Description                       |
| :-------- | :------- | :------- | :--------------------------------     |
| `Id`    | `string` | **Required**| Id of Basket to be Ordered.|



## Responses

Many API endpoints return the JSON representation of the resources created or edited. However, if an invalid request is submitted, or some other error occurs, Talabat returns a JSON response in the following format:

```javascript
{
  "message" : string,
  "statusCode" : int,
  "Details"    : string
}
```

The `message` attribute contains a message commonly used to indicate errors.

The `statusCode` attribute describes the code of the response due to the follwing Status Codes Table.

The `Details` attribute contains error message only in developing env and in case of internal server error(500).

## Status Codes

Talabat returns the following status codes in its API:

| Status Code | Description |
| :--- | :--- |
| 200 | `OK` |
| 400 | `BAD REQUEST` |
| 401 | `UNAUTHORIZED` |
| 404 | `NOT FOUND` |
| 500 | `INTERNAL SERVER ERROR` |


## FAQ

<details>
  <summary>How To Pay Through Application ?</summary>

 You should enter any future date and any valid three numbers for `CVC` and the card number can be got by
 `Stripe PaymentGateway`.
</details>

<details>
  <summary>What About Perfomance For High Traffic Requests ?</summary>

 The caching is done for only 10 minutes for products loaded before. 
</details>


<details>
 <summary>How To Connect To Redis ?</summary>

- First Install Redis Software.
- Open the Command prompt and type this command `redis-server`.


</details>

 <h2 align='center'>⭐ Authors ⭐ </h2>
<!-- readme: collaborators -start -->
<table align='center'> 
<tr>
    <td align="center">
        <a href="https://github.com/yousefosama654">
            <img src="https://avatars.githubusercontent.com/u/93356614?v=4" width="100;" alt="yousefosama654"/>
            <br />
            <sub><b>Yousef</b></sub>
        </a>
    </td></tr>
</table>
<!-- readme: collaborators -end -->
<h2 align='center'>Thank You. 💖 </h2>
