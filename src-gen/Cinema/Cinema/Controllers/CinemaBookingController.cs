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
    [Route("CinemaBooking")]
    public class CinemaBookingController : ControllerBase
    {
        private readonly ICinemaBookingHandler _CinemaBookingHandler;
        private readonly IMapper _mapper;

        public CinemaBookingController(ICinemaBookingHandler CinemaBookingHandler, IMapper mapper)
        {
            _CinemaBookingHandler = CinemaBookingHandler;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<CinemaBooking>>> Get(int page = 0, int pageSize = 100)
        {
            var result = await _CinemaBookingHandler.GetAll(page, pageSize);
            
            if (result == null)
            	return NotFound();
            
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CinemaBooking>> Get(Guid id)
        {
            var result = await _CinemaBookingHandler.Get(id);
            
            if (result == null)
            	return NotFound();
            				            
           	return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Guid>> Create([FromBody]CreateCinemaBookingRequestModel rm)
        {
        	
            var model = _mapper.Map<CinemaBooking>(rm);
            var result = await _CinemaBookingHandler.CreateCinemaBooking(model);
            
            if (result == null)
            	return NotFound();
            				            
            return Ok(result);
        }
        
        [HttpPut]
        [Route("")]
        public async Task<ActionResult<CinemaBooking>> Put([FromBody] UpdateCinemaBookingRequestModel rm)
        {
        	
        	var model = _mapper.Map<CinemaBooking>(rm);
        	var result = await _CinemaBookingHandler.Update(model);
        	
        	if (result == null)
        		return NotFound();
        					            
        	return Ok(result);
        }
        
        [HttpDelete]
        [Route("")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
        	var result = await _CinemaBookingHandler.DeleteCinemaBooking(id);
        	
        	if (!result)
        	     return NotFound();
        	
        	return Ok(result);
        }
        
    }
}
