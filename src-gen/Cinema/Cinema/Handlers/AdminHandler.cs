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
    public interface IAdminHandler
    {
        Task<Guid> CreateAdmin(Admin model);
        Task<bool> DeleteAdmin(Guid id);
        Task<List<Admin>> GetAll(int page, int pageSize);
        Task<Admin> Update(Admin model);
        Task<Admin> Get(Guid id);
    }
    
    public class AdminHandler : IAdminHandler
    {
        private readonly IAdminRepository _AdminRepository;
       IClientHandler _ClientHandler;

        public AdminHandler(IAdminRepository AdminRepository
                             , IClientHandler ClientHandler
                             )
        {
            _AdminRepository = AdminRepository;
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

		public async Task<Guid> CreateAdmin(Admin model)
		{
			if(model.cli.Id.Equals(Guid.NewGuid())){
			      model.cli.Id = new Guid();
			      await _ClientHandler.CreateClient(model.cli);
			   }
			return await _AdminRepository.Insert(model);
		}
		
		public async Task<bool> DeleteAdmin(Guid id)
		{
			return await _AdminRepository.Delete(id);	
		}
		
		public async Task<List<Admin>> GetAll(int page, int pageSize)
		{
			var all = await _AdminRepository.GetPaged(page, pageSize);	
			var map = CreateMapperConf<Admin>();
			var protectiveCopy = all.Select(e => map.Map<Admin, Admin>(e)).ToList();
			var finalResult = new List<Admin>();
			
			foreach (var item in protectiveCopy) item.cli = await _ClientHandler.Get(item.cli.Id);
			
			if(finalResult.Count == 0) finalResult = protectiveCopy.ToList();
			return finalResult;
		}
		
		public async Task<Admin> Update(Admin model)
		{
			if(model.cli != null) await _ClientHandler.Update(model.cli);
			return await _AdminRepository.Put(model);
		}
		
		public async Task<Admin> Get(Guid id)
		{
			var result = await _AdminRepository.GetById(id);
			var map = CreateMapperConf<Admin>();
			var finalResult = map.Map<Admin, Admin>(result);
			if(finalResult.cli != null) finalResult.cli = await _ClientHandler.Get(finalResult.cli.Id);
			return finalResult;	
		}
        
    }
}
