using SimpleWebApi;
using SimpleWebApi.Executor;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterThirdPartyPackages(typeof(Program).Assembly);

builder.Services.AddScoped<CommandExecutor>();

builder.RegisterSwagger(typeof(Program).Assembly);

builder.RegisterDbAndRepositories("Connection");

var app = builder.Build();

app.RegisterSwaggerUi();

app.MapWhen(RoutMapTools.IsValidGetRequest, a =>
{
    a.Run(RoutMapTools.RequestHandler);
});

app.Run();