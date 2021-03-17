using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.GetEntities;

namespace Cresce.Core.Sessions
{
    internal class SessionServices : ISessionServices
    {
        private readonly IGetEntitiesService<Session> _getEntitiesGateway;
        private readonly ICreateEntityGateway<Session> _createEntityGateway;

        public SessionServices(
            IGetEntitiesService<Session> getEntitiesGateway,
            ICreateEntityGateway<Session> createEntityGateway
        )
        {
            _getEntitiesGateway = getEntitiesGateway;
            _createEntityGateway = createEntityGateway;
        }

        public Task<IEnumerable<Session>> GetSessions(IEmployeeAuthorization authorization)
            => _getEntitiesGateway.GetEntities(authorization);

        public async Task<Session> CreateSession(Session session, IEmployeeAuthorization authorization)
        {
            authorization.EnsureIsValid();

            session = session with
            {
                EmployeeId = authorization.EmployeeId
            };

            return session with
            {
                Id = await _createEntityGateway.Create(session)
            };
        }
    }
}
