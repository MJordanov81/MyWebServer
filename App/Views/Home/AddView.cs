namespace App.Views.Home
{
    using MyWebServer.Server.Contracts;

    public class AddView : IView
    {
        public string View()
        {
            return
                @"<style>
                footer { text-align: center }
                pre {background-color: #F94F80}
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
                <h2>Add New Cake</h2>
                    <form method=""POST"" action=""/add"">
                        <label for=""name"">Name: </>
                        <input id =""name"" type=""text"" name=""name""/>

                        <label for=""price"">Price: </>
                        <input id =""price"" type=""number"" step=""0.01"" min=""0.00"" name=""price""/>

                        <input type=""submit"" value=""Submit""/>
                    </form>
                
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
