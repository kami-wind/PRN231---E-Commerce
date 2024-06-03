using BusinessObjects_Layer;
using Repository.IRepository;

namespace Repository.Repository;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDataContext _context;

    public ICategoryRepository Category { get; set; }

    public IProductRepository Product { get; set; }

    public ICartRepository Cart { get; set; }

    public UnitOfWork(ApplicationDataContext context)
    {
        _context = context;
        Category = new CategoryRepository(context);
        Product = new ProductRepository(context);
        Cart = new CartRepository(context);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
