using BusinessObjects_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository;

public interface ICartRepository : IRepository<Cart>
{
    void Update(Cart cart);
}
