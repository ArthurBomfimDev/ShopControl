using ProjetoTeste.Arguments.Arguments.Base.ApiResponse;
using System.Collections.Concurrent;

namespace ProjetoTeste.Domain.Helper;

public static class NotificationHelper
{
    public static ConcurrentDictionary<string, DetailedNotification> ListNotification { get; private set; }

    public static void Add(string key, DetailedNotification detailedNotification)
    {
        ListNotification.TryAdd(key, detailedNotification);
    }
}