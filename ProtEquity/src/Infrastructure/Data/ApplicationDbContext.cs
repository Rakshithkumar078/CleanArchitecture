using System.Linq.Expressions;
using System.Reflection;
using ProtEquity.Application.Common.Interfaces;
using ProtEquity.Domain.Common;
using ProtEquity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProtEquity.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Categories> Categories { get; set; }
    public DbSet<SubCategories> SubCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
        ApplySoftDeleteFilter(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //ConvertDateTimeToLocal();
        return base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Filters only active records and removes Deleted record
    /// </summary>
    /// <param name="modelBuilder"></param>
    private void ApplySoftDeleteFilter(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseAuditableEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType);
                var body = Expression.Equal(Expression.Property(parameter, "IsDeleted"), Expression.Constant(false));
                var lambda = Expression.Lambda(body, parameter);
                //This expression will build the linq expression like x=>x.IsDeleted = false (filters only isdeleted = false records)
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }
}
