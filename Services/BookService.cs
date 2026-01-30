using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Interfaces;

namespace LibraryManagementSystem.Services
{
    public class BookService : ISearchable<Book>, IPrintable
    {
        private readonly List<Book> books;

        public BookService(List<Book> books)
        {
            this.books = books; 
        }

      
        public Book SearchById(int id)
        {
            return books.FirstOrDefault(b => b.Id == id);
        }

        public List<Book> SearchByName(string name)
        {
            return books
                .Where(b => b.Title.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Book> GetAll()
        {
            return books;
        }

        public List<Book> SearchByAuthor(string author)
        {
            return books
                .Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public Book SearchByISBN(string isbn)
        {
            return books.FirstOrDefault(b => b.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase));
        }

        public List<Book> GetAvailableBooks()
        {
            return books.Where(b => b.AvailableCopies > 0).ToList();
        }

        public List<Book> GetBooksByCategory(string category)
        {
            return books.Where(b => b.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // ================= CRUD =================
        public void AddBook(Book book)
        {
            if (book == null) return;
            if (books.Any(b => b.Id == book.Id)) return;
            books.Add(book);
        }

        public void RemoveBook(int bookId)
        {
            var book = books.FirstOrDefault(b => b.Id == bookId);
            if (book != null) books.Remove(book);
        }

        public void UpdateBook(Book updatedBook)
        {
            var book = books.FirstOrDefault(b => b.Id == updatedBook.Id);
            if (book != null)
            {
                book.Title = updatedBook.Title;
                book.Author = updatedBook.Author;
                book.ISBN = updatedBook.ISBN;
                book.Publisher = updatedBook.Publisher;
                book.PublishDate = updatedBook.PublishDate;
                book.Category = updatedBook.Category;
                book.Price = updatedBook.Price;
                book.TotalCopies = updatedBook.TotalCopies;
                book.AvailableCopies = updatedBook.AvailableCopies;
                book.Status = updatedBook.Status;
            }
        }

        public void Delete(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book != null) books.Remove(book);
        }


        public void PrintReceipt()
        {
            Console.WriteLine("=== Books Receipt ===");
            foreach (var b in books)
                Console.WriteLine($"ID: {b.Id}, Title: {b.Title}, Author: {b.Author}");
            Console.WriteLine("====================\n");
        }

        public void PrintReport()
        {
            Console.WriteLine("=== Books Report ===");
            foreach (var b in books)
            {
                Console.WriteLine($"ID: {b.Id}, Title: {b.Title}, Author: {b.Author}, Category: {b.Category}, Available: {b.AvailableCopies}/{b.TotalCopies}");
            }
            Console.WriteLine("===================\n");
        }

        public void PrintDetails()
        {
            foreach (var b in books)
                Console.WriteLine(b.GetBookInfo());
        }
    }
}
