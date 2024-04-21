using Microsoft.Extensions.Options;
using Persistence.Authentication;

namespace WebApp.OptionsSetup
{
    public class ResponseMessagesSetup : IConfigureOptions<SystemMessages>
    {
        private readonly IConfiguration _configuration;
        private const string SectionName = "Messages";

        public ResponseMessagesSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(SystemMessages options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
