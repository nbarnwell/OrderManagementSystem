using System.Data;

namespace DddCqrsExample.Web.Infrastructure
{
    public interface IReadStoreConnectionFactory
    {
        IDbConnection Create();
    }
}