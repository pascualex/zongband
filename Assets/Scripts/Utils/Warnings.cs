using RLEngine.Logs;
using RLEngine.Entities;

using System;

namespace Zongband.Utils
{
    public static class Warnings
    {
        public static string LogNotSupported(Log log)
        {
            var type = log.GetType();
            return $"The log type \"{type}\" is not supported.";
        }

        public static string EntityAlreadyPresent(Entity entity)
        {
            var name = entity.Name;
            return $"The entity \"{name}\" could not be added because it was already present.";
        }

        public static string EntityNotPresent(Entity entity)
        {
            var name = entity.Name;
            return $"The entity \"{name}\" was not previously added or was removed.";
        }

        public static string VisualsType(object? obj, Type expectedType)
        {
            var actual = obj?.GetType().ToString() ?? "null";
            return $"Expected visuals of type {expectedType} but received {actual}.";
        }
    }
}
