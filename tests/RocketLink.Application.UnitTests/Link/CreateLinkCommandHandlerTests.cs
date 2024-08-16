using MediatR;
using Microsoft.EntityFrameworkCore;
using RocketLink.Application.Features.Auth.Register;
using RocketLink.Application.Features.Links.Commands.CreateLink;
using RocketLink.Application.Interfaces;
using RocketLink.Persistence.Contexts;

namespace RocketLink.Application.UnitTests.Link;

[TestFixture]
public class CreateLinkCommandHandlerTests
{
    private IApplicationDbContext _context;
    private IRequestHandler<CreateLinkCommand, Domain.Common.Result<Guid>> _handler;
    private Domain.Entities.User user = new Domain.Entities.User() { Email = "test@example.com", Username = "testuser", Password = "testpassword" };

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<RocketLinkDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new RocketLinkDbContext(options);

        _context.Users.Add(user);

        _context.SaveChanges();

        _handler = new CreateLinkCommandHandler(_context);
    }

    [Test]
    public async Task Handle_ShouldReturnSuccessResult_WhenLinkIsCreated()
    {
        // Arrange
        var command = new CreateLinkCommand
        {
            Title = "Sample Link",
            UserId = user.Id,
            Url = "http://example.com",
            IconCode = "icon-sample"
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
    }


    [TearDown]
    public void Cleanup()
    {
        _context.Dispose();
    }
}
