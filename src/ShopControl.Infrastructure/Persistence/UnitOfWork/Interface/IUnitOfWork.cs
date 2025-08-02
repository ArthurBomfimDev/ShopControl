namespace ShopControl.Infrastructure.Interface.UnitOfWork;

public interface IUnitOfWork
{
    void BeginTransaction();
    void Commit();
}