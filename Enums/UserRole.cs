namespace LibraryManagementSystem.Enums
{
[Flags] 
public enum UserRole
{
    None = 0,                    
    ViewBooks = 1,               
    BorrowBooks = 2,            
    ManageBooks = 4,            
    ManageMembers = 8,          
    ViewReports = 16,            
    Admin = ViewBooks | BorrowBooks | ManageBooks | ManageMembers | ViewReports  // كل الصلاحيات
}
}