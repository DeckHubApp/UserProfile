using System.Collections.Generic;
using System.Threading.Tasks;
using DeckHub.UserProfile.Models;

namespace DeckHub.UserProfile.Data
{
    public interface IShowData
    {
        Task Initialize();
        Task AddViewedShowAsync(string user, string show);
        Task<ICollection<Show>> GetViewedShowsAsync(string user);
    }
}