using Microsoft.EntityFrameworkCore;
using SuperHeroApp.Shared.Entities;

namespace SuperHeroApp.Server.Context
{
	public class SuperHeroContext : DbContext
	{
		public SuperHeroContext(DbContextOptions<SuperHeroContext> options) : base(options) { }

		/// <summary>
		/// Hero Table
		/// </summary>
		public DbSet<SuperHero> Heroes { get; set; }
	}
}
