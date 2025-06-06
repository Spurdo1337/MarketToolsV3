﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Core.Serialization;
using Dapper;
using Npgsql;
using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Application.QueryData.Companies;
using WB.Seller.Companies.Domain.Enums;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Infrastructure.QueryDataHandlers.Companies
{
    internal class SlimCompanyRoleGroupsQueryDataHandler(IDbConnection dbConnection)
        : IQueryDataHandler<SlimCompanyRoleGroupsQueryData, IEnumerable<GroupDto<SubscriptionRole, CompanySlimInfoDto>>>
    {
        private readonly string _queryString = @$"select
                                                s.role as Role,
                                                jsonb_agg(jsonb_build_object('Id', c.id, 'Name', c.Name)) as Values
                                                from companies as c
                                                join subscriptions as s on s.company_id = c.id
                                                join users as u on u.sub_id = s.user_id
                                                where sub_id = @{nameof(SlimCompanyRoleGroupsQueryData.UserId)}
                                                group by s.role";
        public async Task<IEnumerable<GroupDto<SubscriptionRole, CompanySlimInfoDto>>> HandleAsync(SlimCompanyRoleGroupsQueryData queryData)
        {
            var response =
                await dbConnection.QueryAsync<(int Role, string Values)>(
                    _queryString, queryData);

            return response
                .Select(x => new GroupDto<SubscriptionRole, CompanySlimInfoDto>()
                {
                    Key = (SubscriptionRole)x.Role,
                    Values = JsonSerializer.Deserialize<IEnumerable<CompanySlimInfoDto>>(x.Values) ?? []
                });
        }
    }
}
