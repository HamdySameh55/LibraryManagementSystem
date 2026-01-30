using System;

namespace LibraryManagementSystem.Models
{
    public class Fine
    {

        private int _fineId;
        private int _memberId;
        private int _transactionId;
        private decimal _amount;
        private string _reason = string.Empty;
        private DateTime _issueDate;
        private bool _isPaid;
        private DateTime? _paymentDate;


        public int FineId
        {
            get => _fineId;
            set => _fineId = value > 0 ? value : 0;
        }

        public int MemberId
        {
            get => _memberId;
            set => _memberId = value > 0 ? value : 0;
        }

        public int TransactionId
        {
            get => _transactionId;
            set => _transactionId = value > 0 ? value : 0;
        }

        public decimal Amount
        {
            get => _amount;
            set => _amount = value >= 0 ? value : 0;
        }

        public string Reason
        {
            get => _reason;
            set => _reason = string.IsNullOrWhiteSpace(value) ? "Unknown" : value;
        }

        public DateTime IssueDate
        {
            get => _issueDate;
            set => _issueDate = value <= DateTime.Now ? value : DateTime.Now;
        }

        public bool IsPaid
        {
            get => _isPaid;
            private set => _isPaid = value;
        }

        public DateTime? PaymentDate
        {
            get => _paymentDate;
            private set => _paymentDate = value;
        }


        public Fine(int fineId, int memberId, int transactionId, decimal amount, string reason)
        {
            FineId = fineId;
            MemberId = memberId;
            TransactionId = transactionId;
            Amount = amount;
            Reason = reason;
            IssueDate = DateTime.Now;
            IsPaid = false;
            PaymentDate = null;
        }

      
        public void MarkAsPaid()
        {
            if (!IsPaid)
            {
                IsPaid = true;
                PaymentDate = DateTime.Now;
                Console.WriteLine($"Fine {FineId} has been paid on {PaymentDate.Value.ToShortDateString()}.");
            }
            else
            {
                Console.WriteLine($"Fine {FineId} is already paid.");
            }
        }

   
        public void GetFineDetails()
        {
            Console.WriteLine($"Fine ID: {FineId}");
            Console.WriteLine($"Member ID: {MemberId}, Transaction ID: {TransactionId}");
            Console.WriteLine($"Amount: {Amount:C}, Reason: {Reason}");
            Console.WriteLine($"Issue Date: {IssueDate.ToShortDateString()}, Paid: {IsPaid}");
            if (PaymentDate.HasValue)
                Console.WriteLine($"Payment Date: {PaymentDate.Value.ToShortDateString()}");
        }
    }
}
