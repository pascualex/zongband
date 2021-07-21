using System;

namespace Zongband.View.Exceptions
{
    public class VisualsException : Exception
    {
        public VisualsException()
        { }

        public VisualsException(string message)
        : base(message) { }

        public VisualsException(string message, Exception innerException)
        : base(message, innerException) { }

        public static VisualsException FromObject(object? obj, Type expectedType)
        {
            var actual = obj?.GetType().ToString() ?? "null";
            var message = $"Expected visuals of type {expectedType} but received {actual}";
            return new VisualsException(message);
        }
    }
}
