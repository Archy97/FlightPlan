using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Module;

namespace FlightPlanner;

public class AutoMapperConfig
{
    public static IMapper CreateMapper()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<AirportRequest, Airport>()
                .ForMember(f => f.Id, opt => opt.Ignore())
                .ForMember(f => f.AirportCode, opt => opt
                    .MapFrom(f => f.Airport));
            cfg.CreateMap<Airport, AirportRequest>()
                .ForMember(f => f.Airport, opt => opt
                    .MapFrom(f => f.AirportCode));

            cfg.CreateMap<FlightRequest, Flights>();
            cfg.CreateMap<Flights, FlightRequest>();
        });

        configuration.AssertConfigurationIsValid();

        var mapper = configuration.CreateMapper();

        return mapper;
    }
}