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
        Task.Run(async() =>
        {
            await convertidor.ConvertirPesosADolares();
        }).GetAwaiter().GetResult();
        
    }
}