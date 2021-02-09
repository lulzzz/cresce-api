using System;
using Microsoft.Extensions.Configuration;

namespace Cresce.Core
{
    public class Settings
    {
        private readonly IConfiguration? _configuration;

        public Settings(IConfiguration? configuration) => _configuration = configuration;

        public string ConnectionString
        {
            get
            {
                var connectionString = _configuration.GetConnectionString("default");
                return connectionString == "memory" ? EnvConnectionString : connectionString;
            }
        }

        public string Version => _configuration != null ? _configuration["Version"] : string.Empty;

        public string AppKey => _configuration != null ? _configuration["AppKey"] : "fedaf7d8863b48e197b9287d492b708e";
        public string EnvConnectionString => Environment.GetEnvironmentVariable("CRESCE_CONNECTION_STRING") ?? string.Empty;
    }
}
