namespace DddCqrsExample.Framework
{
    public interface IRepository<T> 
        where T : Aggregate, new()
    {
        T Get(string id);
        void Save(T item);
    }
}