using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeckHub.UserProfile.Messaging
{
    public interface IShowViewedQueueClient
    {
        bool IsConnected { get; }
        void RegisterMessageHandler(Func<ShowViewed, string, CancellationToken, Task> handler);
        Task CompleteAsync(string lockToken);
        Task CloseAsync();
    }
}