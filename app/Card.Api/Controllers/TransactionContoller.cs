using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Card.Core.Consts;
using Card.Core.Model;
using Card.Core.Services;
using Card.Core.Services.Options;
using Card.Core.Services.Interfaces;

namespace Card.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactions;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ILogger<TransactionController> logger, ITransactionService transaction)
        {
            _logger = logger;
            _transactions = transaction;
        }



        [HttpPost]
        public async Task<IActionResult> RegisterCards([FromBody] RegisterTransactionOptions options)
        {
            var cards = await _transactions.RegisterTransactionAsync(options);

            if (cards.Code != ResultCodes.Success)
            {
                return StatusCode(cards.Code, cards.Message);
            }

            return Json(cards);
        }
    }
}
