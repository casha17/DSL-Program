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
    [Route("Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminHandler _AdminHandler;
        private readonly IMapper _mapper;

        public AdminController(IAdminHandler AdminHandler, IMapper mapper)
        {
            _AdminHandler = AdminHandler;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Admin>>> Get(int page = 0, int pageSize = 100)
        {
            var result = await _AdminHandler.GetAll(page, pageSize);
            
            if (result == null)
            	return NotFound();
            
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Admin>> Get(Guid id)
        {
            var result = await _AdminHandler.Get(id);
            
            if (result == null)
            	return NotFound();
            				            
           	return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Guid>> Create([FromBody]CreateAdminRequestModel rm)
        {
        	if(!(rm.age <= 10 )) 
        		return BadRequest("Operation failed due to request failing the following constraint: " + 
        								"rm.age <= 10 ");
        	
            var model = _mapper.Map<Admin>(rm);
            var result = await _AdminHandler.CreateAdmin(model);
            
            if (result == null)
            	return NotFound();
            				            
            return Ok(result);
        }
        
        [HttpPut]
        [Route("")]
        public async Task<ActionResult<Admin>> Put([FromBody] UpdateAdminRequestModel rm)
        {
        	if(!(rm.age <= 10 )) 
        		return BadRequest("Operation failed due to request failing the following constraint: " + 
        								"rm.age <= 10 ");
        	
        	var model = _mapper.Map<Admin>(rm);
        	var result = await _AdminHandler.Update(model);
        	
        	if (result == null)
        		return NotFound();
        					            
        	return Ok(result);
        }
        
        [HttpDelete]
        [Route("")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
        	var result = await _AdminHandler.DeleteAdmin(id);
        	
        	if (!result)
        	     return NotFound();
        	
        	return Ok(result);
        }
        
    }
}
