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
    public class SolarSystemService : ISolarSystemService
    {
        private readonly ISolarSystemRepository _repository;
        private readonly ISolarSystemValidator _solarSystemValidator;

        public SolarSystemService(ISolarSystemRepository repository, ISolarSystemValidator solarSystemValidator)
        {
            _repository = repository;
            _solarSystemValidator = solarSystemValidator;
        }

        public async Task<List<SolarSystem>> GetAllSolarSystems(string toSearch, List<Guid> guids, int pagination = 50,
            int skip = 0)
        {
            return await _repository.GetAllSolarSystems(
                new SolarSystemFilter {ToSearch = toSearch, Ids = guids}, pagination, skip);
        }

        public async Task<bool> CreateSolarSystem(SolarSystem inputSolarSystem)
        {
            _solarSystemValidator.Validate(inputSolarSystem);

            var sameIdSolarSystems = await _repository.GetAllSolarSystems(
                new SolarSystemFilter {Ids = new List<Guid> {inputSolarSystem.Id}});

            if (EnumerableExtensions.Any(sameIdSolarSystems))
                throw new PlanetApiException
                {
                    ExceptionMessage = $"Solar System with id {inputSolarSystem.Id} exists in the repository !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            var sameNameSolarSystems = await _repository.GetAllSolarSystems(
                new SolarSystemFilter {ToSearch = inputSolarSystem.Name, PerfectMatch = true});

            if (EnumerableExtensions.Any(sameNameSolarSystems))
                throw new PlanetApiException
                {
                    ExceptionMessage = $"Solar System name {inputSolarSystem.Name} is not unique !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            return await _repository.CreateSolarSystem(inputSolarSystem);
        }

        public async Task<bool> DeleteSolarSystem(string toSearch, List<Guid> guids)
        {
            var solarSystemsToDelete = await _repository.GetAllSolarSystems(
                new SolarSystemFilter {ToSearch = toSearch, Ids = guids, PerfectMatch = true});
            
            if(!EnumerableExtensions.Any(solarSystemsToDelete))
                throw new PlanetApiException
                {
                    ExceptionMessage = $"Solar System is not in the repository !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };
            foreach (var solarSystem in solarSystemsToDelete)
            {
                // todo : check if any planets are in the deleted solar system
                await _repository.DeleteSolarSystem(solarSystem.Id);
            }

            return true;
        }

        public async Task<bool> UpdateSolarSystem(SolarSystem inputSolarSystem)
        {
            _solarSystemValidator.Validate(inputSolarSystem);

            var sameIdSolarSystems = await _repository.GetAllSolarSystems(
                new SolarSystemFilter {Ids = new List<Guid> {inputSolarSystem.Id}});
            if(sameIdSolarSystems.Count != 1)
                throw new PlanetApiException
                {
                    ExceptionMessage = $"Solar System with id {inputSolarSystem.Id} to update has not been found !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };
            if (inputSolarSystem.Name != sameIdSolarSystems.First().Name)
            {
                var sameNameSolarSystems = await _repository.GetAllSolarSystems(
                    new SolarSystemFilter {ToSearch = inputSolarSystem.Name, PerfectMatch = true});
                if(sameNameSolarSystems.Any())
                    throw new PlanetApiException
                    {
                        ExceptionMessage = $"Cannot update solar system name to one that already exists : {inputSolarSystem.Name}.",
                        Severity = ExceptionSeverity.Error,
                        Type = ExceptionType.ServiceException
                    };
            }

            var solarSystem = sameIdSolarSystems.First();
            solarSystem.UpdateByReflection(inputSolarSystem);
            return await _repository.UpdateSolarSystem(solarSystem);
        }
    }
}