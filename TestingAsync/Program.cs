using HtmlAgilityPack;
using ScrapySharp.Extensions;
using TestingAsync;


internal class Program
{
    static void Main(string[] args)
    {
 

        IBuscadorTasas buscadorTasas = new BuscadorTasas(); // En el caso de prueba este será el stub
        ConvertidorDeMoneda convertidor = new ConvertidorDeMoneda(buscadorTasas); // <-- Dependency Injection

        // Esto no es necesario, es sólo para mostrar la lista de tasas:

        //convertidor.ComprarDolaresEnElPopular();
        //Console.WriteLine("Esperando...");

        ObtenerTasa();

        
    }
    public static string ObtenerTasa()
    {
        HtmlWeb oWeb = new HtmlWeb();
        HtmlDocument oDoc = oWeb.Load("https://www.infodolar.com.do/precio-dolar-entidad-banco-popular.aspx/");

        HtmlNode Body = oDoc.DocumentNode.CssSelect("colCompraVenta").First();
        string oBody = Body.InnerHtml;
        return oBody;
    }


}