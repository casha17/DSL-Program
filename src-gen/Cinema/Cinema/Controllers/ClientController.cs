using System;
using System.Threading.Tasks;
using Cinema.Handlers;
using Cinema.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using Cinema.Persistence.Models;

namespace Cinema.Controllers
{
    [Route("Client")]
    public class ClientController : ControllerBase
    {
        private readonly IClientHandler _ClientHandler;
        private readonly IMapper _mapper;

        public ClientController(IClientHandler ClientHandler, IMapper mapper)
        {
            _ClientHandler = ClientHandler;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Client>>> Get(int page = 0, int pageSize = 100)
        {
            var result = await _ClientHandler.GetAll(page, pageSize);
            
            if (result == null)
            	return NotFound();
            
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Client>> Get(Guid id)
        {
            var result = await _ClientHandler.Get(id);
            
            if (result == null)
            	return NotFound();
            				            
           	return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Guid>> Create([FromBody]CreateClientRequestModel rm)
        {
        	
            var model = _mapper.Map<Client>(rm);
            var result = await _ClientHandler.CreateClient(model);
            
            if (result == null)
            	return NotFound();
            				            
            return Ok(result);
        }
        
        [HttpPut]
        [Route("")]
        public async Task<ActionResult<Client>> Put([FromBody] UpdateClientRequestModel rm)
        {
        	
        	var model = _mapper.Map<Client>(rm);
        	var result = await _ClientHandler.Update(model);
        	
        	if (result == null)
        		return NotFound();
        					            
        	return Ok(result);
        }
        
        [HttpDelete]
        [Route("")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
        	var result = await _ClientHandler.DeleteClient(id);
        	
        	if (!result)
        	     return NotFound();
        	
        	return Ok(result);
        }
        
    }
}
