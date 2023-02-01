using HtmlAgilityPack;

namespace HtmlCsvConvertor
{
    public class HtmlCsvConvertor
    {
        public static string HtmlToCsv(string html, string separator = ";")
        {
            var document = new HtmlAgilityPack.HtmlDocument();
            string CsvResult = string.Empty;
            document.LoadHtml(html);
            var tables = document.DocumentNode.SelectNodes("//table").ToList();
            if (tables.Count == 0)
            {
                return " ";
            }
            foreach (var table in tables)
            {
                if (table.ChildNodes.Any((e) => e.OriginalName == "tbody"))
                {
                    foreach (var node in table.ChildNodes.Where(e => e.OriginalName == "tbody"))
                    {
                        foreach (var tr in node.ChildNodes.Where(e => e.OriginalName == "tr"))
                        {
                            CsvResult += "\n";
                            foreach (var td in tr.ChildNodes.Where(e => e.OriginalName == "td"))
                            {
                                CsvResult += $"{td.InnerText}{separator}";
                            }
                            CsvResult = CsvResult.Substring(0, CsvResult.Length - 1);
                        }
                    }

                }
                else
                {
                    foreach (var tr in table.ChildNodes.Where(e => e.OriginalName == "tr"))
                    {
                        CsvResult += "\n";
                        foreach (var td in tr.ChildNodes.Where(e => e.OriginalName == "td"))
                        {

                            CsvResult += $"{td.InnerText}{separator}";
                        }
                        CsvResult = CsvResult.Substring(0, CsvResult.Length - 1);
                    }
                }

            }
            CsvResult = CsvResult.Replace("&nbsp;", " ");
            return CsvResult;
        }
        public static string CsvToHtml(string csv, string separator)
        {
            string html = "<table>";
            for (int i = 0; i < csv.Split("\n").Length; i++)
            {
                html += "<tr>";
                for (int j = 0; j < csv.Split("\n")[i].Split(separator).Length; j++)
                {
                    html += $"<td>{csv.Split("\n")[i].Split(separator)[j]}</td>";
                }
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }
    }
}