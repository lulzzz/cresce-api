using Cresce.Core.Customers;

namespace Cresce.Core.Sql.Customers
{
    internal class CustomerModel : IUnwrap<Customer>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }

        public Customer Unwrap()
        {
            return new Customer
            {
                Id = Id,
                Name = Name,
                Image = new Image(Image),
            };
        }
    }
}