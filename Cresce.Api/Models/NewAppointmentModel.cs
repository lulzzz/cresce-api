using System;
using Cresce.Core;
using Cresce.Core.Appointments;

namespace Cresce.Api.Models
{
    public record NewAppointmentModel : IUnwrap<Appointment>
    {
        public int Id { get; init; }
        public int ServiceId { get; init; }
        public int CustomerId { get; init; }
        public DateTime From { get; init; }
        public DateTime To { get; init; }
        public Recurrence? Recurrence { get; init; }

        public Appointment Unwrap()
        {
            return new()
            {
                Id = Id,
                ServiceId = ServiceId,
                CustomerId = CustomerId,
                From = From,
                To = To,
                Recurrence = Recurrence,
            };
        }
    }
}
