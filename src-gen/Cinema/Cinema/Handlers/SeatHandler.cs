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
    public interface ISeatHandler
    {
        Task<Guid> CreateSeat(Seat model);
        Task<bool> DeleteSeat(Guid id);
        Task<List<Seat>> GetAll(int page, int pageSize);
        Task<Seat> Update(Seat model);
        Task<Seat> Get(Guid id);
        Task<List<Seat>> AddNightPlanToAllResources(List<NightPlan> collection);
    }
    
    public class SeatHandler : ISeatHandler
    {
        private readonly ISeatRepository _SeatRepository;
       INightPlanHandler _NightPlanHandler;

        public SeatHandler(ISeatRepository SeatRepository
                             , INightPlanHandler NightPlanHandler
                             )
        {
            _SeatRepository = SeatRepository;
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

		public async Task<Guid> CreateSeat(Seat model)
		{
			foreach(var sub in model.nightPlans)
			{
				if (sub.Id.Equals(Guid.NewGuid())){
					sub.Id = new Guid();
					await _NightPlanHandler.CreateNightPlan(sub);
				}
			}
			return await _SeatRepository.Insert(model);
		}
		
		public async Task<bool> DeleteSeat(Guid id)
		{
			return await _SeatRepository.Delete(id);	
		}
		
		public async Task<List<Seat>> GetAll(int page, int pageSize)
		{
			var all = await _SeatRepository.GetPaged(page, pageSize);	
			var map = CreateMapperConf<Seat>();
			var protectiveCopy = all.Select(e => map.Map<Seat, Seat>(e)).ToList();
			var finalResult = new List<Seat>();
			
			foreach(var item in protectiveCopy) item.nightPlans = new List<NightPlan>();
			foreach(var item in all)
			{
				var protectedSingle = protectiveCopy.ToList().Find(x => x.Id.Equals(item.Id));
				foreach(var single in item.nightPlans)
				{
					var res = await _NightPlanHandler.Get(single.Id);
					if (res != null) protectedSingle.nightPlans.Add(res);
				}
				finalResult.Add(protectedSingle);
			}
			
			if(finalResult.Count == 0) finalResult = protectiveCopy.ToList();
			return finalResult;
		}
		
		public async Task<Seat> Update(Seat model)
		{
			foreach(var single in model.nightPlans) if(single != null) await _NightPlanHandler.Update(single);
			return await _SeatRepository.Put(model);
		}
		
		public async Task<Seat> Get(Guid id)
		{
			var result = await _SeatRepository.GetById(id);
			var map = CreateMapperConf<Seat>();
			var finalResult = map.Map<Seat, Seat>(result);
			if(result.nightPlans != null)
			{
				var list = new List<NightPlan>();
				foreach(var item in result.nightPlans)
				{
					var res = await _NightPlanHandler.Get(item.Id);
					if (res != null) list.Add(res);
				}
				finalResult.nightPlans = list;
			}
			return finalResult;	
		}
        
        public async Task<List<Seat>> AddNightPlanToAllResources(List<NightPlan> collection)
        {
        	var all = await GetAll(0, 1000);
        	
        	foreach(var res in all)
        	{
        		res.nightPlans.AddRange(collection);
        		await this.Update(res);
        	}
        	
        	return all.ToList();
        }
    }
}
