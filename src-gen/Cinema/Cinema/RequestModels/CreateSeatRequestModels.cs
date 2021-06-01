using Cinema.Persistence.Models;
using System.Collections.Generic;
namespace Cinema.RequestModels
{
	public class CreateSeatRequestModel
	{
public string name {get; set;}
public int weight {get; set;}
public List<NightPlan> nightPlans {get; set;} 
    }
}
