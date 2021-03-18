using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;

namespace Cresce.Core.Sessions
{
    public interface ISessionServices
    {
        Task<IEnumerable<Session>> GetSessions(IEmployeeAuthorization authorization);
        Task<Session> CreateSession(Session session, IEmployeeAuthorization getEmployeeAuthorization);
    }
}
