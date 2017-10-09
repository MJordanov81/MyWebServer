namespace MyWebServer.Server.Utils
{
    using System;
    using StaticData;

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
                throw new ArgumentNullException(String.Format(ExceptionConstants.ObjectCannotBeNullOrEmpty, name));
            }
        }
    }
}
