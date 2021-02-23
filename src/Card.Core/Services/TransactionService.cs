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
    public class TransactionService : ITransactionService
    {
        private readonly CardDBContext _dbContext;
        private ICardsService _cards;

        public TransactionService(CardDBContext dbContext, ICardsService cards)
        {
            _dbContext = dbContext;
            _cards = cards;
        }



        public async Task<Result<Transaction>> RegisterTransactionAsync(RegisterTransactionOptions options)
        {
            if (options.Amount <= 0)
            {
                return new Result<Transaction>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"Prepei na doseis poso megalirerro tou midenos."
                };
            }


            var cards = (await _cards.GetCardsByIdAsync(options.cardsId)).Data;//fernoume tin karta kai ta transaction(limits) ola (incluce)


            if (cards != null)//an i karta iparxei
            {
                DateTime d = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                string transDateNow = d.ToString();


                //pernoume ta simerina transactions (limits) kai eCommerce kai cardPresent, ola (to poli mexri dio na mas ferei)
                var transToday = cards.Transactions
                               .Where(t => t.Created == transDateNow)
                               .ToList();


                if (cards.Balance > options.Amount)
                {

                    if (options.TransType == TransactionType.CardPresent)
                    {
                        var CardPresentToday = transToday.Where(t => t.Type == TransactionType.CardPresent)//fere tin simerini(transToday vl pio pano) eggrafi  me ton tipo cardPresent
                                                         .SingleOrDefault();

                        if (CardPresentToday == null)
                        {
                            var t = new Transaction()
                            {
                                Type = options.TransType,
                                Amount = options.Amount + CardPresentToday.Amount

                            };
                            cards.Transactions.Add(t);
                            cards.Balance = cards.Balance - options.Amount;
                            _dbContext.Update(t);
                            await _dbContext.SaveChangesAsync();
                            return new Result<Transaction>()
                            {
                                Code = ResultCodes.Success,
                                Data = t
                            };
                        }
                        else//an idi simera exei ginei mia cardPresent
                        {
                            if (CardPresentToday.Amount + options.Amount <= 1500)
                            {
                                var t = new Transaction()
                                {
                                    Type = options.TransType,
                                    Amount = CardPresentToday.Amount + options.Amount
                                };
                                CardPresentToday = t;
                                cards.Balance = cards.Balance - options.Amount;
                                await _dbContext.SaveChangesAsync();
                                return new Result<Transaction>()
                                {
                                    Code = ResultCodes.Success,
                                    Data = t
                                };
                            }
                            else
                            {
                                return new Result<Transaction>()
                                {
                                    Code = ResultCodes.BadRequest,
                                    Message = $"Me tin agora afti ksepernas to orio tou cardPresent pou einai 1500"
                                };
                            }
                        }



                    }
                    else if (options.TransType == TransactionType.CardPresent)
                    {
                        var ECommerceToday = transToday.Where(t => t.Type == TransactionType.ECommerce)
                                                  .SingleOrDefault();

                        if (ECommerceToday == null)
                        {
                            var t = new Transaction()
                            {
                                Type = options.TransType,
                                Amount = options.Amount + ECommerceToday.Amount
                            };
                            cards.Transactions.Add(t);
                            cards.Balance = cards.Balance - options.Amount;
                            _dbContext.Update(t);
                            await _dbContext.SaveChangesAsync();
                            return new Result<Transaction>()
                            {
                                Code = ResultCodes.Success,
                                Data = t
                            };
                        }
                        else
                        {
                            if (ECommerceToday.Amount + options.Amount <= 500)
                            {
                                var t = new Transaction()
                                {
                                    Type = options.TransType,
                                    Amount = ECommerceToday.Amount + options.Amount
                                };
                                ECommerceToday = t;
                                cards.Balance = cards.Balance - options.Amount;
                                await _dbContext.SaveChangesAsync();
                                return new Result<Transaction>()
                                {
                                    Code = ResultCodes.Success,
                                    Data = t
                                };
                            }
                            else
                            {
                                return new Result<Transaction>()
                                {
                                    Code = ResultCodes.BadRequest,
                                    Message = $"Me tin agora afti ksepernas to orio tou cardPresent pou einai 1500"
                                };
                            }
                        }
                    }
                    else
                    {
                        return new Result<Transaction>()
                        {
                            Code = ResultCodes.BadRequest,
                            Message = $"Kati den epelekses kala me to tipo tis kinisis"
                        };
                    }
                }
                else
                {
                    return new Result<Transaction>()
                    {
                        Code = ResultCodes.BadRequest,
                        Message = $"To poso agoras {options.Amount} ksepernaei to poso tou ipoloipou tis kartas {cards.Balance}"
                    };
                }
            }
            else
            {
                return new Result<Transaction>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"Den iparxei i karta me kodiko {options.cardsId}"
                };
            }

        }


    }


}