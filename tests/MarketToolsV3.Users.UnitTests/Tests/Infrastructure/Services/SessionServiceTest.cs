﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Services;
using Identity.Domain.Entities;
using Identity.Domain.Events;
using Identity.Domain.Seed;
using Identity.Infrastructure.Database;
using Identity.Infrastructure.Services;
using MarketToolsV3.Users.UnitTests.Mock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;

namespace MarketToolsV3.Users.UnitTests.Tests.Infrastructure.Services
{
    internal class SessionServiceTest
    {
        private Mock<IRepository<Session>> _sessionRepositoryMock;
        private Mock<IOptions<ServiceConfiguration>> _serviceConfigurationMock;
        private Mock<IEventRepository> _eventRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _sessionRepositoryMock = new();
            _sessionRepositoryMock.Setup(x => x.UnitOfWork).Returns(_unitOfWorkMock.Object);
            _eventRepositoryMock = new();
            _serviceConfigurationMock = new();
        }

        [Test]
        public async Task AddAsync_ExpectedCallAddRepositoryMethod()
        {
            Session session = new Session(It.IsNotNull<string>(), It.IsNotNull<string>());

            SessionService sessionService = new SessionService(
                _sessionRepositoryMock.Object,
                _serviceConfigurationMock.Object,
                _eventRepositoryMock.Object);

            await sessionService.AddAsync(session);

            _sessionRepositoryMock.Verify(x=> 
                x.AddAsync(It.Is<Session>(s=> s == session), 
                    It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task AddAsync_ExpectedCallSaveEntitiesAsync()
        {
            Session session = new Session(It.IsNotNull<string>(), It.IsNotNull<string>());

            SessionService sessionService = new SessionService(
                _sessionRepositoryMock.Object,
                _serviceConfigurationMock.Object,
                _eventRepositoryMock.Object);

            await sessionService.AddAsync(session);

            _sessionRepositoryMock.Verify(x=> 
                x.UnitOfWork.SaveEntitiesAsync(
                    It.IsAny<CancellationToken>()), 
                Times.Once);
        }

        [Test]
        public async Task AddAsync_ExpectedCallEventRepositoryAddNotification()
        {
            Session session = new Session(It.IsNotNull<string>(), It.IsNotNull<string>());

            SessionService sessionService = new SessionService(
                _sessionRepositoryMock.Object,
                _serviceConfigurationMock.Object,
                _eventRepositoryMock.Object);

            await sessionService.AddAsync(session);

            _eventRepositoryMock.Verify(x=> x.AddNotification(It.IsAny<SessionCreated>()), Times.Once);
        }

        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        public async Task UpdateAsync_ExpectedNewToken(string token)
        {
            Session session = new Session(It.IsAny<string>(), It.IsAny<string>());

            _sessionRepositoryMock.Setup(x => x.UnitOfWork.SaveChangesAsync(default));

            SessionService sessionService = new SessionService(
                _sessionRepositoryMock.Object,
                _serviceConfigurationMock.Object,
                _eventRepositoryMock.Object);

            await sessionService.UpdateAsync(session, token, It.IsAny<string>());

            Assert.That(token, Is.EqualTo(session.Token));
        }

        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        public async Task UpdateAsync_ExpectedNewAgent(string agent)
        {
            Session session = new Session(It.IsAny<string>(), It.IsAny<string>());

            _sessionRepositoryMock.Setup(x => x.UnitOfWork.SaveChangesAsync(default));

            SessionService sessionService = new SessionService(
                _sessionRepositoryMock.Object,
                _serviceConfigurationMock.Object,
                _eventRepositoryMock.Object);

            await sessionService.UpdateAsync(session, It.IsAny<string>(), agent);

            Assert.That(agent, Is.EqualTo(session.UserAgent));
        }

        [Test]
        public async Task UpdateAsync_ExpectedNewUpdateDate()
        {
            DateTime firstUpdated = DateTime.UtcNow;
            Session session = new Session(It.IsAny<string>(), It.IsAny<string>())
            {
                Updated = firstUpdated
            };

            _sessionRepositoryMock.Setup(x => x.UnitOfWork.SaveChangesAsync(default));

            SessionService sessionService = new SessionService(
                _sessionRepositoryMock.Object,
                _serviceConfigurationMock.Object,
                _eventRepositoryMock.Object);

            await sessionService.UpdateAsync(session, It.IsAny<string>(), It.IsAny<string>());

            Assert.That(session.Updated, Is.GreaterThan(firstUpdated));
        }
    }
}
