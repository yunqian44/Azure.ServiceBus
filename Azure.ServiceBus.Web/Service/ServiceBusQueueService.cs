using Azure.ServiceBus.Web.Models;
using AzureServiceBus= Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using Microsoft.Azure.ServiceBus.Core;

namespace Azure.ServiceBus.Web.Service
{
    public class ServiceBusQueueService : IServiceBusQueueService
    {
        private readonly AzureServiceBus.IQueueClient _queueClient;

        public ServiceBusQueueService(AzureServiceBus.QueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        public async Task SendMessageAsync(Message msg)
        {
            try
            {
                // Serialize data model and create message.
                string messageBody = JsonSerializer.Serialize(msg);
                var message = new AzureServiceBus.Message(Encoding.UTF8.GetBytes(messageBody));

                // Send the message to the queue.
                await _queueClient.SendAsync(message);
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
