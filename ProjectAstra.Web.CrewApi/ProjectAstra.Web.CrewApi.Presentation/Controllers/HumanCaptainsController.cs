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
    public class HumanCaptainsController
    {
        private readonly IHumanCaptainService _service;
        private readonly ILogger<HumanCaptainsController> _logger;

        public HumanCaptainsController(IHumanCaptainService service, ILogger<HumanCaptainsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<HumanCaptain>> GetHumanCaptain([FromQuery] string toSearch, [FromQuery] Guid[] guids,
            [FromQuery] int pagination = 50, [FromQuery] int skip = 0)
        {
            try
            {
                return await _service.GetAllHumanCaptains(toSearch, guids.ToList(),pagination,skip);
            }
            catch (CrewApiException exception)
            {
                exception.LogException(_logger);
                return null;
            }
            catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
            {
                _logger.LogCritical(exception, "Unhandled unexpected exception while retrieving humanCaptains");
                return null;
            }
        }

        [HttpPost]
        [Route("/[controller]/add")]
        public async Task<bool> AddHumanCaptain([FromBody] List<HumanCaptain> humanCaptainList)
        {
            var result = true;
            foreach (var humanCaptain in humanCaptainList)
                try
                {
                    await _service.CreateHumanCaptain(humanCaptain);
                }
                catch (CrewApiException exception)
                {
                    exception.LogException(_logger);
                    result = false;
                }
                catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
                {
                    _logger.LogCritical(exception, $"Unhandled unexpected exception while creating a human captain: {JsonConvert.SerializeObject(humanCaptain, Formatting.Indented)}");
                    result = false;
                }

            return result;
        }

        [HttpDelete]
        [Route("/[controller]/delete")]
        public async Task<bool> DeleteHumanCaptain([FromQuery] string toSearch, [FromQuery] Guid[] guids)
        {
            try
            {
                return await _service.DeleteHumanCaptain(toSearch, guids.ToList());
            }
            catch (CrewApiException exception)
            {
                exception.LogException(_logger);
            }
            catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
            {
                _logger.LogCritical(exception, "Unhandled unexpected exception while deleting a humanCaptain.");
            }

            return false;
        }

        [HttpPut]
        [Route("/[controller]/update")]
        public async Task<bool> UpdateHumanCaptain([FromBody] List<HumanCaptain> humanCaptainList)
        {
            var result = true;
            foreach (var humanCaptain in humanCaptainList)
                try
                {
                    await _service.UpdateHumanCaptain(humanCaptain);
                }
                catch (CrewApiException exception)
                {
                    exception.LogException(_logger);
                    result = false;
                }
                catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
                {
                    _logger.LogCritical(exception, $"Unhandled unexpected exception while updating a human captain: {JsonConvert.SerializeObject(humanCaptain, Formatting.Indented)}");
                    result = false;
                }

            return result;
        }
    }
}