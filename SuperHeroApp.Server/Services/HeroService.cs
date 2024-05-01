using Microsoft.EntityFrameworkCore;
using SuperHeroApp.Server.Context;
using SuperHeroApp.Server.Models;
using SuperHeroApp.Shared.Entities;

namespace SuperHeroApp.Server.Services
{
	public class HeroService
	{
		private readonly SuperHeroContext context;

		public HeroService(SuperHeroContext context)
		{
			this.context = context;
		}

		public async Task<ResultModel<List<SuperHero>>> GetAllHeroesAsync()
		{
			var heroes = await this.context.Heroes.ToListAsync();
			if (heroes is not null)
			{
				return new ResultModel<List<SuperHero>> { Success = true, Result = heroes };
			}
			return new ResultModel<List<SuperHero>> { Success = false, Message = "Heroes not found." };
		}

		public async Task<ResultModel<SuperHero>> GetHeroAsync(int id)
		{
			var hero = await this.context.Heroes.FindAsync(id);
			if (hero is not null)
			{
				return new ResultModel<SuperHero> { Success = true, Result = hero };
			}
			return new ResultModel<SuperHero> { Success = false, Message = "Hero not found." };
		}

		public async Task<ResultModel> AddHeroAsync(SuperHero hero) //TODO: Create DTO for Hero
		{
			try
			{
				this.context.Heroes.Add(hero);
				await context.SaveChangesAsync();
				return new ResultModel() { Success = true, Message = "Hero was added." };
			}
			catch (Exception ex)
			{
				return new ResultModel() { Success = false, Message = ex.Message };
			}
		}

		public async Task<ResultModel> EditHero(SuperHero heroUpdate)
		{
			var dbHero = await this.context.Heroes.FindAsync(heroUpdate.Id);
			try
			{
				if (dbHero is not null)
				{
					//TODO: Change the sets
					dbHero.Name = heroUpdate.Name;
					dbHero.FirstName = heroUpdate.FirstName;
					dbHero.LastName = heroUpdate.LastName;
					dbHero.City = heroUpdate.City;
					await this.context.SaveChangesAsync();
					return new ResultModel { Success = true, Message = "Hero was edited." };
				}
				return new ResultModel() { Success = false, Message = "Hero not found." };
			}
			catch (Exception ex)
			{
				return new ResultModel() { Success = false, Message = ex.Message };
			}
		}

		public async Task<ResultModel> DeleteHero(int id)
		{
			try
			{
				var hero = await this.context.Heroes.FindAsync(id);
				if (hero is not null)
				{
					this.context.Heroes.Remove(hero);
					await context.SaveChangesAsync();
					return new ResultModel() { Success = true, Message = "Hero was removed." };
				}
				return new ResultModel() { Success = false, Message = "Hero not found." };
			}
			catch (Exception ex)
			{
				return new ResultModel() { Success = false, Message = ex.Message };
			}
		}
	}
}