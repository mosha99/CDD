using MediatR;
using SimpleWebApi.Domain.CarDomain.Entities;
using SimpleWebApi.Dto;
using SimpleWebApi.Filters;
using SimpleWebApi.Infrastructure.Request.Querys;
using SimpleWebApi.Infrastructure.Response;

namespace SimpleWebApi.Requests.Cars;

public class GetAllCarQuery : BaseListQuery<Car, CarFilter>, IRequest<ListResult<CarDto>>
{

}