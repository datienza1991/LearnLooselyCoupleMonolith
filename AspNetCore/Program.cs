using MediatR;
using Sales;
using Shipping;

var builder = WebApplication.CreateBuilder(args);
//Register Services
builder.Services.AddSales();
builder.Services.AddShipping();
builder.Services.AddControllers();
builder.Services.AddCap(options => {
    options.UseInMemoryStorage();
    options.UseRabbitMQ("localhost");
    options.UseDashboard();
    options.ConsumerThreadCount = 0;
});
var app = builder.Build();
app.UseRouting();
//Register Endpoints
app.UseEndpoints(e => e.MapSales());
app.UseEndpoints(e => e.MapShipping());
app.UseEndpoints(e => e.MapControllers());
app.Run();
