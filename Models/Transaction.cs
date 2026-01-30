using System;

namespace LibraryManagementSystem.Models
{
    public class Transaction
    {
        private int _transactionId;
        private DateTime _borrowDate;
        private DateTime _dueDate;
        private DateTime? _returnDate;
        private bool _isReturned;
        private decimal _fine;

        public int TransactionId
        {
            get => _transactionId;
            set => _transactionId = value > 0 ? value : 0;
        }

        public Book BorrowedBook { get; private set; }
        public Member BorrowingMember { get; private set; }

        public DateTime BorrowDate
        {
            get => _borrowDate;
            private set => _borrowDate = value;
        }

        public DateTime DueDate
        {
            get => _dueDate;
            set => _dueDate = value >= BorrowDate ? value : BorrowDate.AddDays(14);
        }

        public DateTime? ReturnDate
        {
            get => _returnDate;
            private set => _returnDate = value;
        }

        public bool IsReturned
        {
            get => _isReturned;
            private set => _isReturned = value;
        }

        public decimal Fine
        {
            get => _fine;
            private set => _fine = value >= 0 ? value : 0;
        }

        public Transaction(int transactionId, Book book, Member member)
        {
            TransactionId = transactionId;
            BorrowedBook = book ?? throw new ArgumentNullException(nameof(book));
            BorrowingMember = member ?? throw new ArgumentNullException(nameof(member));

            BorrowDate = DateTime.Now;
            DueDate = BorrowDate.AddDays(14);
            IsReturned = false;
            Fine = 0;
        }

    

        public void MarkAsReturned()
        {
            if (IsReturned)
                return;

            ReturnDate = DateTime.Now;
            IsReturned = true;
            CalculateFine();
        }

        public int CalculateDaysOverdue()
        {
            DateTime checkDate = ReturnDate ?? DateTime.Now;

            int days = (checkDate - DueDate).Days;

            return days > 0 ? days : 0;
        }

        public decimal CalculateFine(decimal perDayFine = 5)
        {
            Fine = CalculateDaysOverdue() * perDayFine;
            return Fine;
        }

        public bool IsOverdue()
        {
            return !IsReturned && DateTime.Now > DueDate;
        }

        public void DisplayTransaction()
        {
            Console.WriteLine($"Transaction ID: {TransactionId}");
            Console.WriteLine($"Book: {BorrowedBook.Title} (ID: {BorrowedBook.Id})");
            Console.WriteLine($"Member: {BorrowingMember.Name} (ID: {BorrowingMember.Id})");
            Console.WriteLine($"Borrow Date: {BorrowDate.ToShortDateString()}, Due Date: {DueDate.ToShortDateString()}");
            Console.WriteLine($"Returned: {IsReturned}, Return Date: {(ReturnDate.HasValue ? ReturnDate.Value.ToShortDateString() : "Not yet")}");
            Console.WriteLine($"Fine: {Fine:C}");
        }
    }
}
