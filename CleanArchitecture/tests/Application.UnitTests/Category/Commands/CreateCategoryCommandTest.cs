using CleanArchitecture.Application.Category.Commands.CreateCategory;
using CleanArchitecture.Application.Category.Queries.GetAllCategories;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace CleanArchitecture.Application.UnitTests.Category.Commands;
[TestFixture]

public class CreateCategoryCommandTest : BaseTestFixture
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
    public async Task CreateCategoryCommandValidator_ValidCategory()
    {
        //Mock Database
        _applicationDbContextMock.Categories.AddRange(new List<Categories>() {
                new Categories { Id = 1, Name = "Customers" },
                new Categories { Id = 2, Name = "Employees" }});
        _applicationDbContextMock.SaveChanges();
        _dbContextMock.Setup(m => m.Categories).Returns(_applicationDbContextMock.Categories);

        //Act
        var validator = new CreateCategoryCommandValidator(_dbContextMock.Object);

        // Arrange
        var command = new CreateCategoryCommand
        {
            Name = "Management"
        };
        var validationResult = await validator.ValidateAsync(command);

        //Assert
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().HaveCount(0);
    }

    [Test]
    public async Task AddCategoryCommandValidator_ExistsProductCategory()
    {
        _applicationDbContextMock.Categories.AddRange(new List<Categories>() {
                new Categories { Id = 1, Name = "Customers" },
                new Categories { Id = 2, Name = "Employees" }});
        _applicationDbContextMock.SaveChanges();
        _dbContextMock.Setup(m => m.Categories).Returns(_applicationDbContextMock.Categories);

        //Act
        var validator = new CreateCategoryCommandValidator(_dbContextMock.Object);

        // Arrange
        var command = new CreateCategoryCommand
        {
            Name = "Customers"
        };
        var validationResult = await validator.ValidateAsync(command);

        //Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(1);
    }

    [Test]
    public async Task AddCategoryCommandValidator_LengthInvalidValidation()
    {
        _applicationDbContextMock.Categories.AddRange(new List<Categories>() {
                    new Categories { Id = 1, Name = "Customers" },
                    new Categories { Id = 2, Name = "Employees" }});
        _applicationDbContextMock.SaveChanges();
        _dbContextMock.Setup(m => m.Categories).Returns(_applicationDbContextMock.Categories);

        //Act
        var validator = new CreateCategoryCommandValidator(_dbContextMock.Object);

        // Arrange
        var command = new CreateCategoryCommand
        {
            Name = "testttsafatfsdscdasgfdsadsddassacrfsdscscdsscsdcdsr"
        };
        var validationResult = await validator.ValidateAsync(command);

        //Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(1);
    }

    [Test]
    public async Task AddCategoryCommandValidator_LengthValidValidation()
    {
        _applicationDbContextMock.Categories.AddRange(new List<Categories>() {
                        new Categories { Id = 1, Name = "Customers" },
                        new Categories { Id = 2, Name = "Employees" }});
        _applicationDbContextMock.SaveChanges();
        _dbContextMock.Setup(m => m.Categories).Returns(_applicationDbContextMock.Categories);

        //Act
        var validator = new CreateCategoryCommandValidator(_dbContextMock.Object);

        // Arrange
        var command = new CreateCategoryCommand
        {
            Name = "testttsafatfsdscdasgfdsadsddassacrfsdscscdsscsdcd"
        };
        var validationResult = await validator.ValidateAsync(command);

        //Assert
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().HaveCount(0);
    }

    [Test]
    public async Task AddCategory_ShouldCreateCategory()
    {
        //Mock Database
        _applicationDbContextMock.Categories.AddRange(new List<Categories>() {
                        new Categories { Id = 1, Name = "Customers" },
                        new Categories { Id = 2, Name = "Employees" }});
        _applicationDbContextMock.SaveChanges();
        _dbContextMock.Setup(m => m.Categories).Returns(_applicationDbContextMock.Categories);

        //Arrange
        var validator = new CreateCategoryCommandValidator(_dbContextMock.Object);
        var command = new CreateCategoryCommand
        {
            Name = "Accessories",
        };
        var _loggerMock = new Mock<ILogger<CreateCategoryCommandHandler>>();
        var commandHandler = new CreateCategoryCommandHandler(_dbContextMock.Object, _loggerMock.Object);

        //Act
        var validationResult = await validator.ValidateAsync(command);
        var productCategory = await commandHandler.Handle(command, new CancellationToken());
        _applicationDbContextMock.SaveChanges();

        //Assert
        var logger = new Mock<ILogger<GetAllCategoriesQueryHandler>>();
        var queryHandler = new GetAllCategoriesQueryHandler(_dbContextMock.Object, _mapper, logger.Object);
        var productCategories = await queryHandler.Handle(new GetAllCategoriesQuery(), new CancellationToken());
        var addedProductCategory = productCategories.LastOrDefault(pc => pc.Name == command.Name);
        ClassicAssert.IsNotNull(addedProductCategory, "The added product category should exist in the list of all product categories.");
        ClassicAssert.AreEqual(command.Name, addedProductCategory?.Name, "The name of the added product category should match the provided name.");
    }

    [Test]
    public async Task AddCategory_WithNullValue()
    {
        //Mock Database
        _applicationDbContextMock.Categories.AddRange(new List<Categories>() {
                        new Categories { Id = 1, Name = "Customers" },
                        new Categories { Id = 2, Name = "Employees" }});
        _applicationDbContextMock.SaveChanges();
        _dbContextMock.Setup(m => m.Categories).Returns(_applicationDbContextMock.Categories);

        //Arrange
        var validator = new CreateCategoryCommandValidator(_dbContextMock.Object);
        var command = new CreateCategoryCommand
        {
            Name = "",
        };
        var _loggerMock = new Mock<ILogger<CreateCategoryCommandHandler>>();
        var commandHandler = new CreateCategoryCommandHandler(_dbContextMock.Object, _loggerMock.Object);

        //Act
        var validationResult = await validator.ValidateAsync(command);
        var productCategory = await commandHandler.Handle(command, new CancellationToken());
        _applicationDbContextMock.SaveChanges();

        //Assert
        var logger = new Mock<ILogger<GetAllCategoriesQueryHandler>>();
        var queryHandler = new GetAllCategoriesQueryHandler(_dbContextMock.Object, _mapper, logger.Object);
        var productCategories = await queryHandler.Handle(new GetAllCategoriesQuery(), new CancellationToken());
        var addedProductCategory = productCategories.LastOrDefault(pc => pc.Name == command.Name);
        ClassicAssert.IsNotNull(addedProductCategory, "The added product category should exist in the list of all product categories.");
        ClassicAssert.AreEqual(command.Name, addedProductCategory?.Name, "The name of the added product category should match the provided name.");
    }
}
