namespace MyWebServer.Server.Enums
{
    using System.Collections.Generic;

    public enum MimeTypeCode
    {
        TextHtml = 0
    }

    public static class MimeType
    {
        public static IDictionary<MimeTypeCode, string> Types = new Dictionary<MimeTypeCode, string>()
        {
            {MimeTypeCode.TextHtml, "text/Html" }
        };
    }
}
