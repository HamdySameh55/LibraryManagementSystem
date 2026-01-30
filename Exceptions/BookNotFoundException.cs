namespace LibraryManagementSystem.Exceptions
{
    public class BookNotFoundException : LibraryException
    {
        public BookNotFoundException(int bookId)
            : base($"Book with ID {bookId} was not found.")
        {
        }
    }
}
