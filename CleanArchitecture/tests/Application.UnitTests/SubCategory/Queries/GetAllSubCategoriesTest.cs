using CleanArchitecture.Application.SubCategory.Queries.GetAllSubCategories;
using CleanArchitecture.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CleanArchitecture.Application.UnitTests.SubCategory.Queries;
[TestFixture]
public class GetAllSubCategoriesTest : BaseTestFixture
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
    public async Task GetAllSubCategories()
    {
        _applicationDbContextMock.Categories.AddRange(new List<Categories>() {
                new Categories { Id = 1, Name = "Test"},
                new Categories { Id = 2, Name = "Normal"} });
        _applicationDbContextMock.SaveChanges();

        _dbContextMock.Setup(m => m.Categories).Returns(_applicationDbContextMock.Categories);

        _applicationDbContextMock.SubCategories.AddRange(new List<SubCategories>() {
                new SubCategories { Id = 1, Name = "Performance",  CategoryId = 1},
                new SubCategories { Id = 2, Name = "ExtraActivity", CategoryId = 2 } });
        _applicationDbContextMock.SaveChanges();

        _dbContextMock.Setup(m => m.SubCategories).Returns(_applicationDbContextMock.SubCategories);


        //Arrange
        var _loggerMock = new Mock<ILogger<GetAllSubCategoryQueryHandler>>();
        var queryHandler = new GetAllSubCategoryQueryHandler(_dbContextMock.Object, _mapper, _loggerMock.Object);

        //Act
        var allSubCategories = new GetAllSubCategoryQuery();
        var SubCategories = await queryHandler.Handle(allSubCategories, new CancellationToken());

        //Assert
        Assert.That(SubCategories.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task GetAllSubCategoryException()
    {
        //Arrange
        var _loggerMock = new Mock<ILogger<GetAllSubCategoryQueryHandler>>();
        var allSubCategories = new GetAllSubCategoryQueryHandler(_dbContextMock.Object, _mapper, _loggerMock.Object);

        //Act
        var subCategories = await allSubCategories.Handle(new GetAllSubCategoryQuery(), new CancellationToken());

        //Assert
        Assert.That(subCategories.Count, Is.EqualTo(0));
    }
}
