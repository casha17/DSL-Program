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
    [Route("CinemaHall")]
    public class CinemaHallController : ControllerBase
    {
        private readonly ICinemaHallHandler _CinemaHallHandler;
        private readonly IMapper _mapper;

        public CinemaHallController(ICinemaHallHandler CinemaHallHandler, IMapper mapper)
        {
            _CinemaHallHandler = CinemaHallHandler;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<CinemaHall>>> Get(int page = 0, int pageSize = 100)
        {
            var result = await _CinemaHallHandler.GetAll(page, pageSize);
            
            if (result == null)
            	return NotFound();
            
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CinemaHall>> Get(Guid id)
        {
            var result = await _CinemaHallHandler.Get(id);
            
            if (result == null)
            	return NotFound();
            				            
           	return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Guid>> Create([FromBody]CreateCinemaHallRequestModel rm)
        {
        	
            var model = _mapper.Map<CinemaHall>(rm);
            var result = await _CinemaHallHandler.CreateCinemaHall(model);
            
            if (result == null)
            	return NotFound();
            				            
            return Ok(result);
        }
        
        [HttpPut]
        [Route("")]
        public async Task<ActionResult<CinemaHall>> Put([FromBody] UpdateCinemaHallRequestModel rm)
        {
        	
        	var model = _mapper.Map<CinemaHall>(rm);
        	var result = await _CinemaHallHandler.Update(model);
        	
        	if (result == null)
        		return NotFound();
        					            
        	return Ok(result);
        }
        
        [HttpDelete]
        [Route("")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
        	var result = await _CinemaHallHandler.DeleteCinemaHall(id);
        	
        	if (!result)
        	     return NotFound();
        	
        	return Ok(result);
        }
        
    }
}
