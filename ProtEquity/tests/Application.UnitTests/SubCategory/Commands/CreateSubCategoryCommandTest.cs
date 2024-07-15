using ProtEquity.Application.SubCategory.Commands.CreateSubCategory;
using ProtEquity.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace ProtEquity.Application.UnitTests.SubCategory.Commands;
[TestFixture]
public class CreateSubCategoryCommandTest : BaseTestFixture
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
    public async Task ShouldCreateSubCategory()
    {
        //Mock Database
        _applicationDbContextMock?.SubCategories.AddRange(new List<SubCategories>() {
                new SubCategories { Id = 1, Name = "Food and Drinks",CategoryId =2 },
                new SubCategories { Id = 2, Name = "Gadgets" ,CategoryId = 2}});
        _applicationDbContextMock?.SaveChanges();
        _dbContextMock.Setup(m => m.SubCategories).Returns(_applicationDbContextMock.SubCategories);

        //Arrange
        var command = new CreateSubCategoryCommand
        {
            Name = "SomeXYZ",
            CategoryId = 3,
        };
        var _loggerMock = new Mock<ILogger<CreateSubCategoryCommandHandler>>();
        var SubCategory = new CreateSubCategoryCommandHandler(_dbContextMock.Object, _loggerMock.Object);

        //Act
        var res = await SubCategory.Handle(command, new CancellationToken());
        _applicationDbContextMock.SaveChanges();

        //Assert
        res.Should().Be(3);
    }
    [Test]
    public async Task CreateSubCategoryCommandValidator_ExistsSubCategory()
    {
        _applicationDbContextMock.SubCategories.AddRange(new List<SubCategories>() {
                new SubCategories { Id = 1, Name = "Customers" , CategoryId = 2 },
                new SubCategories { Id = 2, Name = "Employees", CategoryId = 3 }});
        _applicationDbContextMock.SaveChanges();
        _dbContextMock.Setup(m => m.SubCategories).Returns(_applicationDbContextMock.SubCategories);

        //Act
        var validator = new CreateSubCategoryCommandValidator(_dbContextMock.Object);

        // Arrange
        var command = new CreateSubCategoryCommand
        {
            Name = "Customers",
            CategoryId = 2,
        };
        var validationResult = await validator.ValidateAsync(command);

        //Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(1);
    }

    [Test]
    public async Task CreateSubCategoryCommandValidator_LengthInvalidValidation()
    {
        _applicationDbContextMock.SubCategories.AddRange(new List<SubCategories>() {
                    new SubCategories { Id = 1, Name = "Customers" , CategoryId = 2 },
                    new SubCategories { Id = 2, Name = "Employees" , CategoryId = 3 }});
        _applicationDbContextMock.SaveChanges();
        _dbContextMock.Setup(m => m.SubCategories).Returns(_applicationDbContextMock.SubCategories);

        //Act
        var validator = new CreateSubCategoryCommandValidator(_dbContextMock.Object);

        // Arrange
        var command = new CreateSubCategoryCommand
        {
            Name = "CaptainAmericaProposingtoLoverBoyByVisitingParkNearHisHome"
        };
        var validationResult = await validator.ValidateAsync(command);

        //Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(1);
    }

    [Test]
    public async Task CreateSubCategoryCommandValidator_LengthValidValidation()
    {
        _applicationDbContextMock.SubCategories.AddRange(new List<SubCategories>() {
                        new SubCategories { Id = 1, Name = "Customers",CategoryId = 1 },
                        new SubCategories { Id = 2, Name = "Employees",CategoryId = 3 }});
        _applicationDbContextMock.SaveChanges();
        _dbContextMock.Setup(m => m.SubCategories).Returns(_applicationDbContextMock.SubCategories);

        //Act
        var validator = new CreateSubCategoryCommandValidator(_dbContextMock.Object);

        // Arrange
        var command = new CreateSubCategoryCommand
        {
            Name = "BusinessAutomation"
        };
        var validationResult = await validator.ValidateAsync(command);

        //Assert
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().HaveCount(0);
    }
}
