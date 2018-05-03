using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Slidable.UserProfile.Data.Options;

namespace Slidable.UserProfile.Data
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