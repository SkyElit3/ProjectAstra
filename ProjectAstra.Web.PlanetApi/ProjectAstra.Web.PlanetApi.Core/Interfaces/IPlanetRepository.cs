using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectAstra.Web.PlanetApi.Core.Filters;
using ProjectAstra.Web.PlanetApi.Core.Models;

namespace ProjectAstra.Web.PlanetApi.Core.Interfaces
{
    public interface IPlanetRepository
    {
        public Task<List<Planet>> GetAllPlanets(PlanetFilter filter, int pagination = 50, int skip = 0);

        public Task<bool> CreatePlanet(Planet inputPlanet);

        public Task<bool> DeletePlanet(Guid id);

        public Task<bool> UpdatePlanet(Planet inputPlanet);
    }
}