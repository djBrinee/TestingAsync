using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingAsync
{
    public class ConvertidorDeMoneda
    {
        /* DEPENDENCY INJECTION - Esta clase requiere un IBuscadorTasas en el constructor
         * lo que permite en los casos de pruebas pasarle el stub */
        IBuscadorTasas buscadorTasas;
        public ConvertidorDeMoneda(IBuscadorTasas buscadorTasas)
        {
            this.buscadorTasas = buscadorTasas;
        }

        // Esto no es necesario para su programa. Esto sólo es mostrando como recorrer la lista de tasas
        public void MostrarReporteDeTasas()
        {
            var tasas = buscadorTasas.ObtenerTasas();
            foreach (Tasa tasa in tasas.Result)
                Console.WriteLine($"{tasa.Entidad,-45}  {tasa.Valor,6}  {tasa.MonedaOrigen}->{tasa.MonedaDestino}");
        }

        public async Task ComprarDolaresEnElPopular()
        {
            Console.WriteLine("Cuántos dólares quieres comprar");
            float valorEnPesos = float.Parse(Console.ReadLine());
            Console.WriteLine("\n");
            var tasas = await buscadorTasas.ObtenerTasas();

            // Filtrando lista, según criterios Entidad, MonedaOrigen y Destino, y tomando el 1er resultado
            var tasaVentaDolaresPopular = tasas.Where(x => x.Entidad == "Banco Popular"
                                                     && x.MonedaOrigen == "DOP"
                                                     && x.MonedaDestino == "USD").First();

            /* Debido a que todas las tasas están en DOP, para convertir de Pesos a Dólares se debe dividir,
             mientras que para convertir de Dólares a Pesos se debe multiplicar. Es decir:
                   DOP->USD:    cantidadDolares = cantidadPesos   ÷ tasaPesos
                   USD->DOP:    cantidadPesos   = cantidadDolares × tasaPesos 
            */
            float valorFinal =  valorEnPesos / tasaVentaDolaresPopular.Valor;
            Console.WriteLine($"Este es el resultado {valorFinal}");
        }
    }
}
