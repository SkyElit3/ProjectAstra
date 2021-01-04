using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectAstra.Web.CrewApi.Core.Enums;
using ProjectAstra.Web.CrewApi.Core.Exceptions;
using ProjectAstra.Web.CrewApi.Core.Extensions;
using ProjectAstra.Web.CrewApi.Core.Filters;
using ProjectAstra.Web.CrewApi.Core.Interfaces;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Services
{
    public class ShuttleService : IShuttleService
    {
        private readonly IShuttleRepository _repository;
        private readonly ITeamOfExplorersRepository _teamOfExplorersRepository;
        private readonly IShuttleValidator _shuttleValidator;

        public ShuttleService(IShuttleRepository repository, IShuttleValidator shuttleValidator, ITeamOfExplorersRepository teamOfExplorersRepository)
        {
            _repository = repository;
            _shuttleValidator = shuttleValidator;
            _teamOfExplorersRepository = teamOfExplorersRepository;
        }

        public async Task<List<Shuttle>> GetAllShuttles(string toSearch, List<Guid> guids, int pagination = 50,
            int skip = 0)
        {
            return await _repository.GetAllShuttles(
                new ShuttleFilter {ToSearch = toSearch, Ids = guids}, pagination, skip);
        }

        public async Task<bool> CreateShuttle(Shuttle inputShuttle)
        {
            _shuttleValidator.Validate(inputShuttle);

            var sameIdShuttles = await _repository.GetAllShuttles(
                new ShuttleFilter {Ids = new List<Guid>() {inputShuttle.Id}});

            if (sameIdShuttles.Any())
                throw new CrewApiException
                {
                    Message = $"Shuttle with id {inputShuttle.Id} exists in the repository !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            var nameAlikeShuttles = await _repository.GetAllShuttles(
                new ShuttleFilter {ToSearch = inputShuttle.Name, PerfectMatch = true});

            if (nameAlikeShuttles.Any())
                throw new CrewApiException
                {
                    Message = $"Shuttle name {inputShuttle.Name} is not unique !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            return await _repository.CreateShuttle(inputShuttle);
        }

        public async Task<bool> DeleteShuttle(string toSearch, List<Guid> guids)
        {
            var shuttlesToDelete = await _repository.GetAllShuttles(
                new ShuttleFilter {ToSearch = toSearch, Ids = guids, PerfectMatch = true});

            if (!shuttlesToDelete.Any())
                throw new CrewApiException
                {
                    Message = "Shuttle is not in the repository.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            foreach (var shuttle in shuttlesToDelete)
            {
                var sameShuttleIdTeams = await _teamOfExplorersRepository.GetAllTeamsOfExplorers(
                    new TeamOfExplorersFilter {ShuttleGuid = shuttle.Id});
                if(sameShuttleIdTeams.Any())
                    throw new CrewApiException
                    {
                        Message = $"Teams still contain the shuttle id {shuttle.Id}. Delete them before removing the shuttle.",
                        Severity = ExceptionSeverity.Error,
                        Type = ExceptionType.ServiceException
                    };
                await _repository.DeleteShuttle(shuttle.Id);
            }

            return true;
        }

        public async Task<bool> UpdateShuttle(Shuttle inputShuttle)
        {
            _shuttleValidator.Validate(inputShuttle);

            var sameIdShuttles =
                await _repository.GetAllShuttles(new ShuttleFilter {Ids = new List<Guid> {inputShuttle.Id}});

            if (sameIdShuttles.Count != 1)
                throw new CrewApiException
                {
                    Message = $"Shuttle with id {inputShuttle.Id} to update has not been found.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            if (inputShuttle.Name != sameIdShuttles.First().Name)
            {
                var sameNameShuttles = await _repository.GetAllShuttles(
                    new ShuttleFilter {ToSearch = inputShuttle.Name, PerfectMatch = true});

                if (sameNameShuttles.Any(s => s.Id != inputShuttle.Id))
                    throw new CrewApiException
                    {
                        Message = $"Cannot update shuttle name to one that already exists : {inputShuttle.Name}.",
                        Severity = ExceptionSeverity.Error,
                        Type = ExceptionType.ServiceException
                    };
            }

            var shuttle = sameIdShuttles.First();
            shuttle.UpdateByReflection(inputShuttle);
            return await _repository.UpdateShuttle(shuttle);
        }
    }
}