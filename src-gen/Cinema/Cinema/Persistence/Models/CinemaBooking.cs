using System;
using System.Collections.Generic;

namespace Cinema.Persistence.Models
{
    public class CinemaBooking : IEntity
    {
    	public Guid Id {get; set;}
        public string name {get; set;}
        public Client client {get; set;} 
        public Seat seat {get; set;} 
        public NightPlan plan {get; set;} 
    }
}
