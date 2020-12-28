using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectAstra.Web.CrewApi.Core.Enums;
using ProjectAstra.Web.CrewApi.Core.Exceptions;
using ProjectAstra.Web.CrewApi.Core.Filters;
using ProjectAstra.Web.CrewApi.Core.Interfaces;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Services
{
    public class ShuttleService : IShuttleService
    {
        private readonly IShuttleRepository _repository;
        private readonly IShuttleValidator _shuttleValidator;
        private const int MaxPagination = 9999;

        public ShuttleService(IShuttleRepository repository, IShuttleValidator shuttleValidator)
        {
            _repository = repository;
            _shuttleValidator = shuttleValidator;
        }

        public async Task<List<Shuttle>> GetAllShuttles(string toSearch, List<Guid> guids, int pagination = 50, int skip = 0)
        {
            return await _repository.GetAllShuttles(new ShuttleFilter
            {
                ToSearch = toSearch,
                Guids = guids
            },pagination,skip);
        }

        public async Task<bool> CreateShuttle(Shuttle inputShuttle)
        {
            _shuttleValidator.Validate(inputShuttle);
            var nameAlikeShuttles = await _repository.GetAllShuttles(new ShuttleFilter
            {
                ToSearch = inputShuttle.Name,
            },MaxPagination);
            var idAlikeShuttles = await _repository.GetAllShuttles(new ShuttleFilter
            {
                Guids = new List<Guid>() {inputShuttle.Id}
            },MaxPagination);
            if (idAlikeShuttles.Any())
                throw new CrewApiException
                {
                    Message = "Shuttle already exists in the repository !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };
            if (nameAlikeShuttles.Any())
                throw new CrewApiException
                {
                    Message = "Shuttle name is not unique !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            return await _repository.CreateShuttle(inputShuttle);
        }

        public async Task<bool> DeleteShuttle(string toSearch, List<Guid> guids)
        {
            var shuttlesToDelete = await _repository.GetAllShuttles(new ShuttleFilter
            {
                ToSearch = toSearch,
                Guids = guids
            },MaxPagination);
            if (!shuttlesToDelete.Any())
                throw new CrewApiException
                {
                    Message = "Shuttle is not in the repository.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };
            var result = true;
            foreach(var shuttle in shuttlesToDelete)
                result = result && await _repository.DeleteShuttle(shuttle.Id);
            return result;
        }

        public async Task<Shuttle> UpdateShuttle(Shuttle inputShuttle)
        {
            _shuttleValidator.Validate(inputShuttle);
            var sameNameShuttles = await _repository.GetAllShuttles(new ShuttleFilter
            {
                ToSearch = inputShuttle.Name,
                PerfectMatch = true
            },MaxPagination);
            if (sameNameShuttles.Any(shuttle => shuttle.Id != inputShuttle.Id))
                throw new CrewApiException
                {
                    Message = "Cannot update shuttle name to one that already exists.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };
            
            var sameIdShuttles = await _repository.GetAllShuttles(new ShuttleFilter
            {
                Guids = new List<Guid>(){inputShuttle.Id}
            },MaxPagination);
            if (sameIdShuttles.Count == 1)
                return await _repository.UpdateShuttle(inputShuttle);

            throw new CrewApiException
            {
                Message = "Cannot update non-existent shuttle.",
                Severity = ExceptionSeverity.Error,
                Type = ExceptionType.ServiceException
            };
        }
    }
}