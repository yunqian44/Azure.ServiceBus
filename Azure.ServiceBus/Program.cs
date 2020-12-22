using System;
using System.Text.Json;
using System.Threading.Tasks;
using ServiceMessage = Azure.ServiceBus;
using System.Text;
using Azure.ServiceBus.Common;
using System.Collections.Generic;
using Azure.Messaging.ServiceBus;

namespace Azure.ServiceBus
{
    class Program
    {
        private ServiceBusClient queueClient;
        public static async Task Main(string[] args)
        {
            Program p = new Program();

            //await p.SendMessageAsync(new Model.Message() { Id=2,Content= "This is second queue message", CreateTime=DateTime.Now,Title="TextQueue"});

            await p.ReceivedMessageAsync();
            //await p.DeleteMessagesAsync();
            await p.PeekMessageAsync();
            Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// Send Message
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public async Task SendMessageAsync(Model.Message msg)
        {
            var a = new Appsettings();
            await using var queueClient = new ServiceBusClient(Appsettings.app("ServiceBus", "PrimaryConnectionString"));
            try
            {
                // create the sender
                ServiceBusSender sender = queueClient.CreateSender(Appsettings.app("ServiceBus", "QueueName"));
                string messageBody = JsonSerializer.Serialize(msg);
                // create a message that we can send. UTF-8 encoding is used when providing a string.
                ServiceBusMessage message = new ServiceBusMessage(messageBody);

                // send the message
                await sender.SendMessageAsync(message);
                Console.WriteLine($"Sending message: {messageBody} success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Received Message
        /// </summary>
        /// <returns></returns>
        public async Task ReceivedMessageAsync()
        {
            var a = new Appsettings();
            await using var queueClient = new ServiceBusClient(Appsettings.app("ServiceBus", "PrimaryConnectionString"));
            try
            {
                // create a receiver that we can use to receive the message
                ServiceBusReceiver receiver = queueClient.CreateReceiver(Appsettings.app("ServiceBus", "QueueName"));

                // the received message is a different type as it contains some service set properties
                ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();

                // get the message body as a string
                string body = receivedMessage.Body.ToString();
                Console.WriteLine(body);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Delete Messages
        /// </summary>
        /// <returns></returns>
        public async Task DeleteMessagesAsync()
        {
            var a = new Appsettings();
            await using var queueClient = new ServiceBusClient(Appsettings.app("ServiceBus", "PrimaryConnectionString"));
            try
            {
                // create a receiver that we can use to receive the message
                ServiceBusReceiver receiver = queueClient.CreateReceiver(Appsettings.app("ServiceBus", "QueueName"));
                ServiceBusReceivedMessage peekedMessage = await receiver.ReceiveMessageAsync();

                await receiver.CompleteMessageAsync(peekedMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Peek Message
        /// </summary>
        /// <returns></returns>
        public async Task PeekMessageAsync()
        {
            var a = new Appsettings();
            await using var queueClient = new ServiceBusClient(Appsettings.app("ServiceBus", "PrimaryConnectionString"));
            try
            {
                // create a receiver that we can use to receive the message
                ServiceBusReceiver receiver = queueClient.CreateReceiver(Appsettings.app("ServiceBus", "QueueName"));
                ServiceBusReceivedMessage peekedMessage = await receiver.PeekMessageAsync();

                // get the message body as a string
                string body = peekedMessage.Body.ToString();
                Console.WriteLine(body);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
