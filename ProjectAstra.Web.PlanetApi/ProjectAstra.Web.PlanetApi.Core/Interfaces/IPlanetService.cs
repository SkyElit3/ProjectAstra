using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectAstra.Web.PlanetApi.Core.Models;

namespace ProjectAstra.Web.PlanetApi.Core.Interfaces
{
    public interface IPlanetService
    {
        public Task<List<Planet>> GetAllPlanets(string toSearch, List<Guid> guids,Guid shuttleId = new Guid(), int pagination = 50, int skip = 0);

        public Task<bool> CreatePlanet(Planet inputPlanet);

        public Task<bool> DeletePlanet(string toSearch, List<Guid> guids);

        public Task<bool> UpdatePlanet(Planet inputPlanet);
    }
}