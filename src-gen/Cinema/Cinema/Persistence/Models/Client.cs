using System;
using System.Collections.Generic;

namespace Cinema.Persistence.Models
{
	public class Client : IEntity
	{
		public Guid Id {get; set;}
        public string name {get; set;}
        public int age {get; set;}
        public int height {get; set;}
        public bool isVip {get; set;}
        public List<int> discount {get; set;}
    }
}
