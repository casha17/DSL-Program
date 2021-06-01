using Cinema.Persistence.Models;
using System.Collections.Generic;
namespace Cinema.RequestModels
{
	public class CreateAdminRequestModel : CreateClientRequestModel
	{
		public Client cli {get; set;} 
    }
}
