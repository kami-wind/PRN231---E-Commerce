using BusinessObjects_Layer;
using Repository.IRepository;

namespace Repository.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{

    public ApplicationDataContext _context;
    public CategoryRepository(ApplicationDataContext context) : base(context)
    {
        _context = context;
    }

    public void Update(Category category)
    {
        var categoryDB = _context.Categories.FirstOrDefault(x => x.Id == category.Id);

        if (categoryDB != null)
        {
            categoryDB.Name = category.Name;
        }
    }
}
