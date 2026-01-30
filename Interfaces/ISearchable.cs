using System.Collections.Generic;

namespace LibraryManagementSystem.Interfaces
{
    public interface ISearchable<T>
    {
        T SearchById(int id);
        List<T> SearchByName(string name);
        List<T> GetAll();
    }
}
