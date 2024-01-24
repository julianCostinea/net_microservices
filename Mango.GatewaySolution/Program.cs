using Mango.GatewaySolution.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppAuthetication();

builder.Services.AddOcelot();


var app = builder.Build();

app.MapGet("/", () => "Hello World!");
var configuration = new OcelotPipelineConfiguration
{
    AuthenticationMiddleware = async (cpt, est) =>
    {
        await est.Invoke();
    }
};
app.UseOcelot(configuration).Wait();
app.Run();