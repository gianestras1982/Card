using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Card.Core.Consts;
using Card.Core.Data;
using Card.Core.Model;
using Card.Core.Services.Interfaces;
using Card.Core.Services.Options;
using System.IO;
using Card.Core.Model.Types;

namespace Card.Core.Services
{
    public class CardsService : ICardsService
    {
        private readonly CardDBContext _dbContext;

        public CardsService(CardDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Result<Cards>> RegisterCardsAsync(RegisterCardOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.Description))
                return new Result<Cards>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"Prepei na doseis perigrafi!"
                };


            var cards = new Cards()
            {
                Description = options.Description
            };

            await _dbContext.AddAsync<Cards>(cards);
            await _dbContext.SaveChangesAsync();

            return new Result<Cards>()
            {
                Code = ResultCodes.Success,
                Data = cards
            };
        }

        public async Task<Result<Cards>> GetCardsByIdAsync(int cardsId)
        {
            var cards = await _dbContext.Set<Cards>()
                           .Where(c => c.CardsId == cardsId)
                           .Include(c => c.Transactions)
                           .SingleOrDefaultAsync();

            if (cards != null)
            {
                return new Result<Cards>()
                {
                    Code = ResultCodes.Success,
                    Data = cards
                };
            }
            else
            {
                return new Result<Cards>()
                {
                    Code = ResultCodes.NotFound,
                    Message = $"Den vrethike karta me kodiko {cardsId}"
                };
            }
        }

        public async Task<Result<Cards>> UpdateCardsBalanceAsync(int cardsId, decimal balance)
        {
            var cards = (await GetCardsByIdAsync(cardsId)).Data;

            if (cards != null)
            {
                cards.Balance = balance;

                _dbContext.Update<Cards>(cards);
                await _dbContext.SaveChangesAsync(); 

                return new Result<Cards>()
                {
                    Code = ResultCodes.Success,
                    Data = cards
                };
            }
            else
            {
                return new Result<Cards>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"Den iparxei karta me kodiko {cardsId}"
                };
            }

        }

    }

}