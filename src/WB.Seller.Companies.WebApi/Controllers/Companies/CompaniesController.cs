﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WB.Seller.Companies.Application.Commands;
using WB.Seller.Companies.Application.Queries;
using WB.Seller.Companies.WebApi.Extensions;
using WB.Seller.Companies.WebApi.Models.Companies;

namespace WB.Seller.Companies.WebApi.Controllers.Companies
{
    [Route("api/v{version:apiVersion}/companies")]
    [ApiController]
    [ApiVersion("1")]
    public class CompaniesController(IMediator mediator)
        : ControllerBase
    {
        [HttpGet("slim")]
        [MapToApiVersion("1")]
        public async Task<IActionResult> CreateAsync(CancellationToken cancellationToken)
        {
            GetSlimCompaniesQuery query = new()
            {
                UserId = "2b90e7de-1862-4ab2-9fce-015d1ec20e71"
            };

            var result = await mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
