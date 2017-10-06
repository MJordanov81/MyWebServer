namespace ByTheCakeApplication.Views.Home
{
    using Resources.images;
    using WebServerApplication.Server.Contracts;

    public class IndexView : IView
    {
        public string View()
        {
            return
            @"<style>
                footer { text-align: center }
                pre {background-color: #F94F80}
                span.important {font-style: italic; font-weight: bold}
            </style>

            <head>
                <title>By The Cake</title>
                <meta charset=""utf-8"">
                <meta name=""description"" content =""Buy the cake in By The Cake"" >
                <meta name= ""keywords"" content = ""cake, buy"" >
                <meta name= ""author"" content = ""Marian Jordanov"" >
            </head> 
            <body>  
                <h1>By The Cake</h1>  
                <h2>Enjoy our awesome cakes</h2>                
                <hr> 
                    <ul> 
                        <li><a href=""/"">Home</a></li> 
                            <ol> 
                                <li><a href=""#cakes"">Our Cakes</a></li> 
                                <li><a href=""#cakes"">Our Stores</a></li> 
                            </ol> 
                        <li><a href=""/add"">Add Cake</a></li> 
                        <li><a href=""/search"">Browse Cakes</a></li> 
                        <li><a href=""/aboutus"">About Us</a></li> 
                    </ul>  
                        
                <h2>Home</h2> 
                    <section>  
                        <h3 id=""cakes"">Our Cakes</h3> 
                            <p> Cake is a form of <span class=""important"">sweet dessert</span> that is typically baked. In its oldest forms, <span class=""important"">cakes</span> were modifications of breads, but <span class=""important"">cakes</span> now cover a wide range of preparations that can be simple or elaborate, and that share features with other desserts such as pastries, meringues, custards, and pies.</p> " +
                            $"<img src=\"{Images.CakeImageOne}\" alt=\"cake1\"/>" +
                    @"</section> 
                        <section> 
                        <h3 id=""stores"">Our Stores</h3> 
                            <p> Our stores are located in 21 cities all over the world. Come and see what we have for you.</p>" +
                            $"<img src=\"{Images.CakeImageTwo}\" alt=\"cake2\"/>" +
                   @" </section> 
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
