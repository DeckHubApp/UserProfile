using JetBrains.Annotations;
using MessagePack;

namespace Slidable.UserProfile.Messaging
{
    [MessagePackObject]
    [UsedImplicitly]
    public class ShowViewed
    {
        [Key(0)]
        public string ShowId { get; set; }
        
        [Key(1)]
        public string User { get; set; }

        public bool IsValid => NoNullOrWhitespace(ShowId, User);
        
        private static bool NoNullOrWhitespace(params string[] values)
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (int i = 0; i < values.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(values[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}