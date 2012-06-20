using System.Data;

namespace DddCqrsExample.Web.Intrastructure
{
    public interface IReadStoreConnectionFactory
    {
        IDbConnection Create();
    }
}