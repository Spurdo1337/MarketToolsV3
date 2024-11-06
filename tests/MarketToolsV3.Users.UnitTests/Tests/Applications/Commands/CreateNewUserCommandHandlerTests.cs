﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Commands;
using Identity.Application.Models;
using Identity.Application.Services;
using Identity.Domain.Entities;
using MarketToolsV3.Users.UnitTests.Source;
using Microsoft.Extensions.Logging;
using Moq;

namespace MarketToolsV3.Users.UnitTests.Tests.Applications.Commands
{
    internal class CreateNewUserCommandHandlerTests
    {
        private Mock<IIdentityPersonService> _identityPersonServiceMock;
        private Mock<ISessionService> _sessionServiceMock;
        private Mock<ITokenService<JwtAccessTokenDto>> _jwtAccessTokenServiceMock;
        private Mock<ITokenService<JwtRefreshTokenDto>> _jwtRefreshTokenServiceMock;
        private ILogger<CreateNewUserCommandHandler> _logger;
        private CreateNewUserCommand _command;

        [SetUp]
        public void Setup()
        {
            _identityPersonServiceMock = new Mock<IIdentityPersonService>();
            _sessionServiceMock = new Mock<ISessionService>();
            _jwtAccessTokenServiceMock = new Mock<ITokenService<JwtAccessTokenDto>>();
            _jwtRefreshTokenServiceMock = new Mock<ITokenService<JwtRefreshTokenDto>>();
            _logger = Logging.Create<CreateNewUserCommandHandler>();
            _command = new CreateNewUserCommand
            {
                Email = "new-email",
                Login = "new-login",
                Password = "new-password",
                UserAgent = "user-agent"
            };
        }

        [Test]
        public async Task Handle_CallAddUser()
        {
            CreateNewUserCommandHandler commandHandler = new CreateNewUserCommandHandler(
                _identityPersonServiceMock.Object,
                _logger,
                _sessionServiceMock.Object,
                _jwtAccessTokenServiceMock.Object,
                _jwtRefreshTokenServiceMock.Object);

            AuthDetailsDto result = await commandHandler.Handle(_command, It.IsAny<CancellationToken>());

            _identityPersonServiceMock.Verify(x=> 
                x.AddAsync(It.IsAny<IdentityPerson>(), It.IsAny<string>()), 
                Times.Once);
        }
    }
}
