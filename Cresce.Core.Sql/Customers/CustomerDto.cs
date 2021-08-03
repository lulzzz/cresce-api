using Cresce.Core.Customers;

namespace Cresce.Core.Sql.Customers
{
    internal class CustomerDto : IUnwrap<Customer>, IWrap<Customer>, IHaveAutoIdentity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[] Image { get; set; } = new byte[0];

        public Customer Unwrap()
        {
            return new()
            {
                Id = Id,
                Name = Name,
                Image = new Image(Image),
            };
        }

        public void Wrap(Customer entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
