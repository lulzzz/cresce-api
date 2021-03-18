using System;

namespace Cresce.Core.Appointments
{
    public record Appointment
    {
        public int Id { get; init; }
        public int ServiceId { get; init; }
        public int EmployeeId { get; init; }
        public int CustomerId { get; init; }
        public DateTime From { get; init; }
        public DateTime To { get; init; }
        public string Color { get; init; } = string.Empty;
        public Recurrence? Recurrence { get; init; }
        public string EventName { get; init; } = string.Empty;
    }
}
