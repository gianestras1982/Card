using Microsoft.Extensions.Configuration;
using Card.Core.Config;

namespace Card.Core.Config.Extensions
{
    public static class ConfigurationExtentions
    {
        public static AppConfig ReadAppConfiguration(this IConfiguration @this)
        {
            var connString = @this.GetSection("ConnectionStrings").GetSection("CardDatabase").Value;

            return new AppConfig()
            {
                ConnString = connString
            };
        }
    }
}
