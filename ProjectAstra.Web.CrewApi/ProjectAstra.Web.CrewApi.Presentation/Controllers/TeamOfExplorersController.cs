using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectAstra.Web.CrewApi.Core.Exceptions;
using ProjectAstra.Web.CrewApi.Core.Interfaces;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
    public class TeamsOfExplorersController
    {
        private readonly ITeamOfExplorersService _service;
        private readonly ILogger<TeamsOfExplorersController> _logger;

        public TeamsOfExplorersController(ITeamOfExplorersService service, ILogger<TeamsOfExplorersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<TeamOfExplorers>> GetTeamOfExplorers([FromQuery] string toSearch, [FromQuery] Guid[] guids,
            [FromQuery] int pagination = 50, [FromQuery] int skip = 0)
        {
            try
            {
                return await _service.GetAllTeamsOfExplorers(toSearch, guids.ToList(),pagination,skip);
            }
            catch (CrewApiException exception)
            {
                exception.LogException(_logger);
                return null;
            }
            catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
            {
                _logger.LogCritical(exception, "Unhandled unexpected exception while retrieving teams of explorers");
                return null;
            }
        }

        [HttpPost]
        [Route("/[controller]/add")]
        public async Task<bool> AddTeamOfExplorers([FromBody] List<TeamOfExplorers> teamOfExplorersList)
        {
            var result = true;
            foreach (var team in teamOfExplorersList)
                try
                {
                    await _service.CreateTeamOfExplorers(team);
                }
                catch (CrewApiException exception)
                {
                    exception.LogException(_logger);
                    result = false;
                }
                catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
                {
                    _logger.LogCritical(exception, "Unhandled unexpected exception while creating a team of explorers");
                    result = false;
                }

            return result;
        }

        [HttpDelete]
        [Route("/[controller]/delete")]
        public async Task<bool> DeleteTeamOfExplorers([FromQuery] string toSearch, [FromQuery] Guid[] guids)
        {
            try
            {
                return await _service.DeleteTeamOfExplorers(toSearch, guids.ToList());
            }
            catch (CrewApiException exception)
            {
                exception.LogException(_logger);
            }
            catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
            {
                _logger.LogCritical(exception, "Unhandled unexpected exception while deleting a team of explorers.");
            }

            return false;
        }

        [HttpPut]
        [Route("/[controller]/update")]
        public async Task<List<TeamOfExplorers>> UpdateTeamOfExplorers([FromBody] List<TeamOfExplorers> teamOfExplorersList)
        {
            var result = new List<TeamOfExplorers>();
            foreach (var team in teamOfExplorersList)
                try
                {
                    result.Add(await _service.UpdateTeamOfExplorers(team));
                }
                catch (CrewApiException exception)
                {
                    exception.LogException(_logger);
                }
                catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
                {
                    _logger.LogCritical(exception, "Unhandled unexpected exception while updating a team of explorers");
                }

            return result;
        }
    }
}