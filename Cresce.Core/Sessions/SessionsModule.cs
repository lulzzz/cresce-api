using Microsoft.Extensions.DependencyInjection;

namespace Cresce.Core.Sessions
{
    public class SessionsModule : IServicesModule
    {
        public void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.RegisterGetEntities<Session>();
            serviceCollection.AddTransient<ISessionServices, SessionServices>();
        }
    }
}
