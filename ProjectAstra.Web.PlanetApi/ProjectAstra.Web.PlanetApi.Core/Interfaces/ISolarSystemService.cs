using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectAstra.Web.PlanetApi.Core.Models;

namespace ProjectAstra.Web.PlanetApi.Core.Interfaces
{
    public interface ISolarSystemService
    {
        public Task<List<SolarSystem>> GetAllSolarSystems(string toSearch, List<Guid> guids, int pagination = 50, int skip = 0);

        public Task<bool> CreateSolarSystem(SolarSystem inputSolarSystem);

        public Task<bool> DeleteSolarSystem(string toSearch, List<Guid> guids);

        public Task<bool> UpdateSolarSystem(SolarSystem inputSolarSystem);
    }
}