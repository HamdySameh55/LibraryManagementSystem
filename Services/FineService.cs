using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public class FineService
    {
        private readonly List<Fine> fines;
        private readonly TransactionService transactionService;
        private int nextFineId = 1;
        private const decimal FinePerDay = 2.0m;

        public FineService(List<Fine> fines, TransactionService transactionService)
        {
            this.fines = fines;                 // DataManager.Fines
            this.transactionService = transactionService;
            if (fines.Any())
                nextFineId = fines.Max(f => f.FineId) + 1;
        }

        // ================= Calculate & Issue =================
        public decimal CalculateFine(Transaction transaction)
        {
            if (transaction == null)
                throw new Exception("Transaction is null");

            int daysOverdue = transaction.CalculateDaysOverdue();
            return daysOverdue * FinePerDay;
        }

        public Fine IssueFine(Transaction transaction, string reason)
        {
            if (transaction == null)
                throw new Exception("Transaction is null");

            decimal amount = CalculateFine(transaction);
            if (amount <= 0)
                throw new Exception("No fine for this transaction");

            var fine = new Fine(nextFineId++, transaction.BorrowingMember.Id, transaction.TransactionId, amount, reason);
            fines.Add(fine);
            return fine;
        }

        public Fine IssueFineByTransactionId(int transactionId, string reason)
        {
            var transaction = transactionService.GetAllTransactions()
                                                .FirstOrDefault(t => t.TransactionId == transactionId);
            if (transaction == null)
                throw new Exception("Transaction not found");

            return IssueFine(transaction, reason);
        }

        // ================= Pay & Queries =================
        public void PayFine(int fineId)
        {
            var fine = fines.FirstOrDefault(f => f.FineId == fineId);
            if (fine == null)
                throw new Exception("Fine not found");

            fine.MarkAsPaid();
        }

        public List<Fine> GetMemberFines(int memberId)
        {
            return fines.Where(f => f.MemberId == memberId).ToList();
        }

        public List<Fine> GetUnpaidFines()
        {
            return fines.Where(f => !f.IsPaid).ToList();
        }

        public decimal GetTotalOwed(int memberId)
        {
            return fines.Where(f => f.MemberId == memberId && !f.IsPaid).Sum(f => f.Amount);
        }
    }
}
