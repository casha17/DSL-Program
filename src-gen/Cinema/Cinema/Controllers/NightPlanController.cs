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
    [Route("NightPlan")]
    public class NightPlanController : ControllerBase
    {
        private readonly INightPlanHandler _NightPlanHandler;
        private readonly IMapper _mapper;

        public NightPlanController(INightPlanHandler NightPlanHandler, IMapper mapper)
        {
            _NightPlanHandler = NightPlanHandler;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<NightPlan>>> Get(int page = 0, int pageSize = 100)
        {
            var result = await _NightPlanHandler.GetAll(page, pageSize);
            
            if (result == null)
            	return NotFound();
            
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<NightPlan>> Get(Guid id)
        {
            var result = await _NightPlanHandler.Get(id);
            
            if (result == null)
            	return NotFound();
            				            
           	return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Guid>> Create([FromBody]CreateNightPlanRequestModel rm)
        {
        	
            var model = _mapper.Map<NightPlan>(rm);
            var result = await _NightPlanHandler.CreateNightPlan(model);
            
            if (result == null)
            	return NotFound();
            				            
            return Ok(result);
        }
        
        [HttpPut]
        [Route("")]
        public async Task<ActionResult<NightPlan>> Put([FromBody] UpdateNightPlanRequestModel rm)
        {
        	
        	var model = _mapper.Map<NightPlan>(rm);
        	var result = await _NightPlanHandler.Update(model);
        	
        	if (result == null)
        		return NotFound();
        					            
        	return Ok(result);
        }
        
        [HttpDelete]
        [Route("")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
        	var result = await _NightPlanHandler.DeleteNightPlan(id);
        	
        	if (!result)
        	     return NotFound();
        	
        	return Ok(result);
        }
        
    }
}
