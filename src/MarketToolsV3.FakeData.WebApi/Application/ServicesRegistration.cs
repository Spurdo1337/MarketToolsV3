﻿using MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers;
using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Application.Services.Implementation;

namespace MarketToolsV3.FakeData.WebApi.Application
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<INotificationHandler<TimeoutTaskDetailsNotification>, TimeoutTaskDetailsHandler>();
            serviceCollection.AddScoped<INotificationHandler<FakeDataTaskNotification>, FakeDataTaskHandler>();
            serviceCollection.AddScoped<INotificationHandler<FakeDataTasksFailNotification>, FakeDataTasksFailHandler>();

            serviceCollection.AddScoped<IFakeDataTaskService, FakeDataTaskService>();
            serviceCollection.AddScoped<IFakeDataTaskMapService, FakeDataTaskMapService>();

            serviceCollection.AddSingleton(typeof(IPublisher<>), typeof(Publisher<>));
            serviceCollection.AddSingleton(typeof(ISubscriber<>), typeof(Subscriber<>));

            return serviceCollection;
        }
    }
}
