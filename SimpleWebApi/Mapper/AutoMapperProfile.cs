using AutoMapper;
using SimpleWebApi.Domain.AirPlaneDomain;
using SimpleWebApi.Domain.CarDomain.Entities;
using SimpleWebApi.Dto;
using SimpleWebApi.Requests.AirPlane;
using SimpleWebApi.Requests.Cars;

namespace SimpleWebApi.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Car, CarDto>();
        CreateMap<AddCarCommand, Car>();
        CreateMap<AirPlane, AirPlaneDto>();
        CreateMap<AddAirPlaneCommand, AirPlane>();
        CreateMap<EditAirPlaneCommand, AirPlane>();
    }
}