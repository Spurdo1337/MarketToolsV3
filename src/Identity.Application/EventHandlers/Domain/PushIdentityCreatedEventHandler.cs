﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Events;
using IntegrationEvents.Contract.Identity;
using MarketToolsV3.IntegrationEventLogService.Services.Abstract;
using MassTransit;
using MediatR;

namespace Identity.Application.EventHandlers.Domain
{
    public class PushIdentityCreatedEventHandler(IPublishEndpoint publishEndpoint)
        : INotificationHandler<IdentityCreated>
    {
        public async Task Handle(IdentityCreated notification, CancellationToken cancellationToken)
        {
            IdentityCreatedIntegrationEvent integrationMessage = new()
            {
                IdentityId = notification.Identity.Id,
                Login = notification.Identity.UserName ?? "Unknown"
            };

            await publishEndpoint.Publish(integrationMessage, cancellationToken);
        }
    }
}
