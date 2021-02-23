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
    public class CardsController : Controller
    {
        private readonly ICardsService _cards;
        private readonly ILogger<CardsController> _logger;

        public CardsController(ILogger<CardsController> logger, ICardsService cards)
        {
            _logger = logger;
            _cards = cards;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCardsById(int id)
        {
            var cards = (await _cards.GetCardsByIdAsync(id)).Data;

            return Json(cards);
        }



        [HttpPost]
        public async Task<IActionResult> RegisterCards([FromBody] RegisterCardOptions options)
        {
            var cards = await _cards.RegisterCardsAsync(options);

            if (cards.Code != ResultCodes.Success)
            {
                return StatusCode(cards.Code, cards.Message);
            }

            return Json(cards);
        }
    }
}
