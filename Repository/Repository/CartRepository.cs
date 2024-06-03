using BusinessObjects_Layer;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository;

public class CartRepository : Repository<Cart>, ICartRepository
{
    public ApplicationDataContext _context;
    public CartRepository(ApplicationDataContext context) : base(context)
    {
        _context = context;
    }

    public void Update(Cart cart)
    {
        var cartDB = _context.Carts.FirstOrDefault(x => x.Id == cart.Id);

        if (cartDB != null)
        {
            cart.ProductId = cartDB.ProductId;
            cart.ApplicationUserId = cartDB.ApplicationUserId;
            cart.Count = cartDB.Count;
        }
    }
}
