using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.Persistence.Repositories;
using Cinema.Persistence.Models;
using Cinema.RequestModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Cinema.Handlers
{
    public interface ICinemaBookingHandler
    {
        Task<Guid> CreateCinemaBooking(CinemaBooking model);
        Task<bool> DeleteCinemaBooking(Guid id);
        Task<List<CinemaBooking>> GetAll(int page, int pageSize);
        Task<CinemaBooking> Update(CinemaBooking model);
        Task<CinemaBooking> Get(Guid id);
    }
    
    public class CinemaBookingHandler : ICinemaBookingHandler
    {
        private readonly ICinemaBookingRepository _CinemaBookingRepository;
       IClientHandler _ClientHandler;
       ISeatHandler _SeatHandler;
       INightPlanHandler _NightPlanHandler;

        public CinemaBookingHandler(ICinemaBookingRepository CinemaBookingRepository
                             , IClientHandler ClientHandler
                             , ISeatHandler SeatHandler
                             , INightPlanHandler NightPlanHandler
                             )
        {
            _CinemaBookingRepository = CinemaBookingRepository;
            _ClientHandler = ClientHandler;
            _SeatHandler = SeatHandler;
            _NightPlanHandler = NightPlanHandler;
        }
        
        private IMapper CreateMapperConf<T>()
	        		{
	        			var config = new MapperConfiguration(cfg =>
	        			{
	        				cfg.CreateMap<T, T>();
	        			});
	        			return config.CreateMapper();
	        		}

		public async Task<Guid> CreateCinemaBooking(CinemaBooking model)
		{
			if(model.client.Id.Equals(Guid.NewGuid())){
			      model.client.Id = new Guid();
			      await _ClientHandler.CreateClient(model.client);
			   }
			if(model.seat.Id.Equals(Guid.NewGuid())){
			      model.seat.Id = new Guid();
			      await _SeatHandler.CreateSeat(model.seat);
			   }
			if(model.plan.Id.Equals(Guid.NewGuid())){
			      model.plan.Id = new Guid();
			      await _NightPlanHandler.CreateNightPlan(model.plan);
			   }
			return await _CinemaBookingRepository.Insert(model);
		}
		
		public async Task<bool> DeleteCinemaBooking(Guid id)
		{
			return await _CinemaBookingRepository.Delete(id);	
		}
		
		public async Task<List<CinemaBooking>> GetAll(int page, int pageSize)
		{
			var all = await _CinemaBookingRepository.GetPaged(page, pageSize);	
			var map = CreateMapperConf<CinemaBooking>();
			var protectiveCopy = all.Select(e => map.Map<CinemaBooking, CinemaBooking>(e)).ToList();
			var finalResult = new List<CinemaBooking>();
			
			foreach (var item in protectiveCopy) item.client = await _ClientHandler.Get(item.client.Id);
			foreach (var item in protectiveCopy) item.seat = await _SeatHandler.Get(item.seat.Id);
			foreach (var item in protectiveCopy) item.plan = await _NightPlanHandler.Get(item.plan.Id);
			
			if(finalResult.Count == 0) finalResult = protectiveCopy.ToList();
			return finalResult;
		}
		
		public async Task<CinemaBooking> Update(CinemaBooking model)
		{
			if(model.client != null) await _ClientHandler.Update(model.client);
			if(model.seat != null) await _SeatHandler.Update(model.seat);
			if(model.plan != null) await _NightPlanHandler.Update(model.plan);
			return await _CinemaBookingRepository.Put(model);
		}
		
		public async Task<CinemaBooking> Get(Guid id)
		{
			var result = await _CinemaBookingRepository.GetById(id);
			var map = CreateMapperConf<CinemaBooking>();
			var finalResult = map.Map<CinemaBooking, CinemaBooking>(result);
			if(finalResult.client != null) finalResult.client = await _ClientHandler.Get(finalResult.client.Id);
			if(finalResult.seat != null) finalResult.seat = await _SeatHandler.Get(finalResult.seat.Id);
			if(finalResult.plan != null) finalResult.plan = await _NightPlanHandler.Get(finalResult.plan.Id);
			return finalResult;	
		}
        
    }
}
