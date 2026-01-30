using LibraryManagementSystem.Models;
using LibraryManagementSystem.Interfaces;

namespace LibraryManagementSystem.Data
{
    public class DataManager
    {
        private readonly IFileManager<Book> _bookFileManager;
        private readonly IFileManager<Member> _memberFileManager;
        private readonly IFileManager<Transaction> _transactionFileManager;
        private readonly IFileManager<Fine> _fineFileManager;

        private const string BooksFile = "books.json";
        private const string MembersFile = "members.json";
        private const string TransactionsFile = "transactions.json";
        private const string FinesFile = "fines.json";

        public List<Book> Books { get; private set; }
        public List<Member> Members { get; private set; }
        public List<Transaction> Transactions { get; private set; }
        public List<Fine> Fines { get; private set; }

        public DataManager(
            IFileManager<Book> bookFileManager,
            IFileManager<Member> memberFileManager,
            IFileManager<Transaction> transactionFileManager,
            IFileManager<Fine> fineFileManager)
        {
            _bookFileManager = bookFileManager;
            _memberFileManager = memberFileManager;
            _transactionFileManager = transactionFileManager;
            _fineFileManager = fineFileManager;

            LoadAll();
        }

        public void LoadAll()
        {
            Books = _bookFileManager.Load(BooksFile);
            Members = _memberFileManager.Load(MembersFile);
            Transactions = _transactionFileManager.Load(TransactionsFile);
            Fines = _fineFileManager.Load(FinesFile);
        }

        public void SaveAll()
        {
            _bookFileManager.Save(BooksFile, Books);
            _memberFileManager.Save(MembersFile, Members);
            _transactionFileManager.Save(TransactionsFile, Transactions);
            _fineFileManager.Save(FinesFile, Fines);
        }
    }
}
