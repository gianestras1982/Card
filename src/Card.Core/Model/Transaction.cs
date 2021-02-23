using System;

using Card.Core.Model.Types;

namespace Card.Core.Model
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public string Created { get; set; }

        public Transaction()
        {
            DateTime d = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Created = d.ToString();

        }
    }
}
