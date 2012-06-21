using System.Data;

namespace DddCqrsEsExample.Web.Infrastructure
{
    public interface IReadStoreConnectionFactory
    {
        IDbConnection Create();
    }
}