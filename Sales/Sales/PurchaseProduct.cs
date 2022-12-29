using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Sales
{
    public class PurchaseProduct : IRequest
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

            return NoContent();
        }
    }

    public class PurchaseProductHandler : IRequestHandler<PurchaseProduct>
    {
        private readonly ILogger<PurchaseProductHandler> logger;

        public PurchaseProductHandler(ILogger<PurchaseProductHandler> logger)
        {
            this.logger = logger;
        }

        public async Task<Unit> Handle(PurchaseProduct request, CancellationToken cancellationToken)
        {
            this.logger.LogInformation($"PurchaseProductID is {request.ProductId.ToString()}");
            await Task.CompletedTask;
            return Unit.Value;
        }
    }
}