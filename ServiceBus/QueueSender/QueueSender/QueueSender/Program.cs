using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;

namespace QueueSender
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = "Endpoint=sb://joebiden.servicebus.windows.net/;SharedAccessKeyName=samplesend;SharedAccessKey=UxLcqW7ntk4QkaTSh1GGMHGLEMI6A14QHirEJLGbkSo=";
            string queueName = "barbie";
            await using var client = new ServiceBusClient(connectionString);
            ServiceBusSender sender = client.CreateSender(queueName);
            ServiceBusMessage message = new ServiceBusMessage("Hello world! From Victor");
            for (int i = 0; i < 1000; i++)
            {
                await sender.SendMessageAsync(message);
            }
            
        }
    }
}
