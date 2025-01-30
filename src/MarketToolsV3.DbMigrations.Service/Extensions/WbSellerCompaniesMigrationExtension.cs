﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Constants;
using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.DbMigrations.Service.Workers;
using WB.Seller.Companies.Domain.Constants;
using WB.Seller.Companies.Domain.Seed;
using WB.Seller.Companies.Infrastructure.Database;

namespace MarketToolsV3.DbMigrations.Service.Extensions
{
    internal static class WbSellerCompaniesMigrationExtension
    {
        public static async Task AddWbSellerCompaniesMigration(this IHostApplicationBuilder builder)
        {
            ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);

            ITypingConfigManager<ServiceConfiguration> serviceConfigManager =
                await configurationServiceFactory.CreateFromServiceAsync<ServiceConfiguration>(ServiceConstants.ServiceName);

            builder.Services.AddNpgsql<WbSellerCompaniesDbContext>(serviceConfigManager.Value.DatabaseConnection);

            builder.Services.AddHostedService<EfCoreMigrationBackgroundService<WbSellerCompaniesDbContext>>();
        }
    }
}
