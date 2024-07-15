using CleanArchitecture.Application.Category.Commands.DeleteCategory;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CleanArchitecture.Application.UnitTests.Category.Commands;
[TestFixture]
public class DeleteCategoryCommandTest : BaseTestFixture
{
    [SetUp]
    public void SetUp()
    {
        _applicationDbContextMock = new ConnectionFactory().CreateContextForInMemory();
    }

    [TearDown]
    public void TearDown()
    {
        _applicationDbContextMock.Database.EnsureDeleted();
    }

    [Test]
    public async Task DeleteCategory_SuccessfullyDeleted()
    {
        //Mock Database
        _applicationDbContextMock.Categories.AddRange(new List<Categories>() {
                new Categories { Id = 1, Name = "Customers", Created = DateTime.Now, CreatedBy= "Admin", LastModified = DateTime.Now, LastModifiedBy ="Admin" },
                new Categories { Id = 2, Name = "Employees", Created = DateTime.Now, CreatedBy= "Admin", LastModified = DateTime.Now, LastModifiedBy ="Admin" }});
        _applicationDbContextMock.SaveChanges();
        _dbContextMock.Setup(m => m.Categories).Returns(_applicationDbContextMock.Categories);

        _applicationDbContextMock.SubCategories.AddRange(new List<SubCategories>() {
                new SubCategories { Id = 1, Name = "Customer Focus",CategoryId = 1, Created = DateTime.Now, CreatedBy= "Admin", LastModified = DateTime.Now, LastModifiedBy ="Admin", IsDeleted = false },
                new SubCategories { Id = 2, Name = "Team Work", CategoryId = 2, Created = DateTime.Now, CreatedBy= "Admin", LastModified = DateTime.Now, LastModifiedBy ="Admin" , IsDeleted = false },
                new SubCategories { Id = 3, Name = "Team Work", CategoryId = 2, Created = DateTime.Now, CreatedBy= "Admin", LastModified = DateTime.Now, LastModifiedBy ="Admin", IsDeleted = false  }});
        _applicationDbContextMock.SaveChanges();
        _dbContextMock.Setup(m => m.SubCategories).Returns(_applicationDbContextMock.SubCategories);
        //Arrange
        var command = new DeleteCategoryCommand
        {
            Id = 2,
        };
        var _loggerMock = new Mock<ILogger<DeleteCategoryCommandHandler>>();

        //Act
        var Category = new DeleteCategoryCommandHandler(_dbContextMock.Object, _loggerMock.Object);
        var DeleteCategory = await Category.Handle(new DeleteCategoryCommand() { Id = command.Id }, new CancellationToken());
        _applicationDbContextMock.SaveChanges();

        //Assert
        DeleteCategory.Should().BeTrue();
    }

    [Test]
    public async Task DeleteCategory_Failure()
    {
        //Mock Database
        _applicationDbContextMock.Categories.AddRange(new List<Categories>() {
                new Categories { Id = 1, Name = "Customers", Created = DateTime.Now, CreatedBy= "Admin", LastModified = DateTime.Now, LastModifiedBy ="Admin" },
                new Categories { Id = 2, Name = "Employees", Created = DateTime.Now, CreatedBy= "Admin", LastModified = DateTime.Now, LastModifiedBy ="Admin" }});
        _applicationDbContextMock.SaveChanges();
        _dbContextMock.Setup(m => m.Categories).Returns(_applicationDbContextMock.Categories);

        _applicationDbContextMock.SubCategories.AddRange(new List<SubCategories>() {
                new SubCategories { Id = 1, Name = "Customer Focus",CategoryId = 1, Created = DateTime.Now, CreatedBy= "Admin", LastModified = DateTime.Now, LastModifiedBy ="Admin", IsDeleted = false },
                new SubCategories { Id = 2, Name = "Team Work", CategoryId = 2, Created = DateTime.Now, CreatedBy= "Admin", LastModified = DateTime.Now, LastModifiedBy ="Admin" , IsDeleted = false },
                new SubCategories { Id = 3, Name = "Team Work", CategoryId = 2, Created = DateTime.Now, CreatedBy= "Admin", LastModified = DateTime.Now, LastModifiedBy ="Admin", IsDeleted = false  }});
        _applicationDbContextMock.SaveChanges();
        _dbContextMock.Setup(m => m.SubCategories).Returns(_applicationDbContextMock.SubCategories);
        //Arrange
        var command = new DeleteCategoryCommand
        {
            Id = 3,
        };
        var _loggerMock = new Mock<ILogger<DeleteCategoryCommandHandler>>();

        //Act
        var Category = new DeleteCategoryCommandHandler(_dbContextMock.Object, _loggerMock.Object);
        var DeleteCategory = await Category.Handle(new DeleteCategoryCommand() { Id = command.Id }, new CancellationToken());
        _applicationDbContextMock.SaveChanges();

        //Assert
        DeleteCategory.Should().BeFalse();
    }

    [Test]
    public async Task DeleteJobCategory_Exception()
    {
        //Arrange
        var command = new DeleteCategoryCommand
        {
            Id = 3,
        };
        var _loggerMock = new Mock<ILogger<DeleteCategoryCommandHandler>>();

        //Act
        var Category = new DeleteCategoryCommandHandler(_dbContextMock.Object, _loggerMock.Object);
        var DeleteCategory = await Category.Handle(new DeleteCategoryCommand() { Id = command.Id }, new CancellationToken());
        _applicationDbContextMock.SaveChanges();

        //Assert
        DeleteCategory.Should().BeFalse();
    }
}
