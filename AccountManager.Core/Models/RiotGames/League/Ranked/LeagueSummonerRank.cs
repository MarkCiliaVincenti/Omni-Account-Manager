﻿using System.Text.Json.Serialization;

namespace AccountManager.Core.Models.RiotGames.League
{
    public sealed class LeagueSummonerRank
    {
        [JsonPropertyName("queueMap")]
        public QueueMap? QueueMap { get; set; }
    }
}
