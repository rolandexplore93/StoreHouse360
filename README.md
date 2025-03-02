# Storehouse Management System (Back-End)

Storehouse Management System is a robust back-end service built with .NET Core 8. It supports a wide range of warehousing and accounting operations, including inventory tracking, invoice handling, payment processing, and more. With 90+ APIs, this project aims to serve as a scalable solution for businesses that need comprehensive control over their warehouse operations and financial records.

## Usage
* Swagger UI
Go to [StoreHouse backend APIs on Swagger](https://storehouse360-backend.runasp.net/swagger/index.html) to interact with the endpoints.
<img width="756" alt="image" src="https://github.com/user-attachments/assets/79a5116e-ccc0-47df-bd4f-8470d4a83f40" />


## Key Features

* ### Warehousing Operations
    * Manage stock levels, track inventory movements, and receive alerts for low product levels.
* ### Flexible Invoicing
    * Generate various invoice types (e.g., Sales, Purchases, Returns, Imports, Exports). Handle discounts, payment terms, and debt tracking for each invoice.
* ### Payments & Debt Management
    * Pay invoices, monitor outstanding balances, and integrate discounts seamlessly into the payment process.
* ### Product Conversions
    * Split or combine different products to optimize inventory handling. This feature is especially useful for packaging or repacking processes.
* ### Extended Inventory Filters & Grouping
    * Access advanced filtering options and group inventory data in multiple ways (e.g., by product type, location, or supplier).
* ### Accounting Module
    * Journal Entries: Create entries that log financial transactions and maintain ledger accuracy.
    * Account Statements: Monitor individual account balances, debts, and credits over specific periods.
* ### Role-Based Access & Authorization
    * Assign roles to users and enforce policies for accessing system resources. This ensures data is protected according to organizational policies.

<br>

## Technologies

* [ASP.NET Core 8](https://learn.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-8.0)
* [Entity Framework Core 8](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR) for mediator-based CQRS
* [AutoMapper](https://automapper.org/) for object mapping
* [FluentValidation](https://fluentvalidation.net/) for request/response validation
* [Swagger](https://swagger.io/) for interactive API documentation
* [NUnit](https://docs.nunit.org/articles/nunit/intro.html) for unit tests


## Architectural Approach

This project utilized the principles of Clean Architecture to maintain a clear separation of concerns.

   - Domain Layer
Contains entities and core business rules.
Models the real-world concepts (e.g., Products, Invoices, Users) and their behaviors.
   - Application Layer
Implements system use cases through Commands and Queries.
Coordinates data flow between the Domain layer and external services.
Uses CQRS (Command Query Responsibility Segregation) to separate read and write operations.
   - Infrastructure Layer
Provides implementations for data persistence (e.g., Entity Framework) and other external services.
Contains Repositories that abstract away database access, making it easier to switch technologies if needed.
   - Presentation Layer (API Controllers)
Exposes endpoints to the outside world.
Handles HTTP request/response logic and delegates processing to the Application layer.

## CQRS (Command Query Responsibility Segregation)
- Queries retrieve data without modifying the database.
- Commands perform specific tasks like creating invoices, closing orders, or processing payments.
- This approach simplifies maintenance and testing by separating read and write concerns.
  
## Repository Pattern
- Repositories centralize and encapsulate data-access logic.
- They provide a clear interface for the Application layer to interact with the database.
- Encourages loose coupling by isolating domain logic from data persistence details.




## Authors
[@Roland Orobola O.](linkedin.com/in/orobola-roland-ogundipe)

## Acknowledgments
A special thanks to [@Ahmad-Hamwi](https://www.linkedin.com/in/ahmad-hamwi-31566616a/) and [@Abdulrahman-Tayara](https://www.linkedin.com/in/abdulrahman-tayara/) for inspiring the architectural and design patterns utilized here. Their work on WareHousing & Accounting MS solution provided a crucial springboard for the development of these APIs.
