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
    public class TransactionTests : IClassFixture<CardFixture>
    {
        private ITransactionService _transaction;

        public TransactionTests(CardFixture fixture)
        {
            _transaction = fixture.Scope.ServiceProvider.GetRequiredService<ITransactionService>();
        }


        [Fact]
        public async void Test_RegisterTransactionAsync()
        {
            var options = new RegisterTransactionOptions()
            {
                
                cardsId = 1,
                TransType = TransactionType.CardPresent,
                Amount = 300
            };

            var transaction = await _transaction.RegisterTransactionAsync(options);

            Assert.NotNull(transaction);
        }


    }


}
