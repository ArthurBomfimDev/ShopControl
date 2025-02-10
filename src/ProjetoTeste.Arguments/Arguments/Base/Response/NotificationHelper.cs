using System.Collections.Concurrent;

namespace ProjetoTeste.Arguments.Arguments.Base.Response;

public static class NotificationHelper
{
    public static ConcurrentDictionary<string, DetailedNotification> ListNotification { get; private set; }

    public static void Add(string key, DetailedNotification detailedNotification)
    {
        ListNotification.TryAdd(key, detailedNotification);
    }
}
