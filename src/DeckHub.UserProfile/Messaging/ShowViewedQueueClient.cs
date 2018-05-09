using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DeckHub.UserProfile.Messaging
{
    public class ShowViewedQueueClient : TypedQueueClient<ShowViewed>, IShowViewedQueueClient
    {
        public ShowViewedQueueClient(IOptions<MessagingOptions> options, ILogger<ShowViewedQueueClient> logger) : base("shows/viewed", options, logger)
        {
        }
    }
}