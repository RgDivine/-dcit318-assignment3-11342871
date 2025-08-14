
# DCIT 318 Programming II - Assignment 3 Solutions

## Table of Contents
1. [Prerequisites](#prerequisites)
2. [Question 1: Finance Management System](#question-1-finance-management-system)
3. [Question 2: Healthcare System](#question-2-healthcare-system)
4. [Question 3: Warehouse Inventory System](#question-3-warehouse-inventory-system)
5. [Question 4: School Grading System](#question-4-school-grading-system)
6. [Question 5: Inventory Logger System](#question-5-inventory-logger-system)
7. [How to Run](#how-to-run)
8. [Key Learning Objectives](#key-learning-objectives)

## Prerequisites

### Required Tools
- .NET 6.0 or later - Download from [Microsoft .NET Downloads](https://dotnet.microsoft.com/download)
- Visual Studio 2022 (recommended) or Visual Studio Code with C# extension
- C# compiler (included with .NET SDK)

### Required Knowledge
- Object-Oriented Programming concepts
- C# language fundamentals
- Understanding of interfaces, inheritance, and polymorphism
- Basic file I/O operations
- Exception handling

### Additional Dependencies
- System.Text.Json (built into .NET 6+) - for JSON serialization in Question 5
- System.IO - for file operations
- System.Collections.Generic - for collections usage

---

## Question 1: Finance Management System

### Overview
This system demonstrates the use of records, interfaces, sealed classes, and inheritance to create a modular finance management application.

### Key Features
- Transaction Record: Immutable data structure using C# records
- Payment Processing: Multiple processors implementing a common interface
- Account Management: Base class with virtual methods and sealed specialization
- Transaction Tracking: Collection-based transaction history

### Core Components

#### 1. Transaction Record
public record Transaction(int Id, DateTime Date, decimal Amount, string Category);
- Uses positional syntax for immutable transaction data
- Automatically provides equality comparison and hash code generation

#### 2. Payment Processors
- ITransactionProcessor Interface: Defines contract for transaction processing
- BankTransferProcessor: Handles bank transfers
- MobileMoneyProcessor: Handles mobile money transactions
- CryptoWalletProcessor: Handles cryptocurrency transactions

#### 3. Account System
- Account Base Class: Provides basic account functionality
- SavingsAccount Sealed Class: Specialized account with overdraft protection
- Demonstrates inheritance and method overriding

### Learning Objectives
- Understanding C# records for immutable data
- Interface implementation and polymorphism
- Sealed classes and inheritance control
- Virtual method overriding

---

## Question 2: Healthcare System

### Overview
Implements a healthcare management system using generics and collections for type-safe and scalable patient record management.

### Key Features
- Generic Repository Pattern: Type-safe data storage and retrieval
- Patient Management: Comprehensive patient information tracking
- Prescription Tracking: Medicine prescription management with patient relationships
- Data Relationships: Dictionary-based patient-prescription mapping

### Core Components

#### 1. Generic Repository
public class Repository<T>
{
    private List<T> items = new List<T>();
    // CRUD operations with generic type safety
}
- Provides Add, GetAll, GetById, and Remove operations
- Uses Func delegates for flexible querying
- Ensures type safety across different entity types

#### 2. Entity Classes
- Patient Class: Stores patient demographic information
- Prescription Class: Links medications to patients with date tracking

#### 3. Healthcare App
- Manages repositories for both patients and prescriptions
- Builds relationship mappings using Dictionary collections
- Provides comprehensive reporting capabilities

### Learning Objectives
- Generic classes and type constraints
- Repository pattern implementation
- Collection types (List, Dictionary) usage
- Func delegates for flexible querying

---

## Question 3: Warehouse Inventory System

### Overview
Demonstrates advanced generics, custom exceptions, and collection management in an inventory control system.

### Key Features
- Generic Inventory Repository: Type-safe item management with constraints
- Multiple Product Types: Electronics and grocery items with different properties
- Custom Exception Handling: Specific exceptions for inventory operations
- Generic Methods: Flexible operations across different item types

### Core Components

#### 1. Inventory Items
public interface IInventoryItem
{
    int Id { get; }
    string Name { get; }
    int Quantity { get; set; }
}
- ElectronicItem: Electronics with warranty information
- GroceryItem: Perishables with expiry dates
- Both implement the IInventoryItem interface

#### 2. Generic Repository with Constraints
public class InventoryRepository<T> where T : IInventoryItem
- Dictionary-based storage using item IDs as keys
- Generic constraints ensure type safety
- Comprehensive CRUD operations

#### 3. Custom Exception System
- DuplicateItemException: Prevents duplicate item addition
- ItemNotFoundException: Handles missing item scenarios
- InvalidQuantityException: Validates quantity constraints

### Learning Objectives
- Generic constraints (where T : interface)
- Custom exception creation and handling
- Dictionary collections for key-based storage
- Generic method implementation

---

## Question 4: School Grading System

### Overview
File processing system that demonstrates file I/O, custom exceptions, and data validation for student grade management.

### Key Features
- File Input Processing: CSV-style data parsing from text files
- Data Validation: Comprehensive input validation with custom exceptions
- Grade Calculation: Automatic grade assignment based on score ranges
- Report Generation: Formatted output with grade distribution statistics

### Core Components

#### 1. Student Class
public class Student
{
    public string GetGrade()
    {
        return Score switch
        {
            >= 80 and <= 100 => "A",
            >= 70 and <= 79 => "B",
            // ... more grade ranges
        };
    }
}
- Uses C# switch expressions for clean grade calculation
- Encapsulates student data and behavior

#### 2. Custom Exceptions
- InvalidScoreFormatException: Handles non-numeric score data
- MissingFieldException: Manages incomplete record scenarios

#### 3. File Processing
- StreamReader/StreamWriter: Efficient file I/O operations
- Using Statements: Ensures proper resource disposal
- Comprehensive Error Handling: Graceful handling of file system errors

### Learning Objectives
- File I/O operations with proper resource management
- Custom exception design and implementation
- Data parsing and validation techniques
- Switch expressions for clean conditional logic

---

## Question 5: Inventory Logger System

### Overview
Advanced system showcasing C# records, generics with constraints, JSON serialization, and comprehensive file operations.

### Key Features
- Immutable Records: C# records for data integrity
- Generic Logging: Type-safe logging system with interface constraints
- JSON Serialization: Modern data persistence using System.Text.Json
- Error Recovery: Robust file operation error handling

### Core Components

#### 1. Immutable Record
public record InventoryItem(int Id, string Name, int Quantity, DateTime DateAdded) : IInventoryEntity;
- Positional record syntax for concise immutable data
- Implements marker interface for type safety

#### 2. Generic Logger
public class InventoryLogger<T> where T : IInventoryEntity
- Generic class with interface constraint
- JSON-based serialization for data persistence
- Comprehensive error handling for file operations

#### 3. Application Integration
- InventoryApp: Orchestrates the entire logging workflow
- Session Simulation: Demonstrates data persistence across application restarts

### Learning Objectives
- C# records for immutable data modeling
- Generic constraints with interfaces
- JSON serialization/deserialization
- Comprehensive file operation error handling

---

## How to Run

### Individual Question Solutions

1. Create a new Console Application:
      dotnet new console -n DCITAssignment3
   cd DCITAssignment3
   

2. Replace Program.cs content with the respective question's code

3. Run the application:
      dotnet run
   

### For Question 4 (Grading System)
The program automatically creates a sample input file students.txt. You can also create your own with the format:
101,Alice Smith,84
102,Bob Johnson,76
103,Charlie Brown,92

### For Question 5 (Inventory Logger)
The program creates a JSON file inventory_log.json for data persistence.

---

## Key Learning Objectives

### 1. Object-Oriented Programming
- Interface design and implementation
- Inheritance and polymorphism
- Sealed classes for inheritance control
- Virtual method overriding

### 2. C# Advanced Features
- Records for immutable data structures
- Generic classes and methods with constraints
- Switch expressions for clean conditional logic
- Using statements for resource management

### 3. Collections and Data Structures
- List<T> for dynamic arrays
- Dictionary<TKey, TValue> for key-based storage
- LINQ integration with Func delegates

### 4. Exception Handling
- Custom exception design
- Proper exception hierarchy
- Try-catch-finally patterns
- Resource cleanup in exception scenarios

### 5. File Operations
- StreamReader/StreamWriter usage
- JSON serialization with System.Text.Json
- File existence checking
- Comprehensive error handling for I/O operations

### 6. Software Design Patterns
- Repository pattern for data access
- Generic repository implementation
- Separation of concerns
- Single responsibility principle

---

## Expected Output Examples

### Question 1 - Finance Management
=== Finance Management System ===
Mobile Money: Processing $150 for Groceries
Transaction applied. Updated balance: $850
Bank Transfer: Processing $200 for Utilities
Transaction applied. Updated balance: $650
Crypto Wallet: Processing $75 for Entertainment
Transaction applied. Updated balance: $575

Final account balance: $575
Total transactions processed: 3

### Question 2 - Healthcare System
=== Healthcare System ===

=== All Patients ===
Patient ID: 1, Name: John Doe, Age: 35, Gender: Male
Patient ID: 2, Name: Jane Smith, Age: 28, Gender: Female
Patient ID: 3, Name: Bob Johnson, Age: 45, Gender: Male

=== Prescriptions for John Doe (ID: 1) ===
Prescription ID: 1, Medication: Aspirin, Date: 2025-08-04
Prescription ID: 2, Medication: Ibuprofen, Date: 2025-08-09

### Question 3 - Warehouse System
=== Warehouse Inventory Management System ===
Sample data added successfully.

=== Grocery Items ===
Grocery: ID=1, Name=Milk, Quantity=50, Expiry=2025-08-21
Grocery: ID=2, Name=Bread, Quantity=30, Expiry=2025-08-17
Grocery: ID=3, Name=Eggs, Quantity=100, Expiry=2025-08-28

=== Electronic Items ===
Electronic: ID=1, Name=Laptop, Quantity=10, Brand=Dell, Warranty=24 months
Electronic: ID=2, Name=Smartphone, Quantity=25, Brand=Samsung, Warranty=12 months
Electronic: ID=3, Name=Tablet, Quantity=15, Brand=Apple, Warranty=12 months

---

## Notes for Students

1. Code Organization: Each solution is structured to demonstrate specific programming concepts clearly
2. Error Handling: All solutions include comprehensive exception handling
3. Best Practices: Code follows C# naming conventions and SOLID principles
4. Extensibility: Solutions are designed to be easily extended with additional features
5. Testing: Each solution includes sample data and test scenarios

## Additional Resources

- [Microsoft C# Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [.NET API Reference](https://docs.microsoft.com/en-us/dotnet/api/)
- [C# Programming Guide](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/)
- [Generic Types Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/)

---

*This assignment demonstrates comprehensive understanding of C# object-oriented programming, generics, collections, file I/O, and exception handling concepts.*# -dcit318-assignment3-11342871