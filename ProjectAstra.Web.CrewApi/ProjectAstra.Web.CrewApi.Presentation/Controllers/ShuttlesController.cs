using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectAstra.Web.CrewApi.Core.Interfaces;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShuttlesController
    {
        private readonly IShuttleService _service;

        public ShuttlesController(IShuttleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<Shuttle>> GetShuttle([FromQuery] string guidData)
        {
            if (Guid.TryParse(guidData, out var guid))
                return new List<Shuttle> {await _service.GetShuttle(guid)};
            else
                return await _service.GetAllShuttles();
        }

        [HttpPost]
        [Route("/[controller]/add")]
        public async Task<bool> AddShuttle([FromBody] Shuttle inputShuttle)
        {
            return await _service.CreateShuttle(inputShuttle);
        }

        [HttpDelete]
        [Route("/[controller]/delete")]
        public async Task<bool> DeleteShuttle([FromQuery] string guidData)
        {
            if (Guid.TryParse(guidData, out var guid))
                return await _service.DeleteShuttle(guid);
            return false;
        }

        [HttpPut]
        [Route("/[controller]/update")]
        public async Task<Shuttle> UpdateShuttle([FromBody] Shuttle inputShuttle)
        {
            return await _service.UpdateShuttle(inputShuttle);
        }
    }
}