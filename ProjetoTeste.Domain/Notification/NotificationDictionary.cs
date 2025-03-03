using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Base.ApiResponse;
using System.Collections.Concurrent;

namespace ProjetoTeste.Domain.Notification;

public class NotificationDictionary
{

    public static ConcurrentDictionary<string, List<DetailedNotification>>? ListNotification { get; set; }

    public void CreateDictionary()
    {
        ListNotification = new();
    }

    public void Add(string key, DetailedNotification detailedNotification)
    {
        if (ListNotification.ContainsKey(key))
        {
            var list = Get(key);
            var noitfication = list.FirstOrDefault();
            noitfication.ListMessage.AddRange(detailedNotification.ListMessage);
        }
        else
        {
            ListNotification.TryAdd(key, [detailedNotification]);
        }
    }

    public List<DetailedNotification> Get(string key)
    {
        ListNotification.TryGetValue(key, out List<DetailedNotification>? list);

        return list != null ? list : new List<DetailedNotification>();
    }

    public List<DetailedNotification>? GetAllNotification()
    {
        return ListNotification != null ? (from i in ListNotification.Values
                                           from j in i
                                           select j).ToList() : null;
    }
    public bool AddSuccesNotification(string keyIdentificator, string message)
    {
        Add(keyIdentificator, new DetailedNotification(keyIdentificator, [message], EnumNotificationType.Success));
        return true;
    }

    public bool AddErrorNotification(string keyIdentificator, string message)
    {
        Add(keyIdentificator, new DetailedNotification(keyIdentificator, [message], EnumNotificationType.Error));
        return true;
    }

    //public (List<DetailedNotification>? listSuccess, List<DetailedNotification>? listErrors) GetResult()
    //{
    //    var listNotification = GetAllNotification();
    //    if (listNotification == null)
    //        return (null, null);
    //    var listSucess = listNotification!.Where(i => i.NotificationType == EnumNotificationType.Success).ToList();
    //    var listError = listNotification!.Where(i => i.NotificationType == EnumNotificationType.Error).ToList();

    //    return (listSucess, listError);
    //}
}