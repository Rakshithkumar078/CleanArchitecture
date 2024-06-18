using CleanArchitecture.Application.Common.Exceptions;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace CleanArchitecture.Application.UnitTests.Common.Exceptions;
[TestFixture]
public class NotFoundExceptionsTest : BaseTestFixture
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
    public void NotFoundException_DefaultConstructor_ShouldCreateInstance()
    {
        // Act
        var exception = new NotFoundException();

        // Assert
        ClassicAssert.IsNotNull(exception);
    }

    [Test]
    public void NotFoundException_MessageConstructor_ShouldCreateInstanceWithMessage()
    {
        // Arrange
        string message = "Custom error message";

        // Act
        var exception = new NotFoundException(message);

        // Assert
        ClassicAssert.IsNotNull(exception);
        ClassicAssert.AreEqual(message, exception.Message);
    }

    [Test]
    public void NotFoundException_MessageAndInnerExceptionConstructor_ShouldCreateInstanceWithMessageAndInnerException()
    {
        // Arrange
        string message = "Custom error message";
        var innerException = new Exception("Inner exception");

        // Act
        var exception = new NotFoundException(message, innerException);

        // Assert
        ClassicAssert.IsNotNull(exception);
        ClassicAssert.AreEqual(message, exception.Message);
        ClassicAssert.AreSame(innerException, exception.InnerException);
    }

    [Test]
    public void NotFoundException_NameAndKeyConstructor_ShouldCreateInstanceWithFormattedMessage()
    {
        // Arrange
        string name = "Contents";
        object key = 123;

        // Act
        var exception = new NotFoundException(name, key);

        // Assert
        ClassicAssert.IsNotNull(exception);
        ClassicAssert.AreEqual($"Entity \"{name}\" ({key}) was not found.", exception.Message);
    }
}
