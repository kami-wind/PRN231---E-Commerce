using BusinessObjects_Layer;

namespace Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    void Update(Product product);
}
