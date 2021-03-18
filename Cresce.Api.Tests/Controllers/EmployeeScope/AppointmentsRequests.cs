using System;
using Cresce.Core.Appointments;

namespace Cresce.Api.Tests.Controllers.EmployeeScope
{
    internal class AppointmentsRequests : ControllerRequests<Appointment>
    {
        public override string EntitiesUrl => "api/v1/Appointments";

        public override Appointment[] GetExpectedList()
        {
            return new[]
            {
                new Appointment
                {
                    Id = 1,
                    CustomerId = 1,
                    EmployeeId = 1,
                    ServiceId = 1,
                    Color = "0xFF2196F3",
                    From = new DateTime(2021, 3, 16, 10, 0, 0),
                    To = new DateTime(2021, 3, 16, 11, 0, 0),
                    EventName = "Diogo Quintas\nDevelopment",
                },
                new Appointment
                {
                    Id = 2,
                    CustomerId = 1,
                    EmployeeId = 1,
                    ServiceId = 1,
                    Color = "0xFF2196F3",
                    From = new DateTime(2021, 3, 16, 15, 0, 0),
                    To = new DateTime(2021, 3, 16, 16, 0, 0),
                    EventName = "Diogo Quintas\nDevelopment",
                    Recurrence = new Recurrence
                    {
                        Type = RecurrenceType.Weekly,
                        Start = new DateTime(2021, 3, 16),
                        End = new DateTime(2021, 4, 16),
                        WeekDays = new []{ WeekDays.Monday, WeekDays.Tuesday }
                    }

                },
            };
        }
    }
}
