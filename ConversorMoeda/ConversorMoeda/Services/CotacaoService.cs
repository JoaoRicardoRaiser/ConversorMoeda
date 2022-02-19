using ConversorMoeda.Entities;
using ConversorMoeda.Enum;
using System;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConversorMoeda.Services
{
    public class CotacaoService
    {
        private readonly HttpClient _httpClient;
        private readonly CultureInfo _cultureInfoEnUs;
        private readonly CultureInfo _cultureInfoPtBr;
        private readonly CultureInfo _cultureInfoRuRu;

        public CotacaoService()
        {
            _httpClient = new HttpClient();
            _cultureInfoEnUs = new CultureInfo("en-US");
            _cultureInfoPtBr = new CultureInfo("pt-BR");
            _cultureInfoRuRu = new CultureInfo("ru-RU");
        }

        public async Task Executar()
        {
            Console.WriteLine("Digite o valor em R$");
            var valorEmRs = Convert.ToDecimal(Console.ReadLine(), _cultureInfoPtBr);

            Console.WriteLine("");
            Console.WriteLine("Digite a moeda que deseja ser convertido: ");
            Console.WriteLine("1 - Dólar");
            Console.WriteLine("2 - Rublo Russo");
            Console.WriteLine("");
            
            var opcaoMoeda = (TipoMoeda)Convert.ToInt32(Console.ReadLine());

            switch (opcaoMoeda)
            {
                case TipoMoeda.BRL_USD:
                    await ConverterDeBrlParaUsd(valorEmRs);
                    break;

                case TipoMoeda.BRL_RUB:
                    await ConverterDeBrlParaRub(valorEmRs);
                    break;


                default:
                    Console.WriteLine("Opção inválida bobão");
                    break;
            }
        }

        private async Task ConverterDeBrlParaUsd(decimal valorEmRs)
        {
            var cotacao = await ObterCotacao(TipoMoeda.BRL_USD) as CotacaoBrlUsd;

            var valorEmUsdFormatado = (Convert.ToDecimal(valorEmRs) * Convert.ToDecimal(cotacao.BRLUSD.High, _cultureInfoEnUs)).ToString("f2", _cultureInfoEnUs);

            var valorEmRsFormatado = valorEmRs.ToString("f2", _cultureInfoPtBr);

            Console.WriteLine($"BRL: {valorEmRsFormatado} = USD: {valorEmUsdFormatado}");
        }

        private async Task ConverterDeBrlParaRub(decimal valorEmRs)
        {
            var cotacao = await ObterCotacao(TipoMoeda.BRL_RUB) as CotacaoBrlRub;

            var valorEmRubFormatado = (Convert.ToDecimal(valorEmRs) * Convert.ToDecimal(cotacao.BRLRUB.High, _cultureInfoEnUs)).ToString("f2", _cultureInfoRuRu);

            var valorEmRsFormatado = valorEmRs.ToString("f2", _cultureInfoPtBr);

            Console.WriteLine($"BRL: {valorEmRsFormatado} = RUB: {valorEmRubFormatado}");
        }


        public async Task<CotacaoBase> ObterCotacao(TipoMoeda tipoMoeda)
        {
            var tipoMoedaConvertida = tipoMoeda.ToString().Replace("_", "-");
            var json = await _httpClient.GetStringAsync($"https://economia.awesomeapi.com.br/last/{tipoMoedaConvertida}");

            var cotacao = new CotacaoBase();

            switch (tipoMoeda)
            {
                case TipoMoeda.BRL_USD:
                    return JsonSerializer.Deserialize<CotacaoBrlUsd>(json);

                case TipoMoeda.BRL_RUB:
                    return JsonSerializer.Deserialize<CotacaoBrlRub>(json);
                
                default:
                    break;
            }

            return cotacao;
        }
    }
}
