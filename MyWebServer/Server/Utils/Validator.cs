namespace MyWebServer.Server.Utils
{
    using System;

    public static class Validator
    {
        public static void CheckIfNull(object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void CheckIfNullOrEmpty(object obj, string name)
        {
            if (obj as string == String.Empty || obj as string is null)
            {
                throw new ArgumentNullException(name, $"Object {name} cannot be null or empty!");
            }
        }
    }
}
