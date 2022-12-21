using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Sales.Contracts;

namespace Shipping
{
    public class CreateShippingLabel : ICapSubscribe
    {
        private readonly ILogger<CreateShippingLabel> logger;

        public CreateShippingLabel(ILogger<CreateShippingLabel> logger)
        {
            this.logger = logger;
        }

        [CapSubscribe(nameof(OrderPlaced))]
        public void Handle(OrderPlaced orderPlaced)
        {
            logger.LogInformation($"Order {orderPlaced.OrderId} has been created a shipping label");
        }
    }
}