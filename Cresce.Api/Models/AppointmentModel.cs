using System;
using Cresce.Core.Appointments;

namespace Cresce.Api.Models
{
    public record AppointmentModel
    {
        public AppointmentModel(Appointment appointment)
        {
            Id = appointment.Id;
            ServiceId = appointment.ServiceId;
            EmployeeId = appointment.EmployeeId;
            CustomerId = appointment.CustomerId;
            From = appointment.From;
            To = appointment.To;
            Color = appointment.Color;
            Recurrence = appointment.Recurrence;
            EventName = appointment.EventName;
        }

        public AppointmentModel()
        {

        }

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
