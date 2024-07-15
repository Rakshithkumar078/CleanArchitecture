using ProtEquity.Application.Category.Queries.GetAllCategories;
using ProtEquity.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace ProtEquity.Application.UnitTests.Category.Queries;
[TestFixture]
public class GetAllCategoriesTest : BaseTestFixture
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
    public async Task GetAllCategories()
    {
        //Mock Database
        _applicationDbContextMock.Categories.AddRange(new List<Categories>() {
                new Categories { Id = 1, Name = "Customers", Created = DateTime.Now, CreatedBy= "Admin", LastModified = DateTime.Now, LastModifiedBy ="Admin" },
                new Categories { Id = 2, Name = "Employees", Created = DateTime.Now, CreatedBy= "Admin", LastModified = DateTime.Now, LastModifiedBy ="Admin" }});
        _applicationDbContextMock.SaveChanges();
        _dbContextMock.Setup(m => m.Categories).Returns(_applicationDbContextMock.Categories);

        _applicationDbContextMock.SubCategories.AddRange(new List<SubCategories>() {
                new SubCategories { Id = 1, Name = "Customer Focus",CategoryId = 1, Created = DateTime.Now, CreatedBy= "Admin", LastModified = DateTime.Now, LastModifiedBy ="Admin" },
                new SubCategories { Id = 2, Name = "Team Work", CategoryId = 2, Created = DateTime.Now, CreatedBy= "Admin", LastModified = DateTime.Now, LastModifiedBy ="Admin" },
                new SubCategories { Id = 3, Name = "Team Work", CategoryId = 2, Created = DateTime.Now, CreatedBy= "Admin", LastModified = DateTime.Now, LastModifiedBy ="Admin" }});
        _applicationDbContextMock.SaveChanges();
        _dbContextMock.Setup(m => m.Categories).Returns(_applicationDbContextMock.Categories);

        //Arrange
        var logger = new Mock<ILogger<GetAllCategoriesQueryHandler>>();
        var allCategories = new GetAllCategoriesQueryHandler(_dbContextMock.Object, _mapper, logger.Object);

        //Act
        var Categories = await allCategories.Handle(new GetAllCategoriesQuery(), new CancellationToken());

        //Assert
        ClassicAssert.AreEqual(2, Categories.Count);
    }

    [Test]
    public async Task GetAllCategories_NotFound()
    {
        //Mock Database
        _applicationDbContextMock.Categories.AddRange(new List<Categories>() { });
        _dbContextMock.Setup(m => m.Categories).Returns(_applicationDbContextMock.Categories);

        //Arrange
        var logger = new Mock<ILogger<GetAllCategoriesQueryHandler>>();
        var allCategories = new GetAllCategoriesQueryHandler(_dbContextMock.Object, _mapper, logger.Object);

        //Act
        var Categories = await allCategories.Handle(new GetAllCategoriesQuery(), new CancellationToken());

        //Assert
        Categories.Count.Should().Be(0);
    }

    [Test]
    public async Task GetAllProductCategories_Exception()
    {
        //Arrange
        var logger = new Mock<ILogger<GetAllCategoriesQueryHandler>>();
        var allCategories = new GetAllCategoriesQueryHandler(_dbContextMock.Object, _mapper, logger.Object);

        //Act
        var Categories = await allCategories.Handle(new GetAllCategoriesQuery(), new CancellationToken());

        //Assert
        Assert.That(Categories, Is.Empty);
    }
}
