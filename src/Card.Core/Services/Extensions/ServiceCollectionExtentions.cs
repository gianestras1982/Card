using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Card.Core.Config;
using Card.Core.Config.Extensions;
using Card.Core.Data;
using Card.Core.Services.Interfaces;

namespace Card.Core.Services.Extensions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddAppServices(this IServiceCollection @this, IConfiguration config)
        {
            @this.AddSingleton<AppConfig>(config.ReadAppConfiguration());

            @this.AddDbContext<CardDBContext>(
                (serviceProvider, optionsBuilder) =>
                {
                    var appConfig = serviceProvider.GetRequiredService<AppConfig>();

                    optionsBuilder.UseSqlServer(appConfig.ConnString);
                });


            @this.AddScoped<ICardsService, CardsService>();
            @this.AddScoped<ITransactionService, TransactionService>();


        }
    }
}
