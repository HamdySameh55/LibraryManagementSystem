using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public class TransactionService
    {
        private readonly List<Transaction> transactions;
        private readonly List<Book> books;
        private readonly List<Member> members;
        private int nextTransactionId = 1;

        public TransactionService(List<Transaction> transactions, List<Book> books, List<Member> members)
        {
            this.transactions = transactions; 
            this.books = books;               
            this.members = members;          

            if (transactions.Any())
                nextTransactionId = transactions.Max(t => t.TransactionId) + 1;
        }


        public Transaction BorrowBook(int bookId, int memberId)
        {
            var book = books.FirstOrDefault(b => b.Id == bookId);
            var member = members.FirstOrDefault(m => m.Id == memberId);

            if (book == null)
                throw new Exception("Book not found");
            if (member == null)
                throw new Exception("Member not found");
            if (book.AvailableCopies <= 0)
                throw new Exception("Book not available");
            if (member.CurrentBorrowedBooks >= member.MaxBooksAllowed)
                throw new Exception("Member cannot borrow more books");

            book.AvailableCopies--;
            member.CurrentBorrowedBooks++;

            var transaction = new Transaction(nextTransactionId++, book, member);
            transactions.Add(transaction);

            return transaction;
        }

        public void ReturnBook(int transactionId)
        {
            var transaction = transactions.FirstOrDefault(t => t.TransactionId == transactionId);

            if (transaction == null)
                throw new Exception("Transaction not found");
            if (transaction.IsReturned)
                throw new Exception("Book already returned");

            transaction.BorrowedBook.AvailableCopies++;
            transaction.BorrowingMember.CurrentBorrowedBooks--;
            transaction.MarkAsReturned();
        }

        public void ExtendBorrowPeriod(int transactionId, int days)
        {
            if (days <= 0)
                throw new Exception("Days must be positive");

            var transaction = transactions.FirstOrDefault(t => t.TransactionId == transactionId);

            if (transaction == null)
                throw new Exception("Transaction not found");
            if (transaction.IsReturned)
                throw new Exception("Cannot extend returned book");

            transaction.DueDate = transaction.DueDate.AddDays(days);
        }

        // ================= Queries =================
        public List<Transaction> GetAllTransactions()
        {
            return transactions.ToList();
        }

        public List<Transaction> GetMemberTransactions(int memberId)
        {
            return transactions.Where(t => t.BorrowingMember.Id == memberId).ToList();
        }

        public List<Transaction> GetOverdueTransactions()
        {
            return transactions.Where(t => t.IsOverdue() && !t.IsReturned).ToList();
        }

        public List<Transaction> GetCurrentBorrowedBooks()
        {
            return transactions.Where(t => !t.IsReturned).ToList();
        }
    }
}
