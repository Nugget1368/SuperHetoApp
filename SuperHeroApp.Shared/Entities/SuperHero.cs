	using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroApp.Shared.Entities
{
	public class SuperHero
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
	}
}
