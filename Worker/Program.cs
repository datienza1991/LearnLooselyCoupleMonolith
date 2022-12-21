
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sales;
using Shipping;
using Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSales();
        services.AddShipping();
        services.AddHostedService<MessageProcessor>();

        services.AddCap(options =>
        {
            options.UseInMemoryStorage();
            options.UseRabbitMQ("localhost");
        });
    })
    .Build();

host.Run();