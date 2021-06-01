using Cinema.Persistence.Models;
using System.Collections.Generic;
namespace Cinema.RequestModels
{
    public class CreateCinemaBookingRequestModel
    {
public string name {get; set;}
public Client client {get; set;} 
public Seat seat {get; set;} 
public NightPlan plan {get; set;} 
    }
}
