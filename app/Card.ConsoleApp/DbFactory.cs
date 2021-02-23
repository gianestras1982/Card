using Card.Core.Config;
using Card.Core.Config.Extensions;
using Card.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Card.ConsoleApp
{
    public class DbFactory
    {
        public DbContextOptionsBuilder<CardDBContext> GetCon()
        {
            ConfigurationBuilder confBldr = new ConfigurationBuilder();
            IConfigurationBuilder _iconfBldr = confBldr.SetBasePath($"{AppDomain.CurrentDomain.BaseDirectory}");
            _iconfBldr = _iconfBldr.AddJsonFile("appsettings.json", false);
            IConfigurationRoot _iconfRoot = _iconfBldr.Build();

            AppConfig appConfg = _iconfRoot.ReadAppConfiguration();

            DbContextOptionsBuilder<CardDBContext> dbCntxtOptnsBldr = new DbContextOptionsBuilder<CardDBContext>();

            dbCntxtOptnsBldr.UseSqlServer(appConfg.ConnString,
                options => {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                });

            return dbCntxtOptnsBldr;
        }
    }
}
