
using Azure.Messaging.ServiceBus;

string connectionString = "Endpoint=sb://ksday4ns1.servicebus.windows.net/;SharedAccessKeyName=receiverPolicy;SharedAccessKey=Ic9yDupWSKJTnEojM/4fUJ8Xfl0AaAzo4+ASbGiws+M=;EntityPath=stocks";
string queueName = "stocks";
ServiceBusClient client = default!;
ServiceBusProcessor processor = default!;

client = new ServiceBusClient(connectionString);
processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

try
{
    processor.ProcessMessageAsync += MessageHandler;
    processor.ProcessErrorAsync += ErrorHandler;

    await processor.StartProcessingAsync();
    Console.WriteLine("Wait for a minute and then press any key to end the processing");
    Console.ReadKey();

    Console.WriteLine("\nStopping the receiver...");
    await processor.StopProcessingAsync();
    Console.WriteLine("Stopped receiving messages");
}
finally
{
    await processor.DisposeAsync();
    await client.DisposeAsync();
}
static async Task MessageHandler(ProcessMessageEventArgs args)
{
    string body = args.Message.Body.ToString();
    Console.WriteLine($"Received: {body}");
    await args.CompleteMessageAsync(args.Message);
}

static Task ErrorHandler(ProcessErrorEventArgs args)
{
    Console.WriteLine(args.Exception.ToString());
    return Task.CompletedTask;
}
