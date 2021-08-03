using Cresce.Core.Services;

namespace Cresce.Core.Sql.Services
{
    internal class ServiceDto : IUnwrap<Service>, IWrap<Service>, IHaveAutoIdentity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Value { get; set; }
        public byte[] Image { get; set; } = new byte[0];

        public Service Unwrap()
        {
            return new()
            {
                Id = Id,
                Name = Name,
                Image = new Image(Image),
                Value = Value
            };
        }

        public void Wrap(Service entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
