﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;

namespace UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition.NewFieldsData
{
    public interface INotificationNewFieldsData : INewFieldsData<Notification>
    {
        bool? IsRead { get; set; }
    }
}
