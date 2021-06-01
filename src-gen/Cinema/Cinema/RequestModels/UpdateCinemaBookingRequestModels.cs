using Cinema.Persistence.Models;
using System.Collections.Generic;
using System;
		
namespace Cinema.RequestModels
{
    public class UpdateCinemaBookingRequestModel
    {
    	public Guid Id {get; set;}
public string name {get; set;}
public Client client {get; set;} 
public Seat seat {get; set;} 
public NightPlan plan {get; set;} 
    }
}
