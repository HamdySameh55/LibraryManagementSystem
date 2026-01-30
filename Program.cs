using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Data;

namespace LibraryManagementSystem
{
    class Program
    {
        static DataManager dataManager;

        static BookService bookService;
        static MemberService memberService;
        static TransactionService transactionService;
        static FineService fineService;

        static void Main(string[] args)
        {
            // ================= Initialize =================
            dataManager = new DataManager(
                new FileManager<Book>(),
                new FileManager<Member>(),
                new FileManager<Transaction>(),
                new FileManager<Fine>()
            );

            bookService = new BookService(dataManager.Books);
            memberService = new MemberService(dataManager.Members);
            transactionService = new TransactionService(dataManager.Transactions, dataManager.Books, dataManager.Members);
            fineService = new FineService(dataManager.Fines, transactionService);

            bool exit = false;

            while (!exit)
            {
                try
                {
                    ShowMainMenu();
                    Console.Write("Choose option: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1": BooksMenu(); break;
                        case "2": MembersMenu(); break;
                        case "3": BorrowMenu(); break;
                        case "4": FinesMenu(); break;
                        case "5": ReportsMenu(); break;
                        case "6": exit = true; break;
                        default: Console.WriteLine("Invalid choice."); break;
                    }

                    // كل مرة بعد أي عملية نحفظ التغييرات في JSON
                    dataManager.SaveAll();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

                Console.WriteLine("\nPress any key...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        // ================= Main Menu =================
        static void ShowMainMenu()
        {
            Console.WriteLine("===== Library Management System =====");
            Console.WriteLine("1. Books Management");
            Console.WriteLine("2. Members Management");
            Console.WriteLine("3. Borrow & Return");
            Console.WriteLine("4. Fines");
            Console.WriteLine("5. Reports");
            Console.WriteLine("6. Exit");
        }

        // ================= Books Menu =================
        static void BooksMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Books Management ===");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Remove Book");
            Console.WriteLine("3. Search Book");
            Console.WriteLine("4. Show All Books");
            Console.Write("Choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddBook(); break;
                case "2": RemoveBook(); break;
                case "3": SearchBook(); break;
                case "4": ShowAllBooks(); break;
            }
        }

        static void AddBook()
        {
            var book = new Book();

            Console.Write("Title: "); book.Title = Console.ReadLine();
            Console.Write("Author: "); book.Author = Console.ReadLine();
            Console.Write("ISBN: "); book.ISBN = Console.ReadLine();
            Console.Write("Category: "); book.Category = Console.ReadLine();
            Console.Write("Price: "); book.Price = decimal.Parse(Console.ReadLine());
            Console.Write("Total copies: "); book.TotalCopies = int.Parse(Console.ReadLine());
            book.AvailableCopies = book.TotalCopies;

            // ID أوتوماتيكي
            book.Id = dataManager.Books.Any() ? dataManager.Books.Max(b => b.Id) + 1 : 1;

            bookService.AddBook(book);
            Console.WriteLine("Book added successfully.");
        }

        static void RemoveBook()
        {
            Console.Write("Book ID: ");
            int id = int.Parse(Console.ReadLine());
            bookService.RemoveBook(id);
            Console.WriteLine("Book removed.");
        }

        static void SearchBook()
        {
            Console.Write("Enter Author: ");
            string author = Console.ReadLine();
            var result = bookService.SearchByAuthor(author);

            foreach (var b in result)
                Console.WriteLine(b.GetBookInfo());
        }

        static void ShowAllBooks()
        {
            foreach (var b in bookService.GetAll())
                Console.WriteLine(b.GetBookInfo());
        }

        // ================= Members Menu =================
        static void MembersMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Members Management ===");
            Console.WriteLine("1. Register Member");
            Console.WriteLine("2. Renew Membership");
            Console.WriteLine("3. Show All Members");
            Console.Write("Choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": RegisterMember(); break;
                case "2": RenewMember(); break;
                case "3": ShowMembers(); break;
            }
        }

        static void RegisterMember()
        {
            var member = new Member();

            Console.Write("Name: "); member.Name = Console.ReadLine();
            Console.Write("Email: "); member.Email = Console.ReadLine();

            member.Id = dataManager.Members.Any() ? dataManager.Members.Max(m => m.Id) + 1 : 1;

            memberService.RegisterMember(member);
            Console.WriteLine("Member registered.");
        }

        static void RenewMember()
        {
            Console.Write("Member ID: ");
            int id = int.Parse(Console.ReadLine());
            memberService.RenewMembership(id);
            Console.WriteLine("Membership renewed.");
        }

        static void ShowMembers()
        {
            foreach (var m in memberService.GetAll())
                Console.WriteLine($"{m.Id} - {m.Name} - {m.Email}");
        }

        // ================= Borrow & Return =================
        static void BorrowMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Borrow & Return ===");
            Console.WriteLine("1. Borrow Book");
            Console.WriteLine("2. Return Book");
            Console.WriteLine("3. Show Borrowed Books");
            Console.Write("Choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": BorrowBook(); break;
                case "2": ReturnBook(); break;
                case "3": ShowBorrowedBooks(); break;
            }
        }

