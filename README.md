# ASOS Microservice

## Technologies
- ASP.NET Core 8
- Entity Framework Core 8
- RabbitMQ
- gRPC
- Redis
- SignalR
- MediatR
- AutoMapper
- FluentValidation

## Install Tools
- .NET Core SDK 8
- Git client
- Visual Studio 2022
- PostgreSQL
- Docker

## Branch Convention

- feature/[scope]/description
- ex : feature/product/api-create-product

## Commit Convention

- type[scope]: description
- example:

* feat[product]: api product
* fix[product]: api product
* refractor[product]: api product

## Migration

- add-migration comment {ex: add-migration add-table-product}
- update-database
- Lưu ý : Khi config FK cần thêm trong Infrastructure/Data/Configurations

# ASOS Website

## How to run project

- npm run dev
- npm run json-server (optional)

## Branch Convention

- feature/[scope]/description
- ex : feature/product/api-create-product

## Commit Convention

- type[scope]: description
- example:

* feat[product]: api product
* fix[product]: api product
* refractor[product]: api product
