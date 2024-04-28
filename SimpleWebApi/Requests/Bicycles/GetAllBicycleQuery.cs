using MediatR;
using SimpleWebApi.Domain.BicycleDomain.Entities;
using SimpleWebApi.Dto;
using SimpleWebApi.Filters;
using SimpleWebApi.Infrastructure.Request.Querys;
using SimpleWebApi.Infrastructure.Response;

namespace SimpleWebApi.Requests.Bicycles;

public class GetAllBicycleQuery : BaseListQuery<Bicycle, BicycleFilter>, IRequest<ListResult<BicycleDto>>
{

}