using Azure.ServiceBus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceMessage = Azure.ServiceBus;

namespace Azure.ServiceBus.Service
{
    public interface IServiceBusQueueService
    {
       Task SendMessageAsync(ServiceMessage.Model.Message message);
    }
}
