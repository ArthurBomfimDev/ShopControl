namespace ShopControl.Arguments.Arguments.Base;

public class BaseInputIdentityView<TInputIndetityView> where TInputIndetityView : BaseInputIdentityView<TInputIndetityView>, IBaseIdentity { }
public class BaseInputIdentityView_0 : BaseInputIdentityView<BaseInputIdentityView_0>, IBaseIdentity
{
    public long Id { get; private set; }
}