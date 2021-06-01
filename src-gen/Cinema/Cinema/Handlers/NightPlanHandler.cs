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
    public interface INightPlanHandler
    {
        Task<Guid> CreateNightPlan(NightPlan model);
        Task<bool> DeleteNightPlan(Guid id);
        Task<List<NightPlan>> GetAll(int page, int pageSize);
        Task<NightPlan> Update(NightPlan model);
        Task<NightPlan> Get(Guid id);
    }
    
    public class NightPlanHandler : INightPlanHandler
    {
        private readonly INightPlanRepository _NightPlanRepository;

        public NightPlanHandler(INightPlanRepository NightPlanRepository
                             )
        {
            _NightPlanRepository = NightPlanRepository;
        }
        
        private IMapper CreateMapperConf<T>()
	        		{
	        			var config = new MapperConfiguration(cfg =>
	        			{
	        				cfg.CreateMap<T, T>();
	        			});
	        			return config.CreateMapper();
	        		}

		public async Task<Guid> CreateNightPlan(NightPlan model)
		{
			return await _NightPlanRepository.Insert(model);
		}
		
		public async Task<bool> DeleteNightPlan(Guid id)
		{
			return await _NightPlanRepository.Delete(id);	
		}
		
		public async Task<List<NightPlan>> GetAll(int page, int pageSize)
		{
			var all = await _NightPlanRepository.GetPaged(page, pageSize);	
			var map = CreateMapperConf<NightPlan>();
			var protectiveCopy = all.Select(e => map.Map<NightPlan, NightPlan>(e)).ToList();
			var finalResult = new List<NightPlan>();
			
			
			if(finalResult.Count == 0) finalResult = protectiveCopy.ToList();
			return finalResult;
		}
		
		public async Task<NightPlan> Update(NightPlan model)
		{
			return await _NightPlanRepository.Put(model);
		}
		
		public async Task<NightPlan> Get(Guid id)
		{
			var result = await _NightPlanRepository.GetById(id);
			var map = CreateMapperConf<NightPlan>();
			var finalResult = map.Map<NightPlan, NightPlan>(result);
			return finalResult;	
		}
        
    }
}
