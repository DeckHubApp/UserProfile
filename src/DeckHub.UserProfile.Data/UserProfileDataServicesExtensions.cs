using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using DeckHub.UserProfile.Data.Options;

namespace DeckHub.UserProfile.Data
{
    public static class UserProfileDataServicesExtensions
    {
        public static IServiceCollection AddTableStorageData(this IServiceCollection services)
        {
            services.AddSingleton<IConfigureOptions<AzureOptions>, AzureConfigureOptions>();
            services.AddSingleton<IShowData, ShowData>();
            return services;
        }
    }
}