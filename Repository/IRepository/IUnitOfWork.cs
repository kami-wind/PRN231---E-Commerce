namespace Repository.IRepository;

public interface IUnitOfWork
{
    ICategoryRepository Category {  get; }
    IProductRepository Product { get; }
    ICartRepository  Cart { get; }
    // IUser
    // IOrderRepository
    // IOrderDetailRepository
    void Save();
}
