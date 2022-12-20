![Logo](https://images.wuzzuf-data.net/files/company_logo/Otlob-Egypt-7940-1599059524-og.png)


# Talabat APIs

 - This is a Talabat Clone Project built in in Onion Architecture



## Authors

- [@Yousef Osama](https://www.linkedin.com/in/yousef-osama-120033216/)


## Features

- Work on Mac,Linux and Windows.
- Support other version Control Systems.


## Installation

- Install Talabat Project with Visual Studio

```bash
  Just clone the project
  Run on Visual Studio
  Test endpoints on Postman 
```
    

## What are the Layers of the Onion Architecture?
 - Onion Architecture uses the concept of layers, but they are different from 3-tier and n-tier architecture layers. Let’s see what each of these layers represents and should contain.


 - `Domain Layer`
   - At the center part of the Onion Architecture, the domain layer exists; this layer represents the business and behavior objects. The idea is to have all of your domain objects at this core. It holds all application domain objects. Besides the domain objects, you also could have domain interfaces. These domain entities don’t have any dependencies. Domain objects are also flat as they should be, without any heavy code or dependencies
 - `Repository Layer`
   - This layer creates an abstraction between the domain entities and business logic of an application. In this layer, we typically add interfaces that provide object saving and retrieving behavior typically by involving a database. This layer consists of the data access pattern, which is a more loosely coupled approach to data access. We also create a generic repository, and add queries to retrieve data from the source, map the data from data source to a business entity, and persist changes in the business entity to the data source.
 - `Services Layer`
   - The Service layer holds interfaces with common operations, such as Add, Save, Edit, and Delete. Also, this layer is used to communicate between the UI layer and repository layer. The Service layer also could hold business logic for an entity. In this layer, service interfaces are kept separate from its implementation, keeping loose coupling and separation of concerns in mind.
- `Presentation Layer`
   - It’s the outer-most layer, and keeps peripheral concerns like UI and tests. For a Web application, it represents the Web API or Unit Test project. This layer has an implementation of the dependency injection principle so that the application builds a loosely coupled structure and can communicate to the internal layer via interfaces.

## API Documnetation

#### Get all Products

```
  GET /api/Products
```



#### Get Product By Id

```
  - GET /api/Products/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int   ` | **Required**. Id of Product to fetch |
