using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Categories> Categories { get; set; }
    DbSet<SubCategories> SubCategories { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
