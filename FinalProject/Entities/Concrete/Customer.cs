using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Customer:IEntity
    {
        public string CustomerId { get; set; }
        public string ContactName { get; set; }
        public string CompanyNmae { get; set; }
        public string City { get; set; }
    }
}
