using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectAstra.Web.PlanetApi.Core.Filters;
using ProjectAstra.Web.PlanetApi.Core.Interfaces;
using ProjectAstra.Web.PlanetApi.Core.Models;
using ProjectAstra.Web.PlanetApi.Infrastructure.Data;

namespace ProjectAstra.Web.PlanetApi.Infrastructure.Repositories
{
    public class PlanetRepository : IPlanetRepository
    {
        private readonly DataContext _dataContext;

        public PlanetRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<List<Planet>> GetAllPlanets(PlanetFilter filter, int pagination = 50, int skip = 0)
        {
            return await filter.Filter(_dataContext.Planets
                    .AsQueryable())
                .Skip(skip)
                .Take(pagination)
                .ToListAsync();
        }

        public async Task<bool> CreatePlanet(Planet inputPlanet)
        {
            await _dataContext.Planets.AddAsync(inputPlanet);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePlanet(Guid id)
        {
            _dataContext.Planets.Remove(
                await _dataContext.Planets.FirstOrDefaultAsync(shuttle => shuttle.Id.Equals(id)));
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePlanet(Planet inputPlanet)
        {
            _dataContext.Planets.Update(inputPlanet);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}