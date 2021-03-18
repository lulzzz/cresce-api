using System;
using Cresce.Core.Appointments;

namespace Cresce.Core.Sql.Appointments
{
    public class AppointmentDto : IUnwrap<Appointment>, IWrap<Appointment>, IHaveAutoIdentity
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int EmployeeId { get; set; }
        public int CustomerId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string? RecurrenceType { get; set; }
        public string? RecurrenceWeekDays { get; set; }
        public DateTime? RecurrenceStart { get; set; }
        public DateTime? RecurrenceEnd { get; set; }

        public Appointment Unwrap()
        {
            return new Appointment
            {
                Id = Id,
                ServiceId = ServiceId,
                CustomerId = CustomerId,
                EmployeeId = EmployeeId,
                From = From,
                To = To,
                Recurrence = UnwrapRecurrence()
            };
        }

        private Recurrence? UnwrapRecurrence()
        {
            return RecurrenceType == null ? null : new Recurrence
            {
                Type = RecurrenceType,
                WeekDays = WeekDays.FromNumbers(RecurrenceWeekDays ?? ""),
                Start = RecurrenceStart ?? From,
                End = RecurrenceEnd,
            };
        }

        public void Wrap(Appointment entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
