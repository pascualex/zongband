using RLEngine.Core.Logs;
using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

namespace Zongband.Utils
{
    public static class Warnings
    {
        public static string CombinedActionRunning =>
            "The combined action was already running when an action was added";

        public static string EntityAlreadyPresent(IEntity entity)
        {
            var name = entity.Name;
            return $"The entity \"{name}\" could not be added because it was already present.";
        }

        public static string EntityNotPresent(IEntity entity)
        {
            var name = entity.Name;
            return $"The entity \"{name}\" was not previously added or was removed.";
        }

        public static string NotDefaultAsset(string path)
        {
            return $"There is no default asset at {path}";
        }

        public static string AssetNotAvailable(IIdentifiable identifiable)
        {
            return $"There is no asset available for {identifiable.ID}";
        }

        public static string LogNotSupported(ILog log)
        {
            var type = log.GetType();
            return $"The log type \"{type}\" is not supported.";
        }
    }
}
