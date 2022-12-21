using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Sales
{
    public class PurchaseProduct : IRequest<string>
    {
        public Guid ProductId { get; set; }
    }

    public class PurchaseProductController : Controller
    {
        private readonly IMediator mediator;

        public PurchaseProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("/sales/products/{productId:Guid}")]
        public async Task<IActionResult> Action([FromRoute] Guid productId)
        {
            var result = await this.mediator.Send(new PurchaseProduct
            {
                ProductId = productId
            });

            return Ok(result);
        }
    }

    public class PurchaseProductHandler : IRequestHandler<PurchaseProduct, string>
    {
        private readonly ILogger<PurchaseProductHandler> logger;

        public PurchaseProductHandler(ILogger<PurchaseProductHandler> logger)
        {
            this.logger = logger;
        }

        public Task<string> Handle(PurchaseProduct request, CancellationToken cancellationToken)
        {
            this.logger.LogInformation($"PurchaseProductID is {request.ProductId.ToString()}");
            return Task.FromResult(request.ProductId.ToString());
        }
    }
}