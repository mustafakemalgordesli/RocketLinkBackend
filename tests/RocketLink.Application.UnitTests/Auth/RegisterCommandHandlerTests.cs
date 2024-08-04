using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using RocketLink.Application.DTOs;
using RocketLink.Application.Features.Auth.Register;
using RocketLink.Application.Interfaces;
using RocketLink.Persistence.Contexts;

namespace RocketLink.Application.UnitTests.Auth;

[TestFixture]
public class RegisterCommandHandlerTests
{
    private IApplicationDbContext _context;
    private IRequestHandler<RegisterCommand, Domain.Common.Result<AuthResponse>> _handler;
    private IJwtService _jwtService;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<RocketLinkDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new RocketLinkDbContext(options);

        _jwtService = new Mock<IJwtService>().Object;

        _handler = new RegisterCommandHandler(_context, _jwtService);
    }

    [Test]
    public async Task Handle_ShouldRegisterUser_WhenValidCommand()
    {
        // Arrange
        var command = new RegisterCommand
        {
            Username = "test",
            Email = "test@example.com",
            Password = "validpassword"
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        await ClearDb(_context);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.NotNull(result.Value);
    }

    [Test]
    public void Handle_ShouldReturnValidationError_WhenEmailIsInvalid()
    {
        // Arrange
        var command = new RegisterCommand
        {
            Username = "validuser",
            Email = "invalid-email",
            Password = "validpassword"
        };

        var validator = new RegisterCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        Assert.IsFalse(validationResult.IsValid);
    }

    [Test]
    public void Handle_ShouldReturnValidationError_WhenUsernameIsEmpty()
    {
        // Arrange
        var command = new RegisterCommand
        {
            Username = "",
            Email = "user@example.com",
            Password = "validpassword"
        };

        // Act
        var validator = new RegisterCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        Assert.IsFalse(validationResult.IsValid);
    }

    public static async Task ClearDb(IApplicationDbContext context)
    {

        foreach (var item in context.Users)
        {
            context.Users.Remove(item);
        }

        await context.SaveChangesAsync(CancellationToken.None);
    }

    [TearDown]
    public void Cleanup()
    {
        _context.Dispose();
    }
}
