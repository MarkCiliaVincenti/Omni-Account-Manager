﻿using AccountManager.Core.Interfaces;
using AccountManager.Core.Models;
using AccountManager.Core.Models.RiotGames.League;
using AccountManager.Core.Models.RiotGames.League.Responses;
using AccountManager.Core.Models.RiotGames.TeamFightTactics.Responses;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;

namespace AccountManager.Infrastructure.CachedClients
{
    public sealed class CachedLeagueClient : ILeagueClient
    {
        private readonly IAppCache _memoryCache;
        private readonly ILeagueClient _leagueClient;
        public CachedLeagueClient(IAppCache memoryCache, ILeagueClient leagueClient)
        {
            _memoryCache = memoryCache;
            _leagueClient = leagueClient;
        }

        public async Task<Rank> GetSummonerRankByPuuidAsync(Account account)
        {
            var cacheKey = $"{nameof(GetSummonerRankByPuuidAsync)}.{account.Username}";

            return await _memoryCache.GetOrAddAsync(cacheKey,
                async (entry) =>
                {
                    var value = await _leagueClient.GetSummonerRankByPuuidAsync(account);
                    if (value is null)
                        entry.SetAbsoluteExpiration(DateTimeOffset.Now);

                    return value;
                }) ?? new();
        }

        public async Task<Rank> GetTFTRankByPuuidAsync(Account account)
        {
            var cacheKey = $"{account.Username}.{account.AccountType}.{nameof(GetTFTRankByPuuidAsync)}";

            return await _memoryCache.GetOrAddAsync(cacheKey,
                async (entry) =>
                {
                    var value = await _leagueClient.GetTFTRankByPuuidAsync(account);
                    if (value is null)
                        entry.SetAbsoluteExpiration(DateTimeOffset.Now);

                    return value;
                }) ?? new();
        }

        public async Task<List<LeagueQueueMapResponse>?> GetLeagueQueueMappings()
        {
            var cacheKey = nameof(GetLeagueQueueMappings);

            return await _memoryCache.GetOrAddAsync(cacheKey,
                async (entry) =>
                {
                    var value = await _leagueClient.GetLeagueQueueMappings();
                    if (value is null)
                        entry.SetAbsoluteExpiration(DateTimeOffset.Now);

                    return value;
                }) ?? new();
        }

        public async Task<UserChampSelectHistory?> GetUserChampSelectHistory(Account account)
        {
            var cacheKey = $"{account.Username}.{account.AccountType}.{nameof(GetUserChampSelectHistory)}";

            return await _memoryCache.GetOrAddAsync(cacheKey,
               async (entry) =>
               {
                   var value = await _leagueClient.GetUserChampSelectHistory(account);
                   if (value is null)
                       entry.SetAbsoluteExpiration(DateTimeOffset.Now);

                   return value;
               }) ?? new();
        }

        public async Task<MatchHistory?> GetUserLeagueMatchHistory(Account account)
        {
            var cacheKey = $"{account.Username}.{account.AccountType}.{nameof(GetUserLeagueMatchHistory)}";

            return await _memoryCache.GetOrAddAsync(cacheKey,
               async (entry) =>
               {
                   var value = await _leagueClient.GetUserLeagueMatchHistory(account);
                   if (value is null)
                       entry.SetAbsoluteExpiration(DateTimeOffset.Now);

                   return value;
               }) ?? new();
        }

        public async Task<TeamFightTacticsMatchHistory?> GetUserTeamFightTacticsMatchHistory(Account account)
        {
            var cacheKey = $"{account.Username}.{account.AccountType}.{nameof(GetUserTeamFightTacticsMatchHistory)}";

            return await _memoryCache.GetOrAddAsync(cacheKey,
               async (entry) =>
               {
                   var value = await _leagueClient.GetUserTeamFightTacticsMatchHistory(account);
                   if (value is null)
                       entry.SetAbsoluteExpiration(DateTimeOffset.Now);

                   return value;
               }) ?? new();
        }
    }
}
