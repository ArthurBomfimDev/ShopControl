namespace ShopControl.Arguments.Arguments;

public class BaseValidateDTO
{
    public bool Invalid { get; private set; }
    public bool Ignore { get; private set; }

    public bool SetInvalid()
    {
        return Invalid = true;
    }

    public bool SetIgnore()
    {
        return Ignore = true;
    }
}