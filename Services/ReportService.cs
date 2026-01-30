using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public class ReportService
    {
        private TransactionService transactionService;
        private FineService fineService;
        private BookService bookService;
        private MemberService memberService;

        public ReportService(
            TransactionService transactionService,
            FineService fineService,
            BookService bookService,
            MemberService memberService)
        {
            this.transactionService = transactionService;
            this.fineService = fineService;
            this.bookService = bookService;
            this.memberService = memberService;
        }

        public List<Book> GetMostBorrowedBooks(int count)
        {
            return transactionService.GetAllTransactions()
                .GroupBy(t => t.BorrowedBook)
                .OrderByDescending(g => g.Count())
                .Take(count)
                .Select(g => g.Key)
                .ToList();
        }


        public List<Member> GetMostActiveMembers(int count)
        {
            return transactionService.GetAllTransactions()
                .GroupBy(t => t.BorrowingMember)
                .OrderByDescending(g => g.Count())
                .Take(count)
                .Select(g => g.Key)
                .ToList();
        }


        public MonthlyStatistics GetMonthlyStatistics(int month, int year)
        {
            var transactions = transactionService.GetAllTransactions()
                .Where(t =>
                    t.BorrowDate.Month == month &&
                    t.BorrowDate.Year == year)
                .ToList();

            int totalBorrowed = transactions.Count;

            int returned = transactions.Count(t => t.IsReturned);

            int overdue = transactions.Count(t => t.IsOverdue());

            return new MonthlyStatistics
            {
                Month = month,
                Year = year,
                TotalBorrowed = totalBorrowed,
                ReturnedBooks = returned,
                OverdueBooks = overdue
            };
        }

      
        public decimal GetTotalRevenue()
        {
            decimal total = 0;

            var members = memberService.GetAll();

            foreach (var member in members)
            {
                var fines = fineService.GetMemberFines(member.Id)
                                        .Where(f => f.IsPaid);

                total += fines.Sum(f => f.Amount);
            }

            return total;
        }

          
        public int GetAvailableBooksCount()
        {
            return bookService.GetAvailableBooks().Count;
        }

        
        public int GetBorrowedBooksCount()
        {
            return transactionService
                .GetCurrentBorrowedBooks()
                .Count;
        }

     
        public int GetActiveMembersCount()
        {
            return memberService
                .GetActiveMembers()
                .Count;
        }
    }
    public class MonthlyStatistics
    {
        public int Month { get; set; }
        public int Year { get; set; }

        public int TotalBorrowed { get; set; }
        public int ReturnedBooks { get; set; }
        public int OverdueBooks { get; set; }
    }
}
