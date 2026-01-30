using System.Text.Json;
using LibraryManagementSystem.Interfaces;
namespace LibraryManagementSystem.Data
{
    public class FileManager<T> : IFileManager<T>
    {
        public void Save(string filePath, List<T> data)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, json);
        }

        public List<T> Load(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<T>();

            string json = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<List<T>>(json)
                   ?? new List<T>();
        }
    }
}
