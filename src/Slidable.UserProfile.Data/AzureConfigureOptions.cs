using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Slidable.UserProfile.Data.Options;

namespace Slidable.UserProfile.Data
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