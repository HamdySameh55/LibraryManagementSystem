using System.Security.Principal;
using LibraryManagementSystem.Enums;
namespace LibraryManagementSystem.Models

{
 public class Book
{
       private int _id;
        private string _title = string.Empty;
        private string _author = string.Empty;
        private string _isbn = string.Empty;
        private string _publisher = string.Empty;
        private DateTime? _publishDate;
        private string _category = string.Empty;
        private decimal _price;
        private int _totalCopies;
        private int _availableCopies;
        private BookStatus _status;



        public int Id
        {
            get => _id;
            set => _id = value > 0 ? value : 0;
        }

        public string Title
        {
            get => _title;
            set => _title = string.IsNullOrWhiteSpace(value) ? "Unknown Title" : value;
        }

        public string Author
        {
            get => _author;
            set => _author = string.IsNullOrWhiteSpace(value) ? "Unknown Author" : value;
        }

        public string ISBN
        {
            get => _isbn;
            set => _isbn = string.IsNullOrWhiteSpace(value) ? "N/A" : value;
        }

        public string Publisher
        {
            get => _publisher;
            set => _publisher = string.IsNullOrWhiteSpace(value) ? "Unknown Publisher" : value;
        }

        public DateTime? PublishDate
        {
            get => _publishDate;
            set
            {
                if (value == null || value <= DateTime.Now)
                    _publishDate = value;
                else
                    _publishDate = null; 
            }
        }

        public string Category
        {
            get => _category;
            set => _category = string.IsNullOrWhiteSpace(value) ? "General" : value;
        }

        public decimal Price
        {
            get => _price;
            set => _price = value >= 0 ? value : 0;
        }

        public int TotalCopies
        {
            get => _totalCopies;
            set => _totalCopies = value >= 0 ? value : 0;
        }

        public int AvailableCopies
        {
            get => _availableCopies;
            set
            {
                if (value >= 0 && value <= TotalCopies)
                    _availableCopies = value;
                else
                    _availableCopies = TotalCopies;
            }
        }

        public BookStatus Status
        {
            get => _status;
            set => _status = value;
        }
     
   

        public Book()
        {
           
        }
           public bool BorrowBook()
        {
            if (AvailableCopies > 0)
            {
                AvailableCopies--;
                if(AvailableCopies == 0 )
                {
                    Status = BookStatus.Borrowed ;
                }
                else
                {
                    Status = BookStatus.Available ; 
                }
                return true ;}
                else {return false ;}
            
    }
    public void ReturnBook()
        {
          if (AvailableCopies < TotalCopies)
            {
                AvailableCopies++;
                Status = BookStatus.Available;
            }
        }
         public bool IsAvailable()
        {
            return AvailableCopies > 0;
        }

    public string GetBookInfo()
        {
            return $"ID: {Id}\nTitle: {Title}\nAuthor: {Author}\n" +
                   $"Category: {Category}\nPrice: {Price} EGP\n" +
                   $"Available Copies: {AvailableCopies}/{TotalCopies}\n" +
                   $"Status: {Status}";
        }




}
}