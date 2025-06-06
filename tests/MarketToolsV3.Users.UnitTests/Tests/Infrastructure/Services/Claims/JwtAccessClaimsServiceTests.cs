﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Models;
using Identity.Infrastructure.Services.Abstract;
using Identity.Infrastructure.Services.Implementation.Claims;
using Moq;

namespace MarketToolsV3.Users.UnitTests.Tests.Infrastructure.Services.Claims
{
    internal class JwtAccessClaimsServiceTests
    {
        private Mock<IRolesClaimService> _rolesClaimServiceMock;

        [SetUp]
        public void Setup()
        {
            _rolesClaimServiceMock = new Mock<IRolesClaimService>();
        }

        [Test]
        public void Create_ContainsJti()
        {
            JwtAccessClaimsService service = new(_rolesClaimServiceMock.Object);

            JwtAccessTokenDto accessToken = new()
            {
                UserId = "",
                Id = "",
                SessionId = ""
            };

            IEnumerable<string> claimTypes = service
                .Create(accessToken)
                .Select(x=> x.Type)
                .ToList();

            Assert.That(claimTypes, Does.Contain(JwtRegisteredClaimNames.Jti));
        }

        [Test]
        public void Create_ContainsIat()
        {
            JwtAccessClaimsService service = new(_rolesClaimServiceMock.Object);

            JwtAccessTokenDto accessToken = new()
            {
                UserId = "",
                Id = "",
                SessionId = ""
            };

            IEnumerable<string> claimTypes = service
                .Create(accessToken)
                .Select(x => x.Type)
                .ToList();

            Assert.That(claimTypes, Does.Contain(JwtRegisteredClaimNames.Iat));
        }

        [Test]
        public void Create_ContainsUserId()
        {
            JwtAccessClaimsService service = new(_rolesClaimServiceMock.Object);

            JwtAccessTokenDto accessToken = new()
            {
                UserId = "",
                Id = "",
                SessionId = ""
            };

            IEnumerable<string> claimTypes = service
                .Create(accessToken)
                .Select(x => x.Type)
                .ToList();

            Assert.That(claimTypes, Does.Contain(ClaimTypes.NameIdentifier));
        }
    }
}