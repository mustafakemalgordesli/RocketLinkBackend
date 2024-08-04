using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using RocketLink.Application.DTOs;
using RocketLink.Application.Features.Auth.Login;
using RocketLink.Application.Interfaces;
using RocketLink.Domain.Entities;
using RocketLink.Persistence.Contexts;


namespace RocketLink.Application.UnitTests.Auth;

[TestFixture]
public class LoginCommandHandlerTests
{
    private IApplicationDbContext _context;
    private IRequestHandler<LoginCommand, Domain.Common.Result<AuthResponse>> _handler;
    private IJwtService _jwtService;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<RocketLinkDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new RocketLinkDbContext(options);

        _jwtService = new Mock<IJwtService>().Object;

        _handler = new LoginCommandHandler(_context, _jwtService);
    }

    [Test]
    public async Task Handle_UserNotFound_ShouldReturnFailure()
    {
        // Arrange
        var command = new LoginCommand { Email = "test@example.com", Password = "password" };

        // Act       
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("User.NotFound", result.Error.Code);
    }

    [Test]
    public async Task Handle_ShouldReturnSuccess_WhenUsernameAndPasswordAreCorrect()
    {
        // Arrange

        var user = new User
        {
            Email = "test@example.com",
            Username = "test",
            Password = BCrypt.Net.BCrypt.HashPassword("correctpassword")
        };

        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new LoginCommand
        {
            Email = "test",
            Password = "correctpassword"
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        await ClearDb(_context);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(command.Email, result.Value.User.Username);
    }

    [Test]
    public async Task Handle_ShouldReturnSuccess_WhenEmailAndPasswordAreCorrect()
    {
        // Arrange

        var user = new User
        {
            Email = "test@example.com",
            Username = "test",
            Password = BCrypt.Net.BCrypt.HashPassword("correctpassword")
        };

        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new LoginCommand
        {
            Email = "test@example.com",
            Password = "correctpassword"
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        await ClearDb(_context);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(command.Email, result.Value.User.Email);
    }

    [Test]
    public async Task Handle_ShouldReturnFailure_WhenPasswordIsWrong()
    {
        // Arrange
        var user = new User
        {
            Email = "test@example.com",
            Username = "test",
            Password = BCrypt.Net.BCrypt.HashPassword("correctpassword")
        };

        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new LoginCommand
        {
            Email = "test@example.com",
            Password = "wrongpassword"
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        await ClearDb(_context);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("User.PasswordWrong", result.Error.Code);
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