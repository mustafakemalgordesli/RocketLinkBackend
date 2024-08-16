using MediatR;
using Microsoft.EntityFrameworkCore;
using RocketLink.Application.Features.Users.Queries.CheckEmailInUse;
using RocketLink.Application.Interfaces;
using RocketLink.Persistence.Contexts;

namespace RocketLink.Application.UnitTests.User;

[TestFixture]
public class CheckUsernameInUseQueryTests
{
    private IApplicationDbContext _context;
    private IRequestHandler<CheckEmailInUseQuery, Domain.Common.Result> _handler;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<RocketLinkDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new RocketLinkDbContext(options);

        _context.Users.Add(new Domain.Entities.User { Email = "test@example.com", Username = "testuser", Password = "testpassword" });

        _context.SaveChanges();

        _handler = new CheckUsernameInUseQueryHandler(_context);
    }

    [Test]
    public async Task Handle_ShouldReturnExistError_WhenEmailIsExist()
    {
        // Arrange
        var query = new CheckEmailInUseQuery("test@example.com");

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("email", result.Error.Code);
    }

    [Test]
    public async Task Handle_ShouldReturnSuccess_WhenEmailIsNotExist()
    {
        // Arrange
        var query = new CheckEmailInUseQuery("test2@example.com");

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
    }

    [TearDown]
    public void Cleanup()
    {
        _context.Dispose();
    }
}
