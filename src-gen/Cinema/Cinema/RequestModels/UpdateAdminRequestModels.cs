using Cinema.Persistence.Models;
using System.Collections.Generic;
using System;
		
namespace Cinema.RequestModels
{
	public class UpdateAdminRequestModel : UpdateClientRequestModel
	{
		public Client cli {get; set;} 
    }
}
