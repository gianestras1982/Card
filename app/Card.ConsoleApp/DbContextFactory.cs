using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using Card.Core.Data;
using Card.Core.Config.Extensions;
using System.Reflection;

namespace Card.ConsoleApp
{
    public class DbContextFactory : IDesignTimeDbContextFactory<CardDBContext>
    {
        public CardDBContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath($"{AppDomain.CurrentDomain.BaseDirectory}")
                .AddJsonFile("appsettings.json", false)
                .Build();

            var config = configuration.ReadAppConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<CardDBContext>();

            optionsBuilder.UseSqlServer(
                config.ConnString,
                options => {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                });

            return new CardDBContext(optionsBuilder.Options);
        }
    }
}
