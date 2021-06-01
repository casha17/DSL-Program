using Cinema.Persistence.Models;
using System.Collections.Generic;
using System;
		
namespace Cinema.RequestModels
{
    public class UpdateCinemaHallRequestModel
    {
    	public Guid Id {get; set;}
public string name {get; set;}
public string city {get; set;}
public bool Imax {get; set;}
public Client cli {get; set;} 
    }
}
