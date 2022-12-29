using DotNetCore.CAP;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Sales.Contracts;

namespace Sales
{
    public static class Endpoints
    {
        public static void MapSales(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapViewSaleProductPriceEndpoint();
            endpoints.MapExceptionEndpoint();

            endpoints.MapPost("/sales/orders/{id}", async (int id, MyBody mybody, HttpContext context, IMediator mediator, [FromServices]ICapPublisher publisher) =>
            {
                var result = await mediator.Send(new PurchaseProduct
                {
                    ProductId = Guid.NewGuid()
                });

                await publisher.PublishAsync(nameof(OrderPlaced), new OrderPlaced()
                {
                    OrderId = Guid.NewGuid()
                });

                return Results.Ok("Order has been placed");
            });
        }
    }

    internal static class ViewSaleProductPriceEndpoint
    {
        public static void MapViewSaleProductPriceEndpoint(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/sales/product/{id}", async (int id, HttpContext context) =>
            {
                var randomPrice = new Random().Next(1, 100);
                await context.Response.WriteAsync($"Product with {id} has a price of {randomPrice}");
            });
        }
    }

    internal static class ExceptionEndpoint
    {
        public static void MapExceptionEndpoint(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/sales/error", () =>
            {
                Results.BadRequest("Oops, the '/' route has thrown an exception.");
            });
        }
    }

    public record MyBody(int id);
}