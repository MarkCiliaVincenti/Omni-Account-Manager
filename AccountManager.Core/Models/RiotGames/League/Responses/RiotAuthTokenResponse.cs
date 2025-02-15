﻿using System.Text.Json.Serialization;

namespace AccountManager.Core.Models.RiotGames.League.Responses
{
    public sealed class RiotAuthTokenResponse
    {
        [JsonPropertyName("parameters")]
        public RiotAuthTokenParameters? Parameters { get; set; }
    }
}
