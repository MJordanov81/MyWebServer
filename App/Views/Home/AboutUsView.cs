namespace App.Views.Home
{
    using MyWebServer.Server.Contracts;

    public class AboutUsView : IView
    {
        public string View()
        {
            return
            @"<style>
                footer { text-align: center; }
                pre { background-color: #F94F80; }
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
                <h2>About Us</h2>
                    <dl>
                        <dt>Company Name</dt>
                        <dd>By The Cake Ltd.</dd>
                        <dt>Owner</dt>
                        <dd>Marian Jordanov</dd>
                    </dl>
                
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
