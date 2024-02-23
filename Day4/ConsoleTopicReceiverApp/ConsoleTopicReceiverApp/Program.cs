using Azure.Messaging.ServiceBus;
using ConsoleTopicReceiverApp;
using Newtonsoft.Json;
using ConsoleTopicReceiverApp;


string connectionString = "Endpoint=sb://ksday4ns1.servicebus.windows.net/;SharedAccessKeyName=receiverPolicy;SharedAccessKey=7cEfKgolNuC8zNRRI9x1n4a9tdjbTrG/s+ASbNrjrXI=;EntityPath=orders";
string topicName = "orders";
string subscriptionName = "SubB";//change to ConsumerA,ConsumerB,ConsumerC

await ReceiveMessages();

async Task ReceiveMessages()
{
    ServiceBusClient serviceBusClient = new ServiceBusClient(connectionString);
    ServiceBusReceiver serviceBusReceiver = serviceBusClient.CreateReceiver(topicName, subscriptionName,
        new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete });

    IAsyncEnumerable<ServiceBusReceivedMessage> messages = serviceBusReceiver.ReceiveMessagesAsync();

    await foreach (ServiceBusReceivedMessage message in messages)
    {

        Stock order = JsonConvert.DeserializeObject<Stock>(message.Body.ToString());
        Console.WriteLine("Order Id {0}", order.OrderID);
        Console.WriteLine("Quantity {0}", order.Quantity);
        Console.WriteLine("Unit Price {0}", order.UnitPrice);
        Console.WriteLine();
        //await Console.Out.WriteLineAsync();

    }
}
