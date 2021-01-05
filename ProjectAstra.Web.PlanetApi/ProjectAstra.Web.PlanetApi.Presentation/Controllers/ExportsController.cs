using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjectAstra.Web.PlanetApi.Core.Exceptions;
using ProjectAstra.Web.PlanetApi.Core.Interfaces;
using ProjectAstra.Web.PlanetApi.Core.Models;

namespace ProjectAstra.Web.PlanetApi.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExportsController
    {
        private readonly ILogger<ExportsController> _logger;
        private readonly IPlanetService _planetService;

        public ExportsController(ILogger<ExportsController> logger, IPlanetService planetService)
        {
            _logger = logger;
            _planetService = planetService;
        }

        [HttpGet]
        public async Task<Guid> GetPlanetByShuttleId([FromQuery] Guid shuttleId)
        {
            try
            {
                var planet = (await _planetService.GetAllPlanets(new string(""), new List<Guid>(), shuttleId))
                    .First();
                if (planet != null)
                    return planet.Id;
            }
            catch (PlanetApiException exception)
            {
                exception.LogException(_logger);
                return new Guid();
            }
            catch (Exception exception) when (exception.GetType() != typeof(PlanetApiException))
            {
                _logger.LogCritical(exception, "Unhandled unexpected exception while retrieving data in exports");
                return new Guid();
            }

            return new Guid();
        }

        [HttpPut]
        [Route("/[controller]/update")]
        public async Task<bool> UpdatePlanet([FromQuery] Guid shuttleId, [FromQuery] Guid planetId,
            [FromQuery] int numberOfRobotsDelta)
        {
            try
            {
                if (shuttleId != Guid.Empty) // provide shuttleId to do anything
                {
                    if (planetId != Guid.Empty) // provide planetId to visit a planet and numberOfRobots if wanted
                    {
                        var planet = (await _planetService.GetAllPlanets(new string(""), new List<Guid> {planetId}))
                            .First();
                        planet.ShuttleId = shuttleId;
                        if (numberOfRobotsDelta != 0)
                            planet.NumberOfRobots = planet.NumberOfRobots + numberOfRobotsDelta;
                        await _planetService.UpdatePlanet(planet);
                    }
                    else // don't provide a planetId to un-visit a planet
                    {
                        var planet =
                            (await _planetService.GetAllPlanets(new string(""), new List<Guid>(), shuttleId)).First();
                        planet.ShuttleId = Guid.Empty;
                        planet.NumberOfRobots = 0;
                        await _planetService.UpdatePlanet(planet);
                    }
                }
            }
            catch (PlanetApiException exception)
            {
                exception.LogException(_logger);
                return false;
            }
            catch (Exception exception) when (exception.GetType() != typeof(PlanetApiException))
            {
                _logger.LogCritical(exception,
                    $"Unhandled unexpected exception while updating a Planet in exports controller");
                return false;
            }

            return false;
        }
    }
}