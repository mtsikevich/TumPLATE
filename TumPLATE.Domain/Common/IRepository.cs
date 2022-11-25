namespace TumPLATE.Domain.Common
{
    public interface IRepository<T,F>
    {
        Task<F> GetAsync(int id);
        
        Task SaveAsync();
    }
}
