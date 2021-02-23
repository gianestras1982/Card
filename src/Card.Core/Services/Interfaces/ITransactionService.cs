using System.Collections.Generic;
using System.Threading.Tasks;
using Card.Core.Model;
using Card.Core.Model.Types;
using Card.Core.Services.Options;

namespace Card.Core.Services.Interfaces
{
    public interface ITransactionService
    {
        public Task<Result<Transaction>> RegisterTransactionAsync(RegisterTransactionOptions options);

    }
}
