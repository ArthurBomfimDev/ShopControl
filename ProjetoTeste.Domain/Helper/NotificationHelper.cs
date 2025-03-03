using ProjetoTeste.Arguments.Arguments.Base.ApiResponse;
using System.Collections.Concurrent;

namespace ProjetoTeste.Domain.Helper;

public static class NotificationHelper
{
    public static ConcurrentDictionary<string, List<DetailedNotification>> ListNotification { get; private set; }

    public static void CreateDict()
    {
        ListNotification = new();
    }

    public static void Add(string key, DetailedNotification detailedNotification)
    {
        if (ListNotification.ContainsKey(key))
        {
            var list = Get(key);
            var noitfication = list.FirstOrDefault();
            noitfication.ListMessage.Union(detailedNotification.ListMessage);
        }
        else
        {
            ListNotification.TryAdd(key, [detailedNotification]);
        }
    }

    public static List<DetailedNotification> Get(string key)
    {
        ListNotification.TryGetValue(key, out List<DetailedNotification>? listDetailedNotification);
        if (listDetailedNotification != null)
            return listDetailedNotification;

        return default;
    }

    public static List<DetailedNotification> Get()
    {
        return (from i in ListNotification
                from j in i.Value
                select j).ToList();
    }
}