        static void BorrowBook()
        {
            Console.Write("Book ID: "); int bookId = int.Parse(Console.ReadLine());
            Console.Write("Member ID: "); int memberId = int.Parse(Console.ReadLine());

            transactionService.BorrowBook(bookId, memberId);
            Console.WriteLine("Book borrowed successfully.");
        }

        static void ReturnBook()
        {
            Console.Write("Transaction ID: "); int id = int.Parse(Console.ReadLine());
            transactionService.ReturnBook(id);
            Console.WriteLine("Book returned.");
        }

        static void ShowBorrowedBooks()
        {
            var list = transactionService.GetCurrentBorrowedBooks();
            foreach (var t in list)
                Console.WriteLine($"{t.TransactionId} - {t.BorrowedBook.Title} - {t.BorrowingMember.Name}");
        }

        // ================= Fines Menu =================
        static void FinesMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Fines ===");
            Console.WriteLine("1. Show all fines");
            Console.WriteLine("2. Pay fine");
            Console.Write("Choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": ShowAllFines(); break;
                case "2": PayFine(); break;
            }
        }

        static void ShowAllFines()
        {
            foreach (var f in fineService.GetUnpaidFines())
                Console.WriteLine($"{f.FineId} - Member:{f.MemberId} - {f.Amount}");
        }

        static void PayFine()
        {
            Console.Write("Fine ID: ");
            int id = int.Parse(Console.ReadLine());
            fineService.PayFine(id);
            Console.WriteLine("Fine paid.");
        }

        // ================= Reports Menu =================
        static void ReportsMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Reports ===");
            Console.WriteLine("1. Most borrowed books");
            Console.WriteLine("2. Statistics");
            Console.Write("Choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": MostBorrowedBooks(); break;
                case "2": Statistics(); break;
            }
        }

        static void MostBorrowedBooks()
        {
            var books = transactionService.GetAllTransactions()
                                         .GroupBy(t => t.BorrowedBook)
                                         .OrderByDescending(g => g.Count())
                                         .Take(5)
                                         .Select(g => g.Key);

            foreach (var b in books)
                Console.WriteLine(b.Title);
        }

        static void Statistics()
        {
            Console.WriteLine($"Available books: {bookService.GetAvailableBooks().Count}");
            Console.WriteLine($"Borrowed books: {transactionService.GetCurrentBorrowedBooks().Count}");
            Console.WriteLine($"Active members: {memberService.GetActiveMembers().Count}");
            Console.WriteLine($"Total revenue: {fineService.GetMemberFines(0).Sum(f => f.Amount)}");
        }
    }
}
