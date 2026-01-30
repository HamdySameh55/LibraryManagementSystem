# Library Management System - LibrarySystem

A **C# Console Application** for managing a library efficiently, including books, members, transactions, and fines. The system is designed to be simple, organized, and easy to use.

## 🏆 Project Overview
This project aims to provide a complete library management solution where you can:
- Add, update, and remove books.
- Register members and manage their subscriptions.
- Track book borrowing and returns.
- Calculate fines for late returns.
- Generate reports for most borrowed books and other statistics.

## ⚙️ Features
- **Books Management**: Add new books, remove books, search by author, display all books.
- **Members Management**: Register new members, renew memberships, show all members.
- **Borrow & Return**: Track borrowed books, return books, show currently borrowed books.
- **Fines Management**: Display unpaid fines, pay fines, automatically calculate fines.
- **Reports & Statistics**: Most borrowed books, available vs borrowed books, active members, total revenue from fines.

## 📂 Project Structure
- `Models/` : Core classes like `Book`, `Member`, `Transaction`.
- `Services/` : Business logic like `BookService`, `MemberService`, `TransactionService`.
- `Data/` : Data management and JSON file storage.
- `Enums/` : Enumerations like book status, membership type.
- `Exceptions/` : Custom exceptions for the project.
- `Utils/` : Helper classes and utility functions.
- `Program.cs` : Entry point with console-based user interface.

## 🚀 How to Run
1. Make sure **.NET 9.0 SDK** is installed.
2. Open a terminal inside the project folder.
3. Run the project using:
   ```bash
   dotnet run
