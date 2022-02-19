﻿using System.Text.Json.Serialization;

namespace ConversorMoeda.Entities
{
    public class InformacoesCotacao
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        
        [JsonPropertyName("codein")]
        public string CodeIn { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("high")]
        public string High { get; set; }

        [JsonPropertyName("low")]
        public string Low { get; set; }

        [JsonPropertyName("varBid")]
        public string VarBid { get; set; }

        [JsonPropertyName("pctChange")]
        public string PctChange { get; set; }

        [JsonPropertyName("bid")]
        public string Bid { get; set; }

        [JsonPropertyName("ask")]
        public string Ask { get; set; }

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        [JsonPropertyName("create_date")]
        public string CreateDate { get; set; }
    }
}
