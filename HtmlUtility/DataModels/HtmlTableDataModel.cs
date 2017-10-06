namespace HtmlUtility.DataModels
{
    using System.Collections.Generic;

    public class HtmlTableDataModel
    {
        private readonly List<string> headers;

        private readonly string[][] data;

        public HtmlTableDataModel(List<string> headers, string[][] data)
        {
            this.headers = headers;
            this.data = data;
        }

        public IList<string> Headers => this.headers;

        public string[][] Data => this.data;
    }
}
