using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroApp.Server.Context;
using SuperHeroApp.Shared.Entities;

namespace SuperHeroApp.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SuperHeroController : ControllerBase
	{
		private readonly SuperHeroContext context;

		//TODO: Create Service and inject Service instead of Context
		public SuperHeroController(SuperHeroContext context)
		{
			this.context = context;
		}
		[HttpGet]
		public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
		{
			try
			{
				var heroes = await this.context.Heroes.ToListAsync();
				return Ok(heroes);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<SuperHero>> GetHero(int id)
		{
			try
			{
				var hero = await this.context.Heroes.FindAsync(id);
				return Ok(hero);
			}
			catch
			{
				return NotFound();
			}
		}

		[HttpPost]
		public async Task<ActionResult> AddHero(SuperHero hero) //TODO: Create DTO Superhero
		{
			try
			{
				this.context.Heroes.Add(hero);
				await context.SaveChangesAsync();

				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<ActionResult> EditHero(SuperHero heroUpdate)
		{
			var dbHero = await this.context.Heroes.FindAsync(heroUpdate.Id);
			if (dbHero == null)
			{
				return NotFound();
			}
			try
			{
				//TODO: Change the sets
				dbHero.Name = heroUpdate.Name;
				dbHero.FirstName = heroUpdate.FirstName;
				dbHero.LastName = heroUpdate.LastName;
				dbHero.City = heroUpdate.City;

				await this.context.SaveChangesAsync();
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteHero(int id)
		{
			var hero = await this.context.Heroes.FindAsync(id);
			if (hero == null)
			{
				return NotFound();
			}
			try
			{
				this.context.Heroes.Remove(hero);
				await context.SaveChangesAsync();
				return Ok("Hero removed.");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
