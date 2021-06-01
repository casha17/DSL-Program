using System;
using System.Collections.Generic;

namespace Cinema.Persistence.Models
{
    public class NightPlan : IEntity
    {
    	public Guid Id {get; set;}
        public string name {get; set;}
    }
}
