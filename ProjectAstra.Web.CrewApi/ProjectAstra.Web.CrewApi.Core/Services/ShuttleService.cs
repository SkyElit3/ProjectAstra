using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using ProjectAstra.Web.CrewApi.Core.Interfaces;
using ProjectAstra.Web.CrewApi.Core.Models;
using ProjectAstra.Web.CrewApi.Core.Validators;

namespace ProjectAstra.Web.CrewApi.Core.Services
{
    public class ShuttleService : IShuttleService
    {
        private IShuttleRepo _repository;
        private readonly ILogger _logger;

        public ShuttleService(IShuttleRepo repository, ILogger<ShuttleService> logger)
        {
            _repository = repository;
            _logger = logger;
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
            try
            {
                var validator = new ShuttleValidator(inputShuttle);
                validator.ValidateName().ValidateMaxCrewCapacity();
                return await _repository.CreateShuttle(inputShuttle);
            }
            catch (Exception exception)
            {
                _logger.LogError("Validation exception : " + exception.Message);
                return new Shuttle();
            }
        }

        public async Task<Shuttle> DeleteShuttle(Guid id)
        {
            try
            {
                var result = await _repository.DeleteShuttle(id);
                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError("Repository exception : " + exception.Message);
                return new Shuttle();
            }
        }

        public async Task<Shuttle> UpdateShuttle(Shuttle inputShuttle)
        {
            // TODO: validate the shuttle and the operation
            return await _repository.UpdateShuttle(inputShuttle);
        }
    }
}