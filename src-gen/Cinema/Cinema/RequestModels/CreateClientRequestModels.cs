using Cinema.Persistence.Models;
using System.Collections.Generic;
namespace Cinema.RequestModels
{
    public class CreateClientRequestModel
			{
		public string name {get; set;}
		public int age {get; set;}
		public int height {get; set;}
		public bool isVip {get; set;}
		public List<int> discount {get; set;}
    }
}
