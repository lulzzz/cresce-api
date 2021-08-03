using System;

namespace Cresce.Core.Appointments
{
    public class NewAppointment
    {
        public int Id { get; init; }
        public int ServiceId { get; init; }
        public int CustomerId { get; init; }
        public DateTime From { get; init; }
        public DateTime To { get; init; }
        public Recurrence? Recurrence { get; init; }

    }
}
