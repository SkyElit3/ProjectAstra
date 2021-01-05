using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectAstra.Web.PlanetApi.Core.Filters;
using ProjectAstra.Web.PlanetApi.Core.Models;

namespace ProjectAstra.Web.PlanetApi.Core.Interfaces
{
    public interface ISolarSystemRepository
    {
        public Task<List<SolarSystem>> GetAllSolarSystems(SolarSystemFilter filter, int pagination = 50, int skip = 0);

        public Task<bool> CreateSolarSystem(SolarSystem inputSolarSystem);

        public Task<bool> DeleteSolarSystem(Guid id);

        public Task<bool> UpdateSolarSystem(SolarSystem inputSolarSystem);
    }
}