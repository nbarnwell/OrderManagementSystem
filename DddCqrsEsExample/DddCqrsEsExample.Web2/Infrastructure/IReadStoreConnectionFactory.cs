using System.Data;

namespace DddCqrsEsExample.Web2.Infrastructure
{
    public interface IReadStoreConnectionFactory
    {
        IDbConnection Create();
    }
}