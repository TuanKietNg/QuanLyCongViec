namespace HDDT_BE.Models.Core
{
    public interface TEntity<T>
    {
        T Id { get; set; }
    }
}