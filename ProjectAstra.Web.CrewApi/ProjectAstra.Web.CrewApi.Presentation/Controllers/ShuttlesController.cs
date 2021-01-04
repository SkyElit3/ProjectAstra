using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjectAstra.Web.CrewApi.Core.Exceptions;
using ProjectAstra.Web.CrewApi.Core.Interfaces;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
    public class ShuttlesController
    {
        private readonly IShuttleService _service;
        private readonly ILogger<ShuttlesController> _logger;

        public ShuttlesController(IShuttleService service, ILogger<ShuttlesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<Shuttle>> GetShuttle([FromQuery] string toSearch, [FromQuery] Guid[] guids,
            [FromQuery] int pagination = 50, [FromQuery] int skip = 0)
        {
            try
            {
                return await _service.GetAllShuttles(toSearch, guids.ToList(),pagination,skip);
            }
            catch (CrewApiException exception)
            {
                exception.LogException(_logger);
                return null;
            }
            catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
            {
                _logger.LogCritical(exception, "Unhandled unexpected exception while retrieving shuttles");
                return null;
            }
        }

        [HttpPost]
        [Route("/[controller]/add")]
        public async Task<bool> AddShuttle([FromBody] List<Shuttle> shuttleList)
        {
            var result = true;
            foreach (var shuttle in shuttleList)
                try
                {
                    await _service.CreateShuttle(shuttle);
                }
                catch (CrewApiException exception)
                {
                    exception.LogException(_logger);
                    result = false;
                }
                catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
                {
                    _logger.LogCritical(exception, $"Unhandled unexpected exception while creating a shuttle: {JsonConvert.SerializeObject(shuttle, Formatting.Indented)}");
                    result = false;
                }

            return result;
        }

        [HttpDelete]
        [Route("/[controller]/delete")]
        public async Task<bool> DeleteShuttle([FromQuery] string toSearch, [FromQuery] Guid[] guids)
        {
            try
            {
                return await _service.DeleteShuttle(toSearch, guids.ToList());
            }
            catch (CrewApiException exception)
            {
                exception.LogException(_logger);
            }
            catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
            {
                _logger.LogCritical(exception, "Unhandled unexpected exception while deleting a shuttle.");
            }

            return false;
        }

        [HttpPut]
        [Route("/[controller]/update")]
        public async Task<bool> UpdateShuttle([FromBody] List<Shuttle> shuttleList)
        {
            var result = true;
            foreach (var shuttle in shuttleList)
                try
                {
                    await _service.UpdateShuttle(shuttle);
                }
                catch (CrewApiException exception)
                {
                    exception.LogException(_logger);
                    result = false;
                }
                catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
                {
                    _logger.LogCritical(exception, $"Unhandled unexpected exception while updating a shuttle: {JsonConvert.SerializeObject(shuttle, Formatting.Indented)}");
                    result = false;
                }

            return result;
        }
    }
}