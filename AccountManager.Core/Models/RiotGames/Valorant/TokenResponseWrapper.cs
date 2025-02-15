﻿using AccountManager.Core.Models.RiotGames.Valorant.Responses;
using System.Text.Json.Serialization;

namespace AccountManager.Core.Models.RiotGames.Valorant
{
    public sealed class TokenResponseWrapper
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("response")]
        public TokenResponseParams? Response { get; set; }
        [JsonPropertyName("multifactor")]
        public Multifactor? Multifactor { get; set; }
        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("securityProfile")]
        public string? SecurityProfile { get; set; }
    }
}