using CleanArchitecture.Application.SubCategory.Commands.UpdateSubCategory;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CleanArchitecture.Application.UnitTests.SubCategory.Commands;
[TestFixture]
public class UpdateSubCategoryCommandTest : BaseTestFixture
{
    [SetUp]
    public void SetUp()
    {
        _applicationDbContextMock = new ConnectionFactory().CreateContextForInMemory();
        _applicationDbContextMock.SubCategories.AddRange(new List<SubCategories>() {
                new SubCategories { Id = 1, Name = "Termination",CategoryId = 2 },
                new SubCategories { Id = 2, Name = "Salary increment",CategoryId=2 },
                new SubCategories { Id = 3, Name = "No salary Increment", CategoryId = 2 } });
        _dbContextMock.Setup(m => m.SubCategories).Returns(_applicationDbContextMock.SubCategories);
        _applicationDbContextMock.SaveChanges();
    }

    [TearDown]
    public void TearDown()
    {
        _applicationDbContextMock.Database.EnsureDeleted();
    }


    //Update recommendation success scenario
    [Test]
    public async Task UpdateRecommendation_Success()
    {
        //Arrange
        var validator = new UpdateSubCategoryCommandValidator(_dbContextMock.Object);
        var command = new UpdateSubCategoryCommand
        {
            Id = 1,
            Name = "Ready for promotion",
            CategoryId = 2,
        };
        var _loggerMock = new Mock<ILogger<UpdateSubCategoryCommandHandler>>();

        // Create an instance of the command handler, passing the mock objects as dependencies
        var commandHandler = new UpdateSubCategoryCommandHandler(_dbContextMock.Object, _loggerMock.Object);
        var validationResult = await validator.ValidateAsync(command);

        // Act
        await commandHandler.Handle(command, new CancellationToken());

        // Assert
        var result = await commandHandler.Handle(command, new CancellationToken());

        result.Should().BeTrue();
    }

    //Update recommendation unique name scenario
    [Test]
    public async Task UpdateRecommendationCommand_WithNotAlreadyExits()
    {
        //Arrange
        var validator = new UpdateSubCategoryCommandValidator(_dbContextMock.Object);
        var command = new UpdateSubCategoryCommand
        {
            Id = 3,
            Name = "Consider for merit increment",
            CategoryId = 4,
        };

        //Act
        var validationResult = await validator.ValidateAsync(command);

        //Assert
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    //Update recommendation name already exists sceanrio
    [Test]
    public async Task UpdateRecommendation_WithAlreadyExits()
    {
        var validator = new UpdateSubCategoryCommandValidator(_dbContextMock.Object);

        // Arrange
        var command = new UpdateSubCategoryCommand
        {
            Id = 2,
            Name = "No salary Increment",
            CategoryId = 2,
        };
        var validationResult = await validator.ValidateAsync(command);

        //Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().NotBeEmpty();
    }

    //Update recommendation exception scenario
    [Test]
    public async Task UpdateRecommendation_ShouldGetException()
    {
        var validator = new UpdateSubCategoryCommandValidator(_dbContextMock.Object);

        // Arrange
        var command = new UpdateSubCategoryCommand
        {
            Id = 4,
            Name = "Test",
            CategoryId = 5,

        };
        var _loggerMock = new Mock<ILogger<UpdateSubCategoryCommandHandler>>();

        // Create an instance of the command handler, passing the mock objects as dependencies
        var commandHandler = new UpdateSubCategoryCommandHandler(_dbContextMock.Object, _loggerMock.Object);
        var validationResult = await validator.ValidateAsync(command);
        // Act
        var result = await commandHandler.Handle(command, new CancellationToken());

        //Assert
        result.Should().BeFalse();
    }
}
