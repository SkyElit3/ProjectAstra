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
    public class RobotsController
    {
        private readonly IRobotService _service;
        private readonly ILogger<RobotsController> _logger;

        public RobotsController(IRobotService service, ILogger<RobotsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<Robot>> GetRobot([FromQuery] string toSearch, [FromQuery] Guid[] guids,
            [FromQuery] int pagination = 50, [FromQuery] int skip = 0)
        {
            try
            {
                return await _service.GetAllRobots(toSearch, guids.ToList(),pagination,skip);
            }
            catch (CrewApiException exception)
            {
                exception.LogException(_logger);
                return null;
            }
            catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
            {
                _logger.LogCritical(exception, "Unhandled unexpected exception while retrieving robots");
                return null;
            }
        }

        [HttpPost]
        [Route("/[controller]/add")]
        public async Task<bool> AddRobot([FromBody] List<Robot> robotList)
        {
            var result = true;
            foreach (var robot in robotList)
                try
                {
                    await _service.CreateRobot(robot);
                }
                catch (CrewApiException exception)
                {
                    exception.LogException(_logger);
                    result = false;
                }
                catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
                {
                    _logger.LogCritical(exception, $"Unhandled unexpected exception while creating a Robot: {JsonConvert.SerializeObject(robot, Formatting.Indented)}");
                    result = false;
                }

            return result;
        }

        [HttpDelete]
        [Route("/[controller]/delete")]
        public async Task<bool> DeleteRobot([FromQuery] string toSearch, [FromQuery] Guid[] guids)
        {
            try
            {
                return await _service.DeleteRobot(toSearch, guids.ToList());
            }
            catch (CrewApiException exception)
            {
                exception.LogException(_logger);
            }
            catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
            {
                _logger.LogCritical(exception, "Unhandled unexpected exception while deleting a robot.");
            }

            return false;
        }

        [HttpPut]
        [Route("/[controller]/update")]
        public async Task<bool> UpdateRobot([FromBody] List<Robot> robotList)
        {
            var result = true;
            foreach (var robot in robotList)
                try
                {
                    await _service.UpdateRobot(robot);
                }
                catch (CrewApiException exception)
                {
                    exception.LogException(_logger);
                    result = false;
                }
                catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
                {
                    _logger.LogCritical(exception, $"Unhandled unexpected exception while updating a Robot: {JsonConvert.SerializeObject(robot, Formatting.Indented)}");
                    result = false;
                }

            return result;
        }
    }
}