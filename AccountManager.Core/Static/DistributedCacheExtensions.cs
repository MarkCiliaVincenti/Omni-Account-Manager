﻿using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace AccountManager.Core.Static
{
    public static class DistributedCacheExtensions
    {
        public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value)
        {
            var json = JsonSerializer.Serialize(value);
            await cache.SetStringAsync(key, json);
        }

        public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, TimeSpan expireTimeSpan)
        {
            var json = JsonSerializer.Serialize(value);
            await cache.SetStringAsync(key, json, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.Add(expireTimeSpan),
            });
        }

        public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, DateTimeOffset absoluteExpiry)
        {
            var json = JsonSerializer.Serialize(value);
            await cache.SetStringAsync(key, json, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = absoluteExpiry,
            });
        }

        public static async Task<T?> GetAsync<T>(this IDistributedCache cache, string key)
        {
            var value = await cache.GetStringAsync(key);

            if (value is null)
                return default;

            try
            {
                var deserializedValue = JsonSerializer.Deserialize<T>(value);
                return deserializedValue;
            }
            catch
            {
                return default;
            }
        }

        public static async Task<T?> GetOrCreateAsync<T>(this IDistributedCache cache, string key, Func<Task<T>> factory)
        {
            var value = await cache.GetAsync<T>(key);
            if (value is not null)
                return value;

            value = await factory();

            if (value is null)
                return value;

            await cache.SetAsync(key, value);

            return value;
        }

        public static async Task<T?> GetOrCreateAsync<T>(this IDistributedCache cache, string key, Func<Task<T>> factory, TimeSpan expireTimeSpan)
        {
            var value = await cache.GetAsync<T>(key);
            if (value is not null)
                return value;

            value = await factory();

            if (value is null)
                return value;

            await cache.SetAsync(key, value, expireTimeSpan);

            return value;
        }

        public static async Task<T?> GetOrCreateAsync<T>(this IDistributedCache cache, string key, Func<Task<T>> factory, DateTimeOffset expireDateTime)
        {
            var value = await cache.GetAsync<T>(key);
            if (value is not null)
                return value;

            value = await factory();

            if (value is null)
                return value;

            await cache.SetAsync(key, value, expireDateTime);

            return value;
        }
    }
}