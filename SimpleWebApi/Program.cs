using System.Collections.Concurrent;
using System.IO;
using System.Text.Json;
using System.Xml.XPath;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using SimpleWebApi.Database;
using SimpleWebApi.Domain.BicycleDomain.Entities;
using SimpleWebApi.Domain.CarDomain.Entities;
using SimpleWebApi.Dto;
using SimpleWebApi.Executor;
using SimpleWebApi.Infrastructure.DomainInfra.DbContextBase;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;
using SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Implementations;
using SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Interfaces;
using SimpleWebApi.Infrastructure.Exceptions;
using SimpleWebApi.Infrastructure.Exceptions.Base;
using SimpleWebApi.Infrastructure.Mapper;
using SimpleWebApi.Mapper;
using SimpleWebApi.Requests.Bicycles;
using SimpleWebApi.Requests.Cars;
using SimpleWebApi.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(o => o.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddScoped<CommandExecutor>();
builder.Services.AddAutoMapper(o =>
{
    o.CreateMap<Bicycle, BicycleDto>();
    o.CreateMap<Car, CarDto>(); 
    o.CreateMap<AddBicycleCommand, Bicycle>();
    o.CreateMap<AddCarCommand, Car>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.DocumentFilter<CustomModelDocumentFilter>());
SwaggerExtension.InitTypes(typeof(Program));
builder.Services.AddScoped<ICustomMapper, CustomMapper.AutoMapper>();
builder.Services.AddScoped(typeof(IFullRepository<,>), typeof(FullRepository<,>));
builder.Services.AddScoped(typeof(IReadRepository<,>), typeof(ReadRepository<,>));
builder.Services.AddScoped(typeof(IWriteRepository<,>), typeof(WriteRepository<,>));

var connectionStringBuilder = builder.Configuration.GetSection("Connection").Get<SqlConnectionStringBuilder>();
builder.Services.AddDbContext<IDbContext,AppDbContext>(o =>
{
    o.UseSqlServer(connectionStringBuilder?.ToString());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
        {
            swaggerDoc.FillPaths();
        });
    });
    app.UseSwaggerUI(x=>x.DocExpansion(DocExpansion.None));
}

app.MapPost("Commands/{commandName}",
    async (string commandName, JsonElement request, HttpRequest httpRequest, HttpResponse response, CommandExecutor customPipeline) =>
    {
        try
        {
            return await customPipeline.Execute(commandName, request, httpRequest);
        }
        catch (Exception e)
        {
            IException exception = e as IException ?? new UnexpectedException();
            response.StatusCode = exception.GetStatsCode();
            return exception.GetMessage();
        }
    }
);

app.Run();
