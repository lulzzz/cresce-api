using System;

namespace Cresce.Core.Appointments
{
    public record Appointment
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int EmployeeId { get; set; }
        public int CustomerId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Color { get; set; }
        public Recurrence? Recurrence { get; set; }
    }
}
