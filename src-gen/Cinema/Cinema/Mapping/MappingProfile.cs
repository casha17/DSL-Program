using AutoMapper;
using Cinema.Persistence.Models;
using Cinema.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Cinema.RequestModels;
using Cinema.Persistence.Models;

namespace Cinema.Mapping
{
    public class MappingProfile : Profile{
    	public MappingProfile()
    	{
    	CreateMap<CreateClientRequestModel, Client>().ReverseMap();
    	CreateMap<UpdateClientRequestModel, Client>().ReverseMap();
    	CreateMap<CreateAdminRequestModel, Admin>().ReverseMap();
    	CreateMap<UpdateAdminRequestModel, Admin>().ReverseMap();
    	CreateMap<CreateCinemaHallRequestModel, CinemaHall>().ReverseMap();
    	CreateMap<UpdateCinemaHallRequestModel, CinemaHall>().ReverseMap();
    	CreateMap<CreateSeatRequestModel, Seat>().ReverseMap();
    	CreateMap<UpdateSeatRequestModel, Seat>().ReverseMap();
    	CreateMap<CreateCinemaBookingRequestModel, CinemaBooking>().ReverseMap();
    	CreateMap<UpdateCinemaBookingRequestModel, CinemaBooking>().ReverseMap();
    	CreateMap<CreateNightPlanRequestModel, NightPlan>().ReverseMap();
    	CreateMap<UpdateNightPlanRequestModel, NightPlan>().ReverseMap();
    	}	
    }
}
