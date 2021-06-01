using System;
using System.Collections.Generic;

namespace Cinema.Persistence.Models
{
    public class CinemaHall : IEntity
    {
    	public Guid Id {get; set;}
        public string name {get; set;}
        public string city {get; set;}
        public bool Imax {get; set;}
        public Client cli {get; set;} 
    }
}
