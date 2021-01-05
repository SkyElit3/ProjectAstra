using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using ProjectAstra.Web.PlanetApi.Core.Enums;
using ProjectAstra.Web.PlanetApi.Core.Exceptions;
using ProjectAstra.Web.PlanetApi.Core.Extensions;
using ProjectAstra.Web.PlanetApi.Core.Filters;
using ProjectAstra.Web.PlanetApi.Core.Interfaces;
using ProjectAstra.Web.PlanetApi.Core.Models;

namespace ProjectAstra.Web.PlanetApi.Core.Services
{
    public class PlanetService : IPlanetService
    {
        private readonly IPlanetRepository _repository;
        private readonly IPlanetValidator _planetValidator;
        private readonly ISolarSystemRepository _solarSystemRepository;

        public PlanetService(IPlanetRepository repository, IPlanetValidator planetValidator,
            ISolarSystemRepository solarSystemRepository)
        {
            _repository = repository;
            _planetValidator = planetValidator;
            _solarSystemRepository = solarSystemRepository;
        }

        public async Task<List<Planet>> GetAllPlanets(string toSearch, List<Guid> guids, int pagination = 50,
            int skip = 0)
        {
            return await _repository.GetAllPlanets(
                new PlanetFilter {ToSearch = toSearch, Ids = guids}, pagination, skip);
        }

        public async Task<bool> CreatePlanet(Planet inputPlanet)
        {
            _planetValidator.Validate(inputPlanet);

            var sameIdPlanets = await _repository.GetAllPlanets(
                new PlanetFilter {Ids = new List<Guid> {inputPlanet.Id}});

            if (EnumerableExtensions.Any(sameIdPlanets))
                throw new PlanetApiException
                {
                    ExceptionMessage = $"Planet with id {inputPlanet.Id} exists in the repository !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            var sameNamePlanets = await _repository.GetAllPlanets(
                new PlanetFilter {ToSearch = inputPlanet.Name, PerfectMatch = true});

            if (EnumerableExtensions.Any(sameNamePlanets))
                throw new PlanetApiException
                {
                    ExceptionMessage = $"Planet name {inputPlanet.Name} is not unique !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            var sameIdSolarSystems = await _solarSystemRepository.GetAllSolarSystems(
                new SolarSystemFilter {Ids = new List<Guid> {inputPlanet.SolarSystemId}});
            if (!sameIdSolarSystems.Any())
                throw new PlanetApiException
                {
                    ExceptionMessage =
                        $"Planet's solar system id {inputPlanet.SolarSystemId} does not correspond to a solar system !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            return await _repository.CreatePlanet(inputPlanet);
        }

        public async Task<bool> DeletePlanet(string toSearch, List<Guid> guids)
        {
            var planetsToDelete = await _repository.GetAllPlanets(
                new PlanetFilter {ToSearch = toSearch, Ids = guids, PerfectMatch = true});

            if (!EnumerableExtensions.Any(planetsToDelete))
                throw new PlanetApiException
                {
                    ExceptionMessage = $"Planet is not in the repository !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };
            foreach (var planet in planetsToDelete)
            {
                // todo: check if planet is currently visited by a team of explorers
                await _repository.DeletePlanet(planet.Id);
            }

            return true;
        }

        public async Task<bool> UpdatePlanet(Planet inputPlanet)
        {
            _planetValidator.Validate(inputPlanet);

            var sameIdPlanets = await _repository.GetAllPlanets(
                new PlanetFilter {Ids = new List<Guid> {inputPlanet.Id}});

            if (sameIdPlanets.Count != 1)
                throw new PlanetApiException
                {
                    ExceptionMessage = $"Planet with id {inputPlanet.Id} to update has not been found !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            if (inputPlanet.Name != sameIdPlanets.First().Name)
            {
                var sameNamePlanets = await _repository.GetAllPlanets(
                    new PlanetFilter {ToSearch = inputPlanet.Name, PerfectMatch = true});
                if (sameNamePlanets.Any())
                    throw new PlanetApiException
                    {
                        ExceptionMessage =
                            $"Cannot update Planet name to one that already exists : {inputPlanet.Name}.",
                        Severity = ExceptionSeverity.Error,
                        Type = ExceptionType.ServiceException
                    };
            }

            var sameIdSolarSystems = await _solarSystemRepository.GetAllSolarSystems(
                new SolarSystemFilter {Ids = new List<Guid> {inputPlanet.SolarSystemId}});

            if (!sameIdSolarSystems.Any())
                throw new PlanetApiException
                {
                    ExceptionMessage =
                        $"Cannot update Planet with inexistent solar system id {inputPlanet.SolarSystemId}.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            var planet = sameIdPlanets.First();
            planet.UpdateByReflection(inputPlanet);
            return await _repository.UpdatePlanet(planet);
        }
    }
}