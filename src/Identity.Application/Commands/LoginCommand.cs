﻿using Identity.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Commands
{
    public class LoginCommand : ICommand<LoginResponseDto>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string UserAgent { get; set; } = "Unknown";
    }
}
