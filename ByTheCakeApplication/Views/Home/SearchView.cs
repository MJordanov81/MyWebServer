namespace ByTheCakeApplication.Views.Home
{
    using HtmlUtilities.DataModels;
    using HtmlUtilities.HtmlHelpers;
    using System;
    using WebServerApplication.Server.Contracts;

    public class SearchView : IView
    {
        private HtmlTableDataModel searchResult;

        public SearchView(HtmlTableDataModel data = null)
        {
            this.searchResult = data;
        }

        public string View()
        {
            string resultTable = "No items found!";

            if (this.searchResult != null)
            {
                resultTable = "Search result:" + Environment.NewLine + HtmlHelper.HtmlTable(this.searchResult);
            }

            return @"<style>
                footer { text-align: center }
                pre {background-color: #F94F80}
                table, th, td {border: 1px solid black;}
                .backButton { margin-top: 30; }
            </style>

            <head>
                <title>By The Cake</title>
                <meta charset=""utf-8"">
                <meta name=""description"" content =""Buy the cake in By The Cake"" >
                <meta name= ""keywords"" content = ""cake, buy"" >
                <meta name= ""author"" content = ""Marian Jordanov"" >
            </head> 
            <body>  
                <h2>Search Cake</h2>
                    <form method=""POST"" action=""/search"">
                        <input type=""text"" name=""query""/>

                        <input type=""submit"" value=""Search""/>
                    </form>" + 
                    $"{resultTable}" +

                    @"
                    <div><button class=""backButton""><a href=""/"">Back to Index</a></button></div>

                    <hr> 

                    <pre>
City: Hong Kong                 City: Salzburg
Address: ChoCoLad 18            Address: SchokoLeiden 73
Phone: +78952804429             Phone: +49241432990                                                     
                    </pre>

                    <footer>&reg; All Rights Reserved</footer> 
            </body>";
        }
    }
}
