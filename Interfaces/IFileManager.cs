namespace LibraryManagementSystem.Interfaces
{
    public interface IFileManager<T>
    {
        void Save(string filePath, List<T> data);
        List<T> Load(string filePath);
    }
}
