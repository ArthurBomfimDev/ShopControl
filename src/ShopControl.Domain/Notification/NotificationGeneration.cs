namespace ShopControl.Domain.Notification;

public class NotificationGeneration : NotificationDictionary
{

    #region Error
    public bool InvalidNull(int index)
    {
        return AddErrorNotification(index.ToString(), NotificationMessage.InvalidNull(index));
    }

    public bool InvalidLenght(string identifier, string? value, int minLeght, int maxLenght, string propertyName)
    {
        return AddErrorNotification(identifier, NotificationMessage.InvalidLenght(propertyName, value, minLeght, maxLenght));
    }

    public bool NullField(string identifier, string propertyName)
    {
        return AddErrorNotification(identifier, NotificationMessage.NullField(propertyName));
    }

    public bool RepeatedIdentifier(string identifier, string fieldName)
    {
        return AddErrorNotification(identifier, NotificationMessage.RepeatedIdentifier(fieldName, identifier));
    }

    public bool AlreadyExists(string identifier, string fieldName)
    {
        return AddErrorNotification(identifier, NotificationMessage.AlreadyExists(fieldName, identifier));
    }

    public bool NotFoundId(string idetifier, string className, long id)
    {
        return AddErrorNotification(idetifier, NotificationMessage.NotFoundId(className, id));
    }

    public bool RepeatedId(string idetifier, long id)
    {
        return AddErrorNotification(idetifier, NotificationMessage.RepeatedId(id));
    }

    public bool LikedValue(string idetifier, string linkedValue, string className)
    {
        return AddErrorNotification(idetifier, NotificationMessage.LikedValue(className, linkedValue));
    }

    public bool InvalidCPF(string idetifier, string cpfValue)
    {
        return AddErrorNotification(idetifier, NotificationMessage.InvalidCPF(cpfValue));
    }

    public bool InvalidEmail(string idetifier, string emailValue)
    {
        return AddErrorNotification(idetifier, NotificationMessage.InvalidEmail(emailValue));
    }

    public bool InvalidPhone(string idetifier, string phoneValue)
    {
        return AddErrorNotification(idetifier, NotificationMessage.InvalidPhone(phoneValue));
    }

    public bool NegativeStock(string idetifier, long stock)
    {
        return AddErrorNotification(idetifier, NotificationMessage.NegativeStock(stock));
    }

    public bool NegativePrice(string idetifier, decimal price)
    {
        return AddErrorNotification(idetifier, NotificationMessage.NegativePrice(price));
    }

    public bool InvalidRelatedProperty(string idetifier, string fieldName, long relatedId)
    {
        return AddErrorNotification(idetifier, NotificationMessage.InvalidRelatedProperty(fieldName, relatedId));
    }

    public bool InvalidOrderValueLess(string idetifier, long value, long min)
    {
        return AddErrorNotification(idetifier, NotificationMessage.InvalidOrderValueLess(value, min));
    }

    public bool UnavailableStock(string identifier, long orderedValue, long stock)
    {
        return AddErrorNotification(identifier, NotificationMessage.UnavailableStock(orderedValue, stock));
    }

    #endregion

    #region Success
    public bool SuccessfullyRegistered(string idetifier, string className)
    {
        return AddSuccesNotification(idetifier, NotificationMessage.SuccessfullyRegistered(className, idetifier));
    }

    public bool SuccessfullyUpdated(string idetifier, long id, string className)
    {
        return AddSuccesNotification(idetifier, NotificationMessage.SuccessfullyUpdated(className, idetifier, id));
    }

    public bool SuccessfullyDeleted(string idetifier, string className)
    {
        return AddSuccesNotification(idetifier, NotificationMessage.SuccessfullyDeleted(className, idetifier));
    }
    #endregion

}
