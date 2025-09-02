using ApplicationCore.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;    

namespace Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(EventAppContext dbContext) : base(dbContext)
    {
    }
}
