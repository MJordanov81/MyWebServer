namespace HtmlUtility.HtmlHelpers
{
    using System.Collections.Generic;
    using System.Linq;
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

        public static string RadioButtons(List<string> labels, List<string> values, string name, bool areInline, string id = "", bool required = false)
        {
            StringBuilder buttons = new StringBuilder();
            string isRequired = required ? "required" : "";
            string ending = areInline ? "" : "<br>";

            var labelsAndValues = labels.Zip(values, (l, w) => new {label = l, value = w}).ToList();

            foreach (var item in labelsAndValues)
            {
                buttons.AppendLine($"<input id=\"{id}\" type=\"radio\" name=\"{name}\" value=\"{item.value}\" {isRequired}>{item.label}{ending}");
            }

            return buttons.ToString().Trim();
        }

        public static string SelectInput(List<string> labels, List<string> options, string name, string id = "", string placeHolder = "", bool required = false)
        {
            StringBuilder result = new StringBuilder();
            string isRequired = required ? "required" : "";

            result.AppendLine($"<select name=\"{name}\" {isRequired}>");

            var labelsAndOptions = labels.Zip(options, (l, o) => new { label = l, option = o }).ToList();

            if (placeHolder != "")
            {
                result.AppendLine($"<option id=\"{id}\" value=\"\" disabled selected>{placeHolder}</option>");
            }

            foreach (var item in labelsAndOptions)
            {
                result.AppendLine($"<option id=\"{id}\" value=\"{item.option}\">{item.label}</option>");
            }

            result.AppendLine("</select>");

            return result.ToString().Trim();
        }

        public static string Checkbox(List<string> labels, List<string> values, string name, bool areInline, string id = "")
        {
            StringBuilder result = new StringBuilder();
            string ending = areInline ? "" : "<br>";

            var labelsAndValues = labels.Zip(values, (l, w) => new { label = l, value = w }).ToList();

            foreach (var item in labelsAndValues)
            {
                result.AppendLine($"<input id=\"{id}\" type=\"checkbox\" name=\"{name}\" value=\"{item.value}\">{item.label}{ending}");
            }

            return result.ToString().Trim();
        }
    }
}
