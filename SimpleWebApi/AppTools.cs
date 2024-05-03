using System.Reflection;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SimpleWebApi.Database;
using SimpleWebApi.Extensions;
using SimpleWebApi.Infrastructure.Attributes;
using SimpleWebApi.Infrastructure.DomainInfra.DbContextBase;
using SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Implementations;
using SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Interfaces;
using SimpleWebApi.Infrastructure.Mapper;
using SimpleWebApi.Infrastructure.MediatR;
using SimpleWebApi.Infrastructure.Validations;
using SimpleWebApi.Mapper;
using SimpleWebApi.Swagger;
using SimpleWebApi.Validator;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace SimpleWebApi;

public static class AppTools
{
    public static void RegisterThirdPartyPackages(this WebApplicationBuilder webApplicationBuilder, params Assembly[] assemblies)
    {
        webApplicationBuilder.Services.RegisterAggregateHandlers(assemblies);
        webApplicationBuilder.Services.AddMediatR(o =>
        {
            o.RegisterServicesFromAssemblies(assemblies);
            o.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        webApplicationBuilder.Services.AddAutoMapper(o =>
        {
            o.AddProfile<AutoMapperProfile>();
            //o.RegisterByAttribute(assemblies);
        });
        webApplicationBuilder.Services.AddEndpointsApiExplorer();
        webApplicationBuilder.Services.AddScoped<ICustomMapper, CustomMapper.AutoMapper>();
        webApplicationBuilder.Services.AddScoped(typeof(IEntityValidation<>), typeof(EntityValidation.FluentValidation<>));
    }
    public static void RegisterByAttribute(this IMapperConfigurationExpression configurationExpression, params Assembly[] assemblies)
    {
       var types = assemblies
            .SelectMany(x => x.GetTypes().Where(x => x.GetCustomAttribute<ConvertAbleToAttribute>() != null)).ToList();

       foreach (var type in types)
       {
           var target = type.GetCustomAttribute<ConvertAbleToAttribute>()!.Target;
           configurationExpression.CreateMap(type, target);
       }
    }
    public static void RegisterSwagger(this WebApplicationBuilder builder, params Assembly[] assemblies)
    {
        SwaggerExtension.InitTypes(assemblies);
        builder.Services.AddSwaggerGen(o =>
        {
            o.DocumentFilter<CustomModelDocumentFilter>();
            o.SchemaFilter<SwaggerExcludeFilter>();
        });
    }
    public static void RegisterDbAndRepositories(this WebApplicationBuilder builder, string connectionStringSection)
    {
        var connectionStringBuilder = builder.Configuration.GetSection(connectionStringSection).Get<SqlConnectionStringBuilder>();

        builder.Services.AddDbContext<IDbContext, AppDbContext>(o =>
        {
            o.UseSqlServer(connectionStringBuilder?.ToString());
        });

        builder.Services.AddScoped<IReadDbContext, IDbContext>(serviceProvider => serviceProvider.GetService<IDbContext>()!);
        builder.Services.AddScoped<IWriteDbContext, IDbContext>(serviceProvider => serviceProvider.GetService<IDbContext>()!);

        builder.Services.AddScoped(typeof(IFullRepository<,>), typeof(FullRepository<,>));
        builder.Services.AddScoped(typeof(IReadRepository<,>), typeof(ReadRepository<,>));
        builder.Services.AddScoped(typeof(IWriteRepository<,>), typeof(WriteRepository<,>));
    }
    public static void RegisterSwaggerUi(this WebApplication webApplication)
    {
        if (webApplication.Environment.IsDevelopment())
        {
            webApplication.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.FillPaths();
                });
            });
            webApplication.UseSwaggerUI(x => x.DocExpansion(DocExpansion.None));
        }
    }
}
