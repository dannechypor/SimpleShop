# SimpleShop - E-Commerce Platform

> **Note:** This project was originally created by me as my very first written project 6 years ago. I have recently updated it and added some improvements and fixes to modernize the codebase and enhance functionality.

## Overview

SimpleShop is an e-commerce web application built with ASP.NET Core, following a clean architecture pattern with separation of concerns. The application provides a complete online shopping experience with product catalog, shopping cart, order management, user authentication, and administrative features.

## Technology Stack

### Backend
- **.NET 8.0** - Target framework
- **ASP.NET Core MVC** - Web framework
- **Entity Framework Core** - ORM for database operations
- **AutoMapper** - Object-to-object mapping
- **ASP.NET Core Identity** - Authentication and authorization

### Frontend
- **Razor Views** - Server-side rendering
- **Bootstrap** - CSS framework
- **jQuery** - JavaScript library
- **Ion Range Slider** - Price range slider component

### Database
- **SQL Server** (Production)
- **In-Memory Database** (Development)

## Architecture

The project follows a **3-tier architecture** with clear separation of concerns:

```
SimpleShop/
├── SimpleShop.Model/          # Domain models, DTOs, ViewModels, Entities
├── SimpleShop.DAL/            # Data Access Layer (Repositories, DbContext)
├── SimpleShop.BLL/            # Business Logic Layer (Services, Interfaces)
└── SimpleShop.Web/            # Presentation Layer (Controllers, Views)
```

### Layer Responsibilities

- **Model**: Contains domain entities, DTOs (Data Transfer Objects), ViewModels, and enums
- **DAL (Data Access Layer)**: Handles all database operations through repositories and Entity Framework Core
- **BLL (Business Logic Layer)**: Contains business logic, validation, and service implementations
- **Web**: MVC controllers, Razor views, and static assets

## Features

### Customer Features
- ✅ **Product Catalog** with advanced filtering (category, manufacturer, price range, search)
- ✅ **Product Details** with images and descriptions
- ✅ **Shopping Cart** with cookie-based persistence
- ✅ **Wish List** functionality
- ✅ **Order Management** - View order history and status
- ✅ **Product Reviews** and ratings
- ✅ **User Authentication** - Registration and login
- ✅ **Pagination** for product listings
- ✅ **Sorting** by name and price (ascending/descending)

### Administrative Features
- ✅ **Product Management** - Add, update, delete products
- ✅ **Category Management** - CRUD operations for categories
- ✅ **Manufacturer Management** - CRUD operations for manufacturers
- ✅ **Order Management** - View all orders and change order status
- ✅ **User Management** - Create and manage user accounts

### Technical Features
- ✅ **Caching** - Memory caching for frequently accessed data (categories, manufacturers)
- ✅ **Logging** - Comprehensive error logging using ILogger
- ✅ **Transactions** - Database transactions for complex operations (order creation)
- ✅ **Validation** - Input validation and error handling
- ✅ **AutoMapper** - DTO mapping for clean data transfer

## Project Structure

```
SimpleShop.Model/
├── DTO/              # Data Transfer Objects
├── Entities/         # Domain entities
│   └── Enums/       # Enumeration types
└── ViewModels/      # View models for Razor pages

SimpleShop.DAL/
├── EF/              # Entity Framework DbContext
├── Interfaces/       # Repository interfaces
├── Repositories/    # Repository implementations
└── DbInitialze/     # Database initialization

SimpleShop.BLL/
├── Interfaces/       # Service interfaces
├── Services/        # Service implementations
└── Infrastructure/  # Helper classes (OperationDetails, validators)

SimpleShop.Web/
├── Controllers/     # MVC controllers
├── Views/          # Razor views
├── Components/     # View components
├── Mapping/         # AutoMapper profiles
└── wwwroot/        # Static files (CSS, JS, images)
```

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 / Rider / VS Code
- SQL Server (for production) or SQL Server Express

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd SimpleShop
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Configure database connection** (optional for development)
   - Update `appsettings.json` with your SQL Server connection string
   - Or use in-memory database (default for development)

4. **Run the application**
   ```bash
   dotnet run --project SimpleShop.Web
   ```

5. **Access the application**
   - Navigate to `https://localhost:5001` or `http://localhost:5000`

### Default Configuration

The application uses **In-Memory Database** by default in development mode, which means:
- No database setup required for initial testing
- Data is reset on each application restart
- Perfect for development and testing

## Key Improvements Made

### Code Quality
- ✅ Removed code duplication across services
- ✅ Added comprehensive input validation
- ✅ Improved error handling with proper exception management
- ✅ Added logging throughout the application

### Functionality
- ✅ Added update operations for Products, Categories, and Manufacturers
- ✅ Implemented pagination for list views
- ✅ Added caching for frequently accessed data
- ✅ Enhanced filtering and search capabilities
- ✅ Fixed N+1 query problems

### User Experience
- ✅ Improved product catalog with advanced filters
- ✅ Dynamic price range slider
- ✅ Better visual feedback for active filters
- ✅ Enhanced pagination with page numbers
- ✅ Improved product image handling

## Services Overview

### CatalogService
- Product filtering by category and manufacturer
- Product details retrieval

### ProductService
- Product CRUD operations
- Top-selling products
- Pagination support

### CategoryService
- Category management with caching
- Pagination support

### ManufacturerService
- Manufacturer management with caching
- Pagination support

### OrderService
- Order creation with transaction support
- Order status management
- Order history retrieval

### ShoppingCartService
- Cookie-based cart persistence
- Add/remove items
- Cart total calculation

### WishListService
- Add/remove items from wish list
- Optimized queries to prevent N+1 problems

### ReviewService
- Product reviews with AutoMapper
- Average rating calculation

## Database Schema

### Main Entities
- **Product** - Product information with images, price, dimensions
- **Category** - Product categories
- **Manufacturer** - Product manufacturers
- **Order** - Customer orders
- **OrderDetail** - Order line items
- **Review** - Product reviews and ratings
- **ShoppingCartItem** - Shopping cart items
- **WishListItem** - Wish list items
- **ApplicationUser** - User accounts (ASP.NET Identity)

## Configuration

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SimpleShop;Trusted_Connection=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## Development Notes

### Recent Refactoring
- Removed duplicate methods (`GetAllCategory`, `GetAllManufacturer`, `GetAllProduct`) from multiple services
- Centralized common operations through specialized services
- Added proper dependency injection for all services
- Improved error messages and user feedback

### Known Limitations
- Some legacy code patterns from the original implementation
- In-memory database used by default (data not persisted)
- Basic authentication (no OAuth providers)

## Future Enhancements

Potential improvements for future versions:
- [ ] API endpoints for mobile applications
- [ ] Payment gateway integration
- [ ] Email notifications
- [ ] Advanced search with Elasticsearch
- [ ] Product image upload functionality
- [ ] Multi-language support
- [ ] Admin dashboard with analytics
- [ ] Unit and integration tests

## License

This project is for educational purposes and personal use.

## Author

Originally created as a learning project, now updated and improved.

---

**Note:** This project serves as a demonstration of e-commerce application development using ASP.NET Core and follows best practices for clean architecture and separation of concerns.

