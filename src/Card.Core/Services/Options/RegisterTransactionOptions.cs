using Card.Core.Model.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Card.Core.Services.Options
{
    public class RegisterTransactionOptions
    {
        public int cardsId { get; set; }
        public TransactionType TransType { get; set; }
        public decimal Amount { get; set; }
    }
}
