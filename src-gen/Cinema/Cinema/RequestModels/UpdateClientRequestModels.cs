using Cinema.Persistence.Models;
using System.Collections.Generic;
using System;
		
namespace Cinema.RequestModels
{
    public class UpdateClientRequestModel
    {
    public Guid Id {get; set;}
		public string name {get; set;}
		public int age {get; set;}
		public int height {get; set;}
		public bool isVip {get; set;}
		public List<int> discount {get; set;}
    }
}
