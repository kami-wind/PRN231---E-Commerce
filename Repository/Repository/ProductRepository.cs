using BusinessObjects_Layer;
using Repository.IRepository;

namespace Repository.Repository;

internal class ProductRepository : Repository<Product>, IProductRepository
{
    public ApplicationDataContext _context;
    public ProductRepository(ApplicationDataContext context) : base(context)
    {
        _context = context;
    }

    public void Update(Product product)
    {
        var productDB = _context.Products.FirstOrDefault(x => x.Id == product.Id);

        if (productDB != null)
        {
            productDB.Name = product.Name;
            productDB.Description = product.Description;
            productDB.Price = product.Price;
            if (product.ImageURL != null)
            {
                productDB.ImageURL = product.ImageURL;
            }

        }
    }
}
