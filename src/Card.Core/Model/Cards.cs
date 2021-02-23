using System;
using System.Collections.Generic;

using Card.Core.Model.Types;

namespace Card.Core.Model
{

    public class Cards
    {
        public int CardsId { get; set; }
        public string CardNumber { get; set; } = Guid.NewGuid().ToString();
        public decimal Balance { get; set; } = 0;
        public DateTime Created { get; set; }
        public string Description { get; set; }

        public List<Transaction> Transactions { get; set; }

        public Cards()
        {
            Created = DateTime.Now;
            Transactions = new List<Transaction>();
        }
    }
}
