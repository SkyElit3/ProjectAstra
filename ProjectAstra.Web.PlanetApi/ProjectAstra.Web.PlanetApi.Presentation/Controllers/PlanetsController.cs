using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
    public class PlanetsController
    {
        private readonly IPlanetService _service;
        private readonly ILogger<PlanetsController> _logger;

        public PlanetsController(IPlanetService service, ILogger<PlanetsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<Planet>> GetPlanet([FromQuery] string toSearch, [FromQuery] Guid[] guids,
            [FromQuery] int pagination = 50, [FromQuery] int skip = 0)
        {
            try
            {
                return await _service.GetAllPlanets(toSearch, guids.ToList(), pagination, skip);
            }
            catch (PlanetApiException exception)
            {
                exception.LogException(_logger);
                return null;
            }
            catch (Exception exception) when (exception.GetType() != typeof(PlanetApiException))
            {
                _logger.LogCritical(exception, "Unhandled unexpected exception while retrieving Planets");
                return null;
            }
        }

        [HttpPost]
        [Route("/[controller]/add")]
        public async Task<bool> AddPlanet([FromBody] List<Planet> planetList)
        {
            var result = true;
            foreach (var planet in planetList)
                try
                {
                    await _service.CreatePlanet(planet);
                }
                catch (PlanetApiException exception)
                {
                    exception.LogException(_logger);
                    result = false;
                }
                catch (Exception exception) when (exception.GetType() != typeof(PlanetApiException))
                {
                    _logger.LogCritical(exception,
                        $"Unhandled unexpected exception while creating a Planet: {JsonConvert.SerializeObject(planet, Formatting.Indented)}");
                    result = false;
                }

            return result;
        }

        [HttpDelete]
        [Route("/[controller]/delete")]
        public async Task<bool> DeletePlanet([FromQuery] string toSearch, [FromQuery] Guid[] guids)
        {
            try
            {
                return await _service.DeletePlanet(toSearch, guids.ToList());
            }
            catch (PlanetApiException exception)
            {
                exception.LogException(_logger);
            }
            catch (Exception exception) when (exception.GetType() != typeof(PlanetApiException))
            {
                _logger.LogCritical(exception, "Unhandled unexpected exception while deleting a planet.");
            }

            return false;
        }

        [HttpPut]
        [Route("/[controller]/update")]
        public async Task<bool> UpdatePlanet([FromBody] List<Planet> planetList)
        {
            var result = true;
            foreach (var planet in planetList)
                try
                {
                    await _service.UpdatePlanet(planet);
                }
                catch (PlanetApiException exception)
                {
                    exception.LogException(_logger);
                    result = false;
                }
                catch (Exception exception) when (exception.GetType() != typeof(PlanetApiException))
                {
                    _logger.LogCritical(exception,
                        $"Unhandled unexpected exception while updating a Planet: {JsonConvert.SerializeObject(planet, Formatting.Indented)}");
                    result = false;
                }

            return result;
        }
    }
}