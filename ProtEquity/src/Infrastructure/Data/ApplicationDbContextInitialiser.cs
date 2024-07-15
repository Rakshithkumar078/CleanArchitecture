using ProtEquity.Domain.Constants;
using ProtEquity.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ProtEquity.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        try
        {
            await SeedCategoriesAsync();
            await SeedSubCategoriesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// To Seed Categories data
    /// </summary>
    /// <returns></returns>
    private async Task SeedCategoriesAsync()
    {
        if (!_context.Categories.Any())
        {
            var categoryData = new List<Categories>()
        {
            new Categories() { Name = "Customers", IsDeleted = false, Created = DateTime.Now, CreatedBy ="Admin", LastModified = DateTime.Now, LastModifiedBy = "Admin",},
            new Categories() { Name = "Employees", IsDeleted = false, Created = DateTime.Now, CreatedBy ="Admin", LastModified = DateTime.Now, LastModifiedBy = "Admin",},
            new Categories() { Name = "Society", IsDeleted = false, Created = DateTime.Now, CreatedBy ="Admin", LastModified = DateTime.Now, LastModifiedBy = "Admin",},
            new Categories() { Name = "Products And Services", IsDeleted = false, Created = DateTime.Now, CreatedBy ="Admin", LastModified = DateTime.Now, LastModifiedBy = "Admin",},
            new Categories() { Name = "Management", IsDeleted = false, Created = DateTime.Now, CreatedBy ="Admin", LastModified = DateTime.Now, LastModifiedBy = "Admin",},
            new Categories() { Name = "Fairness", IsDeleted = false, Created = DateTime.Now, CreatedBy ="Admin", LastModified = DateTime.Now, LastModifiedBy = "Admin",},
        };
            _context.Categories.AddRange(categoryData);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }

    /// <summary>
    /// To Seed SubCategories data
    /// </summary>
    /// <returns></returns>
    private async Task SeedSubCategoriesAsync()
    {
        if (!_context.SubCategories.Any())
        {
            var subCategoryData = new List<SubCategories>()
        {
            new() { Name = "Customer Focus", CategoryId = 1, Created = DateTime.Now, CreatedBy = "Admin", LastModified = DateTime.Now, LastModifiedBy = "Admin", IsDeleted = false },
            new() { Name = "Team Work", CategoryId = 2, Created = DateTime.Now, CreatedBy = "Admin", LastModified = DateTime.Now, LastModifiedBy = "Admin", IsDeleted = false },
            new() { Name = "Communication Skills", CategoryId = 2, Created = DateTime.Now, CreatedBy = "Admin", LastModified = DateTime.Now, LastModifiedBy = "Admin", IsDeleted = false },
            new() { Name = "Cost Consciousness", CategoryId = 3, Created = DateTime.Now, CreatedBy = "Admin", LastModified = DateTime.Now, LastModifiedBy = "Admin", IsDeleted = false },
            new() { Name = "Job Knowledge/Technical Skills", CategoryId = 4, Created = DateTime.Now, CreatedBy = "Admin", LastModified = DateTime.Now, LastModifiedBy = "Admin", IsDeleted = false },
            new() { Name = "Work Attitde", CategoryId = 4, Created = DateTime.Now, CreatedBy = "Admin", LastModified = DateTime.Now, LastModifiedBy = "Admin", IsDeleted = false },
            new() { Name = "Quality of Work", CategoryId = 4, Created = DateTime.Now, CreatedBy = "Admin", LastModified = DateTime.Now, LastModifiedBy = "Admin", IsDeleted = false },
            new() { Name = "Quantity of Work", CategoryId = 4, Created = DateTime.Now, CreatedBy = "Admin", LastModified = DateTime.Now, LastModifiedBy = "Admin", IsDeleted = false },
            new() { Name = "Safety", CategoryId = 4, Created = DateTime.Now, CreatedBy = "Admin", LastModified = DateTime.Now, LastModifiedBy = "Admin", IsDeleted = false },
            new() { Name = "Process Improvement", CategoryId = 4, Created = DateTime.Now, CreatedBy = "Admin", LastModified = DateTime.Now, LastModifiedBy = "Admin", IsDeleted = false },
            new() { Name = "Problem Solving", CategoryId = 5, Created = DateTime.Now, CreatedBy = "Admin", LastModified = DateTime.Now, LastModifiedBy = "Admin", IsDeleted = false },
            new() { Name = "Supervision / Motivation of Staff", CategoryId = 5, Created = DateTime.Now, CreatedBy = "Admin", LastModified = DateTime.Now, LastModifiedBy = "Admin", IsDeleted = false },
            new() { Name = "Attendance/Punctuality", CategoryId = 6, Created = DateTime.Now, CreatedBy = "Admin", LastModified = DateTime.Now, LastModifiedBy = "Admin", IsDeleted = false },
            new() { Name = "Dependability/Responsibility", CategoryId = 6, Created = DateTime.Now, CreatedBy = "Admin", LastModified = DateTime.Now, LastModifiedBy = "Admin", IsDeleted = false },
        };
            _context.SubCategories.AddRange(subCategoryData);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
