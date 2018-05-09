using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DeckHub.UserProfile.Data;

namespace DeckHub.UserProfile.Messaging
{
    public class ShowViewedProcessor : IHostedService
    {
        private readonly IShowData _showData;
        private readonly IShowViewedQueueClient _queueClient;
        private readonly ILogger<ShowViewedProcessor> _logger;

        public ShowViewedProcessor(IShowData showData, IShowViewedQueueClient queueClient, ILogger<ShowViewedProcessor> logger)
        {
            _showData = showData;
            _queueClient = queueClient;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (_queueClient.IsConnected)
            {
                _queueClient.RegisterMessageHandler(Handler);
            }

            return Task.CompletedTask;
        }

        private async Task Handler(ShowViewed show, string lockToken, CancellationToken ct)
        {
            if (!show.IsValid)
            {
                _logger.LogError("Invalid ShowViewed message received.");
                await _queueClient.CompleteAsync(lockToken).ConfigureAwait(false);
                return;
            }


            try
            {
                await _showData.AddViewedShowAsync(show.User, show.ShowId).ConfigureAwait(false);
                await _queueClient.CompleteAsync(lockToken).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error adding Viewed Show.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _queueClient.CloseAsync();
        }
        
        
    }
}