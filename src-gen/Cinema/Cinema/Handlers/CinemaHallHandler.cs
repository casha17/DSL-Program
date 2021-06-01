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
    public interface ICinemaHallHandler
    {
        Task<Guid> CreateCinemaHall(CinemaHall model);
        Task<bool> DeleteCinemaHall(Guid id);
        Task<List<CinemaHall>> GetAll(int page, int pageSize);
        Task<CinemaHall> Update(CinemaHall model);
        Task<CinemaHall> Get(Guid id);
    }
    
    public class CinemaHallHandler : ICinemaHallHandler
    {
        private readonly ICinemaHallRepository _CinemaHallRepository;
       IClientHandler _ClientHandler;

        public CinemaHallHandler(ICinemaHallRepository CinemaHallRepository
                             , IClientHandler ClientHandler
                             )
        {
            _CinemaHallRepository = CinemaHallRepository;
            _ClientHandler = ClientHandler;
        }
        
        private IMapper CreateMapperConf<T>()
	        		{
	        			var config = new MapperConfiguration(cfg =>
	        			{
	        				cfg.CreateMap<T, T>();
	        			});
	        			return config.CreateMapper();
	        		}

		public async Task<Guid> CreateCinemaHall(CinemaHall model)
		{
			if(model.cli.Id.Equals(Guid.NewGuid())){
			      model.cli.Id = new Guid();
			      await _ClientHandler.CreateClient(model.cli);
			   }
			return await _CinemaHallRepository.Insert(model);
		}
		
		public async Task<bool> DeleteCinemaHall(Guid id)
		{
			return await _CinemaHallRepository.Delete(id);	
		}
		
		public async Task<List<CinemaHall>> GetAll(int page, int pageSize)
		{
			var all = await _CinemaHallRepository.GetPaged(page, pageSize);	
			var map = CreateMapperConf<CinemaHall>();
			var protectiveCopy = all.Select(e => map.Map<CinemaHall, CinemaHall>(e)).ToList();
			var finalResult = new List<CinemaHall>();
			
			foreach (var item in protectiveCopy) item.cli = await _ClientHandler.Get(item.cli.Id);
			
			if(finalResult.Count == 0) finalResult = protectiveCopy.ToList();
			return finalResult;
		}
		
		public async Task<CinemaHall> Update(CinemaHall model)
		{
			if(model.cli != null) await _ClientHandler.Update(model.cli);
			return await _CinemaHallRepository.Put(model);
		}
		
		public async Task<CinemaHall> Get(Guid id)
		{
			var result = await _CinemaHallRepository.GetById(id);
			var map = CreateMapperConf<CinemaHall>();
			var finalResult = map.Map<CinemaHall, CinemaHall>(result);
			if(finalResult.cli != null) finalResult.cli = await _ClientHandler.Get(finalResult.cli.Id);
			return finalResult;	
		}
        
    }
}
