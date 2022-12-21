using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Sales
{
    public static class Endpoints
    {
        public static void MapShipping(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPut("/shipping/{id}", async context =>{
                var id = context.GetRouteData().Values["id"] ?? "";

                await context.Response.WriteAsync($"Order has been placed. {id}");
            }
            );
            endpoints.MapGet("/shipping", () => { return "Hello Shipping"; });
        }
    }
}