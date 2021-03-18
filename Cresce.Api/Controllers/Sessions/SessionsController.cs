using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cresce.Api.Models;
using Cresce.Core.Authentication;
using Cresce.Core.Sessions;
using Microsoft.AspNetCore.Mvc;

namespace Cresce.Api.Controllers.Sessions
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionServices _service;

        public SessionsController(ISessionServices service) => _service = service;

        [HttpGet]
        public async Task<IEnumerable<SessionModel>> GetSessions(
            [FromHeader] IEmployeeAuthorization authorization)
        {
            return (await _service.GetSessions(authorization))
                .Select(entity => new SessionModel(entity));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSession(
            [FromHeader] IEmployeeAuthorization authorization,
            NewSessionModel session)
        {
            var newSession = await _service.CreateSession(session.Unwrap(), authorization);
            return Created(
                $"api/v1/Session/{newSession.Id}",
                new SessionModel(
                    newSession
                )
            );
        }
    }
}
