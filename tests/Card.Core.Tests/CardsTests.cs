using Card.Core.Config;
using Card.Core.Config.Extensions;
using Card.Core.Data;
using Card.Core.Model.Types;
using Card.Core.Services;
using Card.Core.Services.Interfaces;
using Card.Core.Services.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Xunit;

namespace Card.Core.Tests
{
    public class CardsTests : IClassFixture<CardFixture>
    {
        private ICardsService _cards;

        public CardsTests(CardFixture fixture)
        {
            _cards = fixture.Scope.ServiceProvider.GetRequiredService<ICardsService>();
        }


        [Fact]
        public async void Test_RegisterCardsAsync()
        {
            var options = new RegisterCardOptions()
            {
                Description = "my family card"
            };

            var cards = await _cards.RegisterCardsAsync(options);

            Assert.NotNull(cards);
        }

        [Fact]
        public async void Test_GetCardsByIdAsync()
        {
            var cards = await _cards.GetCardsByIdAsync(4);

            Assert.NotNull(cards);
        }

        [Fact]
        public async void Test_UpdateCardsBalanceAsync()
        {
            var cards = await _cards.UpdateCardsBalanceAsync(1, 8000);

            Assert.NotNull(cards);
        }


    }

}