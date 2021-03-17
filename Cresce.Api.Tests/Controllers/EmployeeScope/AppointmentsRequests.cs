using System;
using Cresce.Api.Models;

namespace Cresce.Api.Tests.Controllers.EmployeeScope
{
    internal class SessionsRequests : ControllerRequests<SessionModel>
    {
        public override string EntitiesUrl => "api/v1/Sessions";

        public override SessionModel[] GetExpectedList()
        {
            return new[]
            {
                new SessionModel
                {
                    Id = 1,
                    Discount = 10.0,
                    Hours = 3.5,
                    Value = 30.0,
                    CustomerId = 1,
                    EmployeeId = 1,
                    ServiceId = 1,
                    StartedAt = new DateTime(2020, 02, 10)
                }
            };
        }
    }
}
