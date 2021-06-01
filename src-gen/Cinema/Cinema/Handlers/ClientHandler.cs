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
    public interface IClientHandler
    {
        Task<Guid> CreateClient(Client model);
        Task<bool> DeleteClient(Guid id);
        Task<List<Client>> GetAll(int page, int pageSize);
        Task<Client> Update(Client model);
        Task<Client> Get(Guid id);
    }
    
    public class ClientHandler : IClientHandler
    {
        private readonly IClientRepository _ClientRepository;

        public ClientHandler(IClientRepository ClientRepository
                             )
        {
            _ClientRepository = ClientRepository;
        }
        
        private IMapper CreateMapperConf<T>()
	        		{
	        			var config = new MapperConfiguration(cfg =>
	        			{
	        				cfg.CreateMap<T, T>();
	        			});
	        			return config.CreateMapper();
	        		}

		public async Task<Guid> CreateClient(Client model)
		{
			return await _ClientRepository.Insert(model);
		}
		
		public async Task<bool> DeleteClient(Guid id)
		{
			return await _ClientRepository.Delete(id);	
		}
		
		public async Task<List<Client>> GetAll(int page, int pageSize)
		{
			var all = await _ClientRepository.GetPaged(page, pageSize);	
			var map = CreateMapperConf<Client>();
			var protectiveCopy = all.Select(e => map.Map<Client, Client>(e)).ToList();
			var finalResult = new List<Client>();
			
			
			if(finalResult.Count == 0) finalResult = protectiveCopy.ToList();
			return finalResult;
		}
		
		public async Task<Client> Update(Client model)
		{
			return await _ClientRepository.Put(model);
		}
		
		public async Task<Client> Get(Guid id)
		{
			var result = await _ClientRepository.GetById(id);
			var map = CreateMapperConf<Client>();
			var finalResult = map.Map<Client, Client>(result);
			return finalResult;	
		}
        
    }
}
