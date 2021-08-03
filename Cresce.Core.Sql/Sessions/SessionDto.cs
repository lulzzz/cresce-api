using System;
using Cresce.Core.Sessions;

namespace Cresce.Core.Sql.Sessions
{
    internal class SessionDto : IUnwrap<Session>, IWrap<Session>, IHaveAutoIdentity
    {
        public int Id { get; set; }
        public DateTime StartedAt { get; set; }
        public int ServiceId { get; set; }
        public int EmployeeId { get; set; }
        public int CustomerId { get; set; }
        public double Hours { get; set; }
        public double Discount { get; set; }
        public double Value { get; set; }

        public Session Unwrap()
        {
            return new()
            {
                Id = Id,
                StartedAt = StartedAt,
                ServiceId = ServiceId,
                EmployeeId = EmployeeId,
                CustomerId = CustomerId,
                Hours = Hours,
                Discount = Discount,
                Value = Value
            };
        }

        public void Wrap(Session entity)
        {
            Id = entity.Id;
            StartedAt = entity.StartedAt;
            ServiceId = entity.ServiceId;
            EmployeeId = entity.EmployeeId;
            CustomerId = entity.CustomerId;
            Hours = entity.Hours;
            Discount = entity.Discount;
            Value = entity.Value;
        }
    }
}
