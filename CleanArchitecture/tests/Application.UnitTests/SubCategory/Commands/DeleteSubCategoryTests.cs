using CleanArchitecture.Application.SubCategory.Commands.DeleteSubCategory;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CleanArchitecture.Application.UnitTests.SubCategory.Commands;
[TestFixture]
public class DeleteSubCategoryTests : BaseTestFixture
{
    [SetUp]
    public void SetUp()
    {
        _applicationDbContextMock = new ConnectionFactory().CreateContextForInMemory();
        _applicationDbContextMock.SubCategories.AddRange(new List<SubCategories>()
        {
           new SubCategories { Id = 1, Name = "Termination",CategoryId = 3 },
           new SubCategories { Id = 2, Name = "Transfer to other types or work",CategoryId = 3},
           new SubCategories { Id = 3, Name = "Salary increment", CategoryId = 3}
        });
        _applicationDbContextMock.SaveChanges();
        _dbContextMock.Setup(m => m.SubCategories).Returns(_applicationDbContextMock.SubCategories);
    }

    [TearDown]
    public void TearDown()
    {
        _applicationDbContextMock.Database.EnsureDeleted();
    }

    //Delete recommendation success sceanrio
    [Test]
    public async Task DeleteSubCategory_SuccessfullyDeleted()
    {
        //Arrange
        var command = new DeleteSubCategoryCommand
        {
            Id = 2,
        };
        var _loggerMock = new Mock<ILogger<DeleteSubCategoryCommandHandler>>();
        var subCategory = new DeleteSubCategoryCommandHandler(_dbContextMock.Object, _loggerMock.Object);

        //Act
        var handler = await subCategory.Handle(new DeleteSubCategoryCommand() { Id = command.Id }, new CancellationToken());

        //Assert
        handler.Should().BeTrue();
    }

    //Delete recommendation for not found scenario
    [Test]
    public async Task DeleteSubCategory_NotFound()
    {
        //Arrange
        var command = new DeleteSubCategoryCommand
        {
            Id = 8,
        };
        var _loggerMock = new Mock<ILogger<DeleteSubCategoryCommandHandler>>();
        var recommendation = new DeleteSubCategoryCommandHandler(_dbContextMock.Object, _loggerMock.Object);

        //Act
        var handle = await recommendation.Handle(new DeleteSubCategoryCommand() { Id = command.Id }, new CancellationToken());

        //Assert
        handle.Should().BeFalse();
    }
}
