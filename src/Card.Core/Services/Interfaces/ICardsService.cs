using System.Collections.Generic;
using System.Threading.Tasks;

using Card.Core.Model;
using Card.Core.Model.Types;
using Card.Core.Services.Options;

namespace Card.Core.Services.Interfaces
{
    public interface ICardsService
    {
        public Task<Result<Cards>> RegisterCardsAsync(RegisterCardOptions options);
        public Task<Result<Cards>> GetCardsByIdAsync(int cardsId);
        public Task<Result<Cards>> UpdateCardsBalanceAsync(int cardsId, decimal balance);
    }
}