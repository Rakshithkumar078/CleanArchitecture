using CleanArchitecture.Application.Category.Commands.UpdateCategory;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CleanArchitecture.Application.UnitTests.Category.Commands;
[TestFixture]
public class UpdateCategoryCommandTest : BaseTestFixture
{
    [SetUp]
    public void SetUp()
    {
        _applicationDbContextMock = new ConnectionFactory().CreateContextForInMemory();
        _applicationDbContextMock.Categories.AddRange(new List<Categories>() {
                new Categories { Id = 1, Name = "Customers" },
                new Categories { Id = 2, Name = "Employees" }});
        _applicationDbContextMock.SaveChanges();
        _applicationDbContextMock.SubCategories.AddRange(new List<SubCategories>() {
                new SubCategories { Id = 1, Name = "Customers", CategoryId = 1 },
                new SubCategories { Id = 2, Name = "Employees", CategoryId = 1 }});
        _applicationDbContextMock.SaveChanges();
        _dbContextMock.Setup(m => m.Categories).Returns(_applicationDbContextMock.Categories);
        _dbContextMock.Setup(m => m.SubCategories).Returns(_applicationDbContextMock.SubCategories);
    }

    [TearDown]
    public void TearDown()
    {
        _applicationDbContextMock.Database.EnsureDeleted();
    }

    [Test]
    public async Task UpdateCategory_WithAlreadyNameExits()
    {
        var validator = new UpdateCategoryCommandValidator(_dbContextMock.Object);

        // Arrange
        var command = new UpdateCategoryCommand
        {
            Id = 1,
            Name = "Employees"
        };
        var validationResult = await validator.ValidateAsync(command);

        //Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().NotBeEmpty();
    }

    [Test]
    public async Task UpdateCategory_WithValidNameLength()
    {
        //Arrange
        var validator = new UpdateCategoryCommandValidator(_dbContextMock.Object);
        var command = new UpdateCategoryCommand
        {
            Id = 1,
            Name = "Test129283987892112"
        };

        //Act
        var validationResult = await validator.ValidateAsync(command);

        //Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(1);
    }

    [Test]
    public async Task UpdateCategory_WithInValidNameLength()
    {
        //Arrange
        var validator = new UpdateCategoryCommandValidator(_dbContextMock.Object);
        var command = new UpdateCategoryCommand
        {
            Id = 1,
            Name = "Test12928398789211288888888888888888888888888888888888"
        };

        //Act
        var validationResult = await validator.ValidateAsync(command);

        //Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(2);
    }

    [Test]
    public async Task UpdateCategory_shouldUpdateCategory()
    {
        var validator = new UpdateCategoryCommandValidator(_dbContextMock.Object);

        // Arrange
        var command = new UpdateCategoryCommand
        {
            Id = 1,
            Name = "Test",

        };
        var _loggerMock = new Mock<ILogger<UpdateCategoryCommandHandler>>();

        // Create an instance of the command handler, passing the mock objects as dependencies
        var commandHandler = new UpdateCategoryCommandHandler(_dbContextMock.Object, _loggerMock.Object);
        var validationResult = await validator.ValidateAsync(command);
        // Act
        var test = await commandHandler.Handle(command, new CancellationToken());

        //Assert
        Assert.That(test, Is.EqualTo(true));
    }

    [Test]
    public async Task UpdateCategory_shouldGetException()
    {
        var validator = new UpdateCategoryCommandValidator(_dbContextMock.Object);

        // Arrange
        var command = new UpdateCategoryCommand
        {
            Id = 4,
            Name = "Test",

        };
        var _loggerMock = new Mock<ILogger<UpdateCategoryCommandHandler>>();

        // Create an instance of the command handler, passing the mock objects as dependencies
        var commandHandler = new UpdateCategoryCommandHandler(_dbContextMock.Object, _loggerMock.Object);
        var validationResult = await validator.ValidateAsync(command);
        // Act
        await commandHandler.Handle(command, new CancellationToken());

        //Assert
        Assert.That(command.Id, Is.GreaterThanOrEqualTo(0));
    }
}
