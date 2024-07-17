﻿using Newtonsoft.Json;
using System.Security.Claims;

namespace ViajesFast.Extensions
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static string GetUserId(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
                return null;

            var currentUser = user;
            return currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}