using Cinema.Persistence.Models;
using System.Collections.Generic;
namespace Cinema.RequestModels
{
    public class CreateCinemaHallRequestModel
    {
public string name {get; set;}
public string city {get; set;}
public bool Imax {get; set;}
public Client cli {get; set;} 
    }
}
