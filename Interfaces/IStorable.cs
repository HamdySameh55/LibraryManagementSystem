namespace LibraryManagementSystem.Interfaces
{
    public interface IStorable<T>
    {
        void Save(string filePath);      
        List<T> Load(string filePath);   
        void Delete(int id);            
    }
}
