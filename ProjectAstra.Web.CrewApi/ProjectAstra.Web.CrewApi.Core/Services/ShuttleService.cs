using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectAstra.Web.CrewApi.Core.Interfaces;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Services
{
    public class ShuttleService : IShuttleService
    {
        private IShuttleRepo _repository;

        public ShuttleService(IShuttleRepo repository)
        {
            _repository = repository;
        }

        public async Task<Shuttle> GetShuttle(Guid id)
        {
            return await _repository.GetShuttle(id);
        }

        public async Task<List<Shuttle>> GetAllShuttles()
        {
            return await _repository.GetAllShuttles();
        }

        public async Task<Shuttle> CreateShuttle(Shuttle inputShuttle)
        {
            // TODO: validate the shuttle
            return await _repository.CreateShuttle(inputShuttle);
        }

        public async Task<Shuttle> DeleteShuttle(Guid id)
        {
            // TODO: validate the operation
            return await _repository.DeleteShuttle(id);
        }

        public async Task<Shuttle> UpdateShuttle(Shuttle inputShuttle)
        {
            // TODO: validate the shuttle and the operation
            return await _repository.UpdateShuttle(inputShuttle);
        }
    }
}