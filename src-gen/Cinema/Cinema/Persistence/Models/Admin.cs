using System;
using System.Collections.Generic;

namespace Cinema.Persistence.Models
{
	public class Admin : Client, IEntity
	{
        public Client cli {get; set;} 
    }
}
