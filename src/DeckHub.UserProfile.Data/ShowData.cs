using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using DeckHub.UserProfile.Data.Options;
using DeckHub.UserProfile.Models;

namespace DeckHub.UserProfile.Data
{
    [UsedImplicitly]
    public class ShowData : IShowData
    {
        private static readonly Regex MultipleUnderscores = new Regex("__+", RegexOptions.Compiled);
        private static readonly Regex IllegalKeyCharacters = new Regex(@"[/\\#?]", RegexOptions.Compiled);
        private readonly CloudTable _table;

        public ShowData(IOptions<AzureOptions> options)
        {
            var connectionString = options.Value.TableStorageConnectionString;
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("No Azure Storage connection string configured.");
            }
            if (!CloudStorageAccount.TryParse(connectionString, out var account))
            {
                throw new InvalidOperationException("Invalid Azure Storage connection string.");
            }

            var client = account.CreateCloudTableClient();
            _table = client.GetTableReference("usershows");
        }

        public Task Initialize() => _table.CreateIfNotExistsAsync();

        public async Task AddViewedShowAsync(string user, string show)
        {
            var partitionKey = CleanKey(user);
            var rowKey = CleanKey(show);

            var get = TableOperation.Retrieve<ShowEntity>(partitionKey, rowKey);
            var result = await _table.ExecuteAsync(get);
            if (result.HttpStatusCode == 200)
            {
                return;
            }
            
            var entity = new ShowEntity
            {
                PartitionKey = partitionKey,
                RowKey = rowKey,
                ShowId = show,
                ViewedAt = DateTimeOffset.UtcNow
            };

            var insert = TableOperation.InsertOrReplace(entity);
            await _table.ExecuteAsync(insert);
        }

        public async Task<ICollection<Show>> GetViewedShowsAsync(string user)
        {
            var partitionKey = CleanKey(user);
            var query = new TableQuery<ShowEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));

            return (await _table.ExecuteQuerySegmentedAsync(query, default))
                .Select(e => new Show {ShowId = e.ShowId, ViewedAt = e.ViewedAt})
                .ToArray();
        }

        private static string CleanKey(string key)
        {
            key = MultipleUnderscores.Replace(key, "_");
            return IllegalKeyCharacters.Replace(key, "__");
        }
    }
}
