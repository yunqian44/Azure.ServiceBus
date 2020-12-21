using Azure.ServiceBus.Model;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceMessage = Azure.ServiceBus;

namespace Azure.ServiceBus.Service
{
    public class ServiceBusQueueService : IServiceBusQueueService
    {
        private readonly IQueueClient _queueClient;

        public ServiceBusQueueService(IQueueClient queueClient)
        {
            this._queueClient = queueClient;
        }
        public Task SendMessageAsync(ServiceMessage.Model.Message message)
        {
            throw new NotImplementedException();
        }
    }
}
