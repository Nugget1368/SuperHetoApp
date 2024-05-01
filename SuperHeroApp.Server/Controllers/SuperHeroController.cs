using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroApp.Server.Context;
using SuperHeroApp.Server.Services;
using SuperHeroApp.Shared.Entities;

namespace SuperHeroApp.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SuperHeroController : ControllerBase
	{
		private readonly HeroService service;
		//TODO: Create Service and inject Service instead of Context
		public SuperHeroController(HeroService service)
		{
			this.service = service;
		}
		[HttpGet]
		public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
		{
			var result = await service.GetAllHeroesAsync();
			if (result.Success)
			{
				return Ok(result.Result);
			}
			return BadRequest(result.Message);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<SuperHero>> GetHero(int id)
		{
			var result = await service.GetHeroAsync(id);
			if (result.Success)
			{
				return Ok(result.Result);
			}
			return NotFound(result.Message);
		}

		[HttpPost]
		public async Task<ActionResult> AddHero(SuperHero hero) //TODO: Create DTO Superhero
		{
			var result = await this.service.AddHeroAsync(hero);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		[HttpPut]
		public async Task<ActionResult> EditHero(SuperHero heroUpdate)
		{
			var result = await service.EditHero(heroUpdate);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return NotFound(result.Message);
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteHero(int id)
		{
			try
			{
				var result = await service.DeleteHero(id);
				return Ok(result.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
