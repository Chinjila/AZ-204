using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;

namespace QueueConsumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = "Endpoint=sb://joebiden.servicebus.windows.net/;SharedAccessKeyName=samplesendlisten;SharedAccessKey=CN0JqvMw2uD/Nhflxk0OSuy9wSWRX2RJrWqwknzwtNo=";
            string queueName = "barbie";
            await using var client = new ServiceBusClient(connectionString);
            ServiceBusReceiver receiver = client.CreateReceiver(queueName);
            //ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();
            //string body = receivedMessage.Body.ToString();
            //Console.WriteLine(body);
            //await receiver.CompleteMessageAsync(receivedMessage);

            var options = new ServiceBusProcessorOptions
            {
                // By default or when AutoCompleteMessages is set to true, the processor will complete the message after executing the message handler
                // Set AutoCompleteMessages to false to [settle messages](https://docs.microsoft.com/en-us/azure/service-bus-messaging/message-transfers-locks-settlement#peeklock) on your own.
                // In both cases, if the message handler throws an exception without settling the message, the processor will abandon the message.
                AutoCompleteMessages = false,

                // I can also allow for multi-threading
                MaxConcurrentCalls = 2
            };
            await using ServiceBusProcessor processor = client.CreateProcessor(queueName, options);
            // configure the message and error handler to use
            processor.ProcessMessageAsync += MessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;
            async Task MessageHandler(ProcessMessageEventArgs args)
            {
                string body = args.Message.Body.ToString();
                Console.WriteLine(body);

                // we can evaluate application logic and use that to determine how to settle the message.
                await args.CompleteMessageAsync(args.Message);
            }
            Task ErrorHandler(ProcessErrorEventArgs args)
            {
                // the error source tells me at what point in the processing an error occurred
                Console.WriteLine(args.ErrorSource);
                // the fully qualified namespace is available
                Console.WriteLine(args.FullyQualifiedNamespace);
                // as well as the entity path
                Console.WriteLine(args.EntityPath);
                Console.WriteLine(args.Exception.ToString());
                return Task.CompletedTask;
            }
            
            await processor.StartProcessingAsync();
            
            Console.ReadLine();
        }
    }
}
