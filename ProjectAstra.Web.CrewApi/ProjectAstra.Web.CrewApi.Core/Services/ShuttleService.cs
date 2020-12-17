using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProjectAstra.Web.CrewApi.Core.Enums;
using ProjectAstra.Web.CrewApi.Core.Exceptions;
using ProjectAstra.Web.CrewApi.Core.Interfaces;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Services
{
    public class ShuttleService : IShuttleService
    {
        private readonly IShuttleRepository _repository;
        private readonly IShuttleValidator _shuttleValidator;
        private readonly ILogger<ShuttleService> _logger;

        public ShuttleService(IShuttleRepository repository, ILogger<ShuttleService> logger,
            IShuttleValidator shuttleValidator)
        {
            _repository = repository;
            _logger = logger;
            _shuttleValidator = shuttleValidator;
        }

        public async Task<Shuttle> GetShuttle(Guid id)
        {
            try
            {
                var allShuttles = _repository.GetAllShuttles();
                if ((await allShuttles).Any(shuttle => shuttle.Id == id))
                    return (await allShuttles).First((shuttle => shuttle.Id == id));
                else
                    throw new CrewApiException("Shuttle does not exist in the repository !",
                        ExceptionTypeEnum.RepositoryException);
            }
            catch (CrewApiException exception)
            {
                _logger.LogError(exception.InnerException, exception.TypeEnum + " : " + exception.Message);
                return null;
            }
            catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
            {
                _logger.LogCritical(exception, "Unhandled unexpected exception while creating a shuttle");
                return null;
            }
        }

        public async Task<List<Shuttle>> GetAllShuttles()
        {
            return await _repository.GetAllShuttles();
        }

        public async Task<bool> CreateShuttle(Shuttle inputShuttle)
        {
            try
            {
                _shuttleValidator.Validate(inputShuttle);
                var allShuttles = _repository.GetAllShuttles();
                if ((await allShuttles).Any(shuttle =>
                    shuttle.Id == inputShuttle.Id || shuttle.Name == inputShuttle.Name))
                    throw new CrewApiException("Shuttle already exists in the repository or name is not unique !",
                        ExceptionTypeEnum.RepositoryException);
                else
                    return await _repository.CreateShuttle(inputShuttle);
            }
            catch (CrewApiException exception)
            {
                _logger.LogError(exception.InnerException, exception.TypeEnum + " : " + exception.Message);
                return false;
            }
            catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
            {
                _logger.LogCritical(exception, "Unhandled unexpected exception while creating a shuttle");
                return false;
            }
        }

        public async Task<bool> DeleteShuttle(Guid id)
        {
            try
            {
                var response = (await _repository.GetAllShuttles()).FirstOrDefault(t => t.Id == id);
                if (response == null || response.Id == Guid.Empty)
                    throw new CrewApiException("Shuttle is not in the repository.",
                        ExceptionTypeEnum.RepositoryException);
                return await _repository.DeleteShuttle(id);
            }
            catch (CrewApiException exception)
            {
                _logger.LogError(exception.InnerException, exception.TypeEnum + " : " + exception.Message);
                return false;
            }
            catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
            {
                _logger.LogCritical(exception, "Unhandled unexpected exception while deleting a shuttle.");
                return false;
            }
        }

        public async Task<Shuttle> UpdateShuttle(Shuttle inputShuttle)
        {
            try
            {
                _shuttleValidator.Validate(inputShuttle);
                var allShuttles = _repository.GetAllShuttles();
                if ((await allShuttles).Any(shuttle =>
                    shuttle.Name == inputShuttle.Name && shuttle.Id != inputShuttle.Id))
                    throw new CrewApiException("Cannot update shuttle name to one that already exists.",
                        ExceptionTypeEnum.RepositoryException);
                if ((await allShuttles).Any(shuttle => shuttle.Id == inputShuttle.Id))
                    return await _repository.UpdateShuttle(inputShuttle);
                else
                    throw new CrewApiException("Cannot update non-existent shuttle.",
                        ExceptionTypeEnum.RepositoryException);
            }
            catch (CrewApiException exception)
            {
                _logger.LogError(exception.InnerException, exception.TypeEnum + " : " + exception.Message);
                return new Shuttle();
            }
            catch (Exception exception) when (exception.GetType() != typeof(CrewApiException))
            {
                _logger.LogCritical(exception, "Unhandled unexpected exception while updating a shuttle");
                return new Shuttle();
            }
        }

        // TODO : Create/Update/Delete a list of shuttles all at once
    }
}