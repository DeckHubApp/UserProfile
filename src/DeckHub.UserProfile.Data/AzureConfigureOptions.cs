using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using DeckHub.UserProfile.Data.Options;

namespace DeckHub.UserProfile.Data
{
    [UsedImplicitly]
    public class AzureConfigureOptions : IConfigureOptions<AzureOptions>
    {
        private readonly IConfiguration _configuration;

        public AzureConfigureOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(AzureOptions options)
        {
            _configuration.Bind("Azure", options);
        }
    }
}