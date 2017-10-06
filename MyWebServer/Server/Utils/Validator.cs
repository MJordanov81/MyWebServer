namespace MyWebServer.Server.Utils
{
    using System;

    public static class Validator
    {
        public static void CheckIfNull(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
        }

        public static void CheckIfNullOrEmpty(object obj)
        {
            if (obj as string == String.Empty || obj as string is null)
            {
                throw new ArgumentNullException(nameof(obj), $"Object {nameof(obj)} cannot be null or empty!");
            }
        }
    }
}
