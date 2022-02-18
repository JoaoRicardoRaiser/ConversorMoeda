using ConversorMoeda.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace ConversorMoeda
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite o valor em R$");
            var valorEmRs = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite a moeda que deseja ser convertido: ");
            Console.WriteLine("1 - Dólar");
            Console.WriteLine("2 - Rublo Russo");
            var opcaoMoeda = Convert.ToInt32(Console.ReadLine()); //fazer um enum para esse cara.
            var client = new HttpClient();

            var cotacao = CalcularConversao(client, valorEmRs, opcaoMoeda);

        }

        private static void CalcularConversao(HttpClient client, int valorEmRs, int opcaoMoeda) 
        {
            if(opcaoMoeda == 1) 
            { 
                var cotacaoRealxRubloRusso = JsonSerializer.Deserialize<Cotacao>(client.GetStringAsync($"https://economia.awesomeapi.com.br/last/BRL-RUB").GetAwaiter().GetResult());
                var cotacaoRubloRussoxDolar = JsonSerializer.Deserialize<Cotacao>(client.GetStringAsync($"https://economia.awesomeapi.com.br/last/RUB-USD").GetAwaiter().GetResult());

                var realEmRubloRusso = valorEmRs * Convert.ToDouble(cotacaoRealxRubloRusso.high);


            }
            else
            {
                continue;
            }
        }
    }
}
