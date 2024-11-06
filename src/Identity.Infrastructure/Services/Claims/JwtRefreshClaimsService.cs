﻿using Identity.Application.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services.Claims
{
    public class JwtRefreshClaimsService : IClaimsService<JwtRefreshTokenDto>
    {
        public IEnumerable<Claim> Create(JwtRefreshTokenDto details)
        {
            Claim jti = new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            Claim iat = new(JwtRegisteredClaimNames.Iat,
                EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture),
                ClaimValueTypes.Integer64);
            Claim sessionId = new(ClaimTypes.Sid, details.Id);

            List<Claim> claims =
            [
                jti,
                iat,
                sessionId
            ];

            return claims;
        }
    }
}
