using ConversorMoeda.Services;

namespace ConversorMoeda
{
    class Program
    {
        static void Main(string[] args)
        {
            var cotacaoService = new CotacaoService();
            cotacaoService.Executar().Wait();
        }
    }
}
