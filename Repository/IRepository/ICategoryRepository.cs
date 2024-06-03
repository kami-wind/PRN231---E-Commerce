using BusinessObjects_Layer;

namespace Repository.IRepository;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category category); 
}
