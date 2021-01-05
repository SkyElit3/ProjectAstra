using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectAstra.Web.CrewApi.Core.Exceptions;
using ProjectAstra.Web.CrewApi.Core.Interfaces;

namespace ProjectAstra.Web.CrewApi.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExportsController
    {
        private readonly ILogger<ExportsController> _logger;
        private readonly IShuttleService _shuttleService;

        public ExportsController(ILogger<ExportsController> logger, IShuttleService shuttleService)
        {
            _logger = logger;
            _shuttleService = shuttleService;
        }
        
        [HttpGet]
        public async Task<int> GetShuttleCount([FromQuery] Guid shuttleId)
        {
            try
            {
                return (await _shuttleService.GetAllShuttles(toSearch: new string(""), guids: new List<Guid> {shuttleId})).Count;
            }
            catch (CrewApiException exception)
            {
                exception.LogException(_logger);
                return -1;
            }
            catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
            {
                _logger.LogCritical(exception, "Unhandled unexpected exception while retrieving shuttles count");
                return -1;
            }
        }
    }
}