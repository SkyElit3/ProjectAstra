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
    public class SolarSystemRepository : ISolarSystemRepository
    {
        private readonly DataContext _dataContext;

        public SolarSystemRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<List<SolarSystem>> GetAllSolarSystems(SolarSystemFilter filter, int pagination = 50, int skip = 0)
        {
            return await filter.Filter(_dataContext.SolarSystems
                    .AsQueryable())
                .Skip(skip)
                .Take(pagination)
                .ToListAsync();
        }

        public async Task<bool> CreateSolarSystem(SolarSystem inputSolarSystem)
        {
            await _dataContext.SolarSystems.AddAsync(inputSolarSystem);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSolarSystem(Guid id)
        {
            _dataContext.SolarSystems.Remove(
                await _dataContext.SolarSystems.FirstOrDefaultAsync(shuttle => shuttle.Id.Equals(id)));
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateSolarSystem(SolarSystem inputSolarSystem)
        {
            _dataContext.SolarSystems.Update(inputSolarSystem);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}