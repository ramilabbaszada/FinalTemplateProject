using Entities.Concrete;
using Core.DataAccess;

namespace DataAccess.Abstract
{
    public interface IOrderDal:IEntityRepository<Order>
    {
    }
}
