using System.Threading.Tasks;

namespace Cresce.Core
{
    public interface ICreateEntityGateway<in TEntity>
    {
        Task<int> Create(TEntity entity);
    }
}
