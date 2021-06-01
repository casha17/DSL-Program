using System;
using System.Collections.Generic;

namespace Cinema.Persistence.Models
{
	public class Seat : IEntity
	{
		public Guid Id {get; set;}
        public string name {get; set;}
        public int weight {get; set;}
        public List<NightPlan> nightPlans {get; set;} 
    }
}
