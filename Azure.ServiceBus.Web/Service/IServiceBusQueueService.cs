using Azure.ServiceBus.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.ServiceBus.Web.Service
{
    public interface IServiceBusQueueService
    {
        Task SendMessageAsync(Message msg);
    }
}
