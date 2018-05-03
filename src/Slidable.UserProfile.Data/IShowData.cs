using System.Collections.Generic;
using System.Threading.Tasks;
using Slidable.UserProfile.Models;

namespace Slidable.UserProfile.Data
{
    public interface IShowData
    {
        Task Initialize();
        Task AddViewedShowAsync(string user, string show);
        Task<ICollection<Show>> GetViewedShowsAsync(string user);
    }
}