namespace MyWebServer.Server.Enums
{
    using System.Collections.Generic;

    public enum HeaderTypeCode
    {
        ContentType = 0,
        Location = 1,
        SetCookie = 2,
        Host = 3,
        Cookie = 4
    }

    public static class HeaderType
    {
        public static IDictionary<HeaderTypeCode, string> Types = new Dictionary<HeaderTypeCode, string>
        {
            {HeaderTypeCode.ContentType, "Content-Type"},
            {HeaderTypeCode.Location, "Location"},
            {HeaderTypeCode.SetCookie, "Set-cookie"},
            {HeaderTypeCode.Host, "Host"},
            {HeaderTypeCode.Cookie, "Cookie"}
        };
    }
}
