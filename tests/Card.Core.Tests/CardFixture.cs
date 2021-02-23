using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Card.Core.Services.Extensions;

namespace Card.Core.Tests
{
    public class CardFixture : IDisposable
    {
        public IServiceScope Scope { get; private set; }

        public CardFixture()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath($"{AppDomain.CurrentDomain.BaseDirectory}")
                .AddJsonFile("appsettings.json", false)
                .Build();

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddAppServices(config);

            Scope = serviceCollection.BuildServiceProvider().CreateScope();
        }
        public void Dispose()
        {
            Scope.Dispose();
        }
    }
}
