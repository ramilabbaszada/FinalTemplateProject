using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Category: IEntity
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
