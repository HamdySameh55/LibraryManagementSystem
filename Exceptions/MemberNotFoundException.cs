namespace LibraryManagementSystem.Exceptions
{
    public class MemberNotFoundException : LibraryException
    {
        public MemberNotFoundException(int memberId)
            : base($"Member with ID {memberId} was not found.")
        {
        }
    }
}
