namespace HtmlUtility.HtmlHelpers
{
    using System.Collections.Generic;
    using System.Text;
    using DataModels;

    public static class HtmlHelper
    {
        public static string HtmlTable(HtmlTableDataModel dataModel)
        {
            if (dataModel == null)
            {
                return "";
            }

            IList<string> headers = dataModel.Headers;
            string[][] data = dataModel.Data;

            StringBuilder table = new StringBuilder();

            table.AppendLine("<table>");
            table.AppendLine("<tr>");
            foreach (string header in headers)
            {
                table.AppendLine($"<th>{header}</th>");
            }
            table.AppendLine("</tr>");
            for (int i = 0; i < data.Length; i++)
            {
                table.AppendLine("<tr>");
                foreach (string item in data[i])
                {
                    table.AppendLine($"<td>{item}</td>");
                }
                table.AppendLine("</tr>");
            }

            table.AppendLine("</table>");

            return table.ToString().Trim();
        }
    }
}
