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
    public class SolarSystemsController
    {
        private readonly ISolarSystemService _service;
        private readonly ILogger<SolarSystemsController> _logger;

        public SolarSystemsController(ISolarSystemService service, ILogger<SolarSystemsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<SolarSystem>> GetSolarSystem([FromQuery] string toSearch, [FromQuery] Guid[] guids,
            [FromQuery] int pagination = 50, [FromQuery] int skip = 0)
        {
            try
            {
                return await _service.GetAllSolarSystems(toSearch, guids.ToList(), pagination, skip);
            }
            catch (PlanetApiException exception)
            {
                exception.LogException(_logger);
                return null;
            }
            catch (Exception exception) when (exception.GetType() != typeof(PlanetApiException))
            {
                _logger.LogCritical(exception, "Unhandled unexpected exception while retrieving SolarSystems");
                return null;
            }
        }

        [HttpPost]
        [Route("/[controller]/add")]
        public async Task<bool> AddSolarSystem([FromBody] List<SolarSystem> solarSystemList)
        {
            var result = true;
            foreach (var solarSystem in solarSystemList)
                try
                {
                    await _service.CreateSolarSystem(solarSystem);
                }
                catch (PlanetApiException exception)
                {
                    exception.LogException(_logger);
                    result = false;
                }
                catch (Exception exception) when (exception.GetType() != typeof(PlanetApiException))
                {
                    _logger.LogCritical(exception,
                        $"Unhandled unexpected exception while creating a SolarSystem: {JsonConvert.SerializeObject(solarSystem, Formatting.Indented)}");
                    result = false;
                }

            return result;
        }

        [HttpDelete]
        [Route("/[controller]/delete")]
        public async Task<bool> DeleteSolarSystem([FromQuery] string toSearch, [FromQuery] Guid[] guids)
        {
            try
            {
                return await _service.DeleteSolarSystem(toSearch, guids.ToList());
            }
            catch (PlanetApiException exception)
            {
                exception.LogException(_logger);
            }
            catch (Exception exception) when (exception.GetType() != typeof(PlanetApiException))
            {
                _logger.LogCritical(exception, "Unhandled unexpected exception while deleting a solarSystem.");
            }

            return false;
        }

        [HttpPut]
        [Route("/[controller]/update")]
        public async Task<bool> UpdateSolarSystem([FromBody] List<SolarSystem> solarSystemList)
        {
            var result = true;
            foreach (var solarSystem in solarSystemList)
                try
                {
                    await _service.UpdateSolarSystem(solarSystem);
                }
                catch (PlanetApiException exception)
                {
                    exception.LogException(_logger);
                    result = false;
                }
                catch (Exception exception) when (exception.GetType() != typeof(PlanetApiException))
                {
                    _logger.LogCritical(exception,
                        $"Unhandled unexpected exception while updating a SolarSystem: {JsonConvert.SerializeObject(solarSystem, Formatting.Indented)}");
                    result = false;
                }

            return result;
        }
    }
}