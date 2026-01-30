using LibraryManagementSystem.Enums;
using System;


namespace LibraryManagementSystem.Models
{
    public enum MembershipType
    {
        Basic,
        Gold,
        Platinum
    }
    public class Member : Person
    {
        private MembershipType _membershipType;
        private DateTime _membershipStartDate;
        private DateTime _membershipEndDate;
        private int _maxBooksAllowed;
        private int _currentBorrowedBooks;
        private decimal _totalFines;
        private bool _isActive;

       private List<string> _borrowingHistory = new List<string>();


        public MembershipType MembershipType
        {
            get => _membershipType;
            set => _membershipType = value;
        }

        public DateTime MembershipStartDate
        {
            get => _membershipStartDate;
            set
            {
                if (value <= DateTime.Now)
                    _membershipStartDate = value;
            }
        }

        public DateTime MembershipEndDate
        {
            get => _membershipEndDate;
            set
            {
                if (value >= MembershipStartDate)
                    _membershipEndDate = value;
            }
        }

        public int MaxBooksAllowed
        {
            get => _maxBooksAllowed;
            set => _maxBooksAllowed = value >= 0 ? value : 0;
        }

        public int CurrentBorrowedBooks
        {
            get => _currentBorrowedBooks;
            set
            {
                if (value >= 0 && value <= MaxBooksAllowed)
                    _currentBorrowedBooks = value;
                else
                    _currentBorrowedBooks = MaxBooksAllowed;
            }
        }

        public decimal TotalFines
        {
            get => _totalFines;
            set => _totalFines = value >= 0 ? value : 0;
        }

        public bool IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }

        public List<string> BorrowingHistory => _borrowingHistory;

           public Member()
        {
            MembershipStartDate = DateTime.Now;
            MembershipEndDate = MembershipStartDate.AddYears(1); 
            MaxBooksAllowed = 3; 
            CurrentBorrowedBooks = 0;
            TotalFines = 0;
            IsActive = true;
        }  
        
        public bool CanBorrowBook()
        {
            return IsActive && CurrentBorrowedBooks < MaxBooksAllowed;
        }
        public void BorrowBook(string bookTitle)
        {
            if (CanBorrowBook())
            {
                CurrentBorrowedBooks++;
                _borrowingHistory.Add($"{DateTime.Now.ToShortDateString()} - Borrowed: {bookTitle}");
            }
            else
            {
                Console.WriteLine("Cannot borrow book: limit reached or membership inactive.");
            }
        }
         public void ReturnBook(string bookTitle)
        {
            if (CurrentBorrowedBooks > 0)
            {
                CurrentBorrowedBooks--;
                _borrowingHistory.Add($"{DateTime.Now.ToShortDateString()} - Returned: {bookTitle}");
            }
        }

        public void RenewMembership(int additionalMonths = 12)
        {
            MembershipEndDate = MembershipEndDate.AddMonths(additionalMonths);
            IsActive = true;
        }

        public void PayFine(decimal amount)
        {
            if (amount > 0)
            {
                TotalFines -= amount;
                if (TotalFines < 0) TotalFines = 0;
            }
        }

        public void GetBorrowingHistory()
        {
            Console.WriteLine($"Borrowing History for {Name}:");
            foreach (var record in _borrowingHistory)
            {
                Console.WriteLine(record);
            }
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Member: {Name}, Email: {Email}, Phone: {Phone}");
            Console.WriteLine($"Membership: {MembershipType}, Active: {IsActive}");
            Console.WriteLine($"Books borrowed: {CurrentBorrowedBooks}/{MaxBooksAllowed}, Total fines: {TotalFines:C}");
        }
    }



    }