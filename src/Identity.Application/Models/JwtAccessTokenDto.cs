﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Models
{
    public record JwtAccessTokenDto : IToken
    {
        public required string UserId { get; init; }
        public required string SessionId { get ; init; }
        public List<string> Roles { get; } = [];
    }
}
