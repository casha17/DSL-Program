using Cinema.Persistence.Models;
using System.Collections.Generic;
using System;

namespace Cinema.RequestModels
{
	public class UpdateSeatRequestModel
	{
	public Guid Id {get; set;}
public string name {get; set;}
public int weight {get; set;}
public List<NightPlan> nightPlans {get; set;} 
    }
}
