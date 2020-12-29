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
    public class TeamOfExplorersService : ITeamOfExplorersService
    {
        private readonly ITeamOfExplorersRepository _repository;
        private readonly IShuttleRepository _shuttleRepository;
        private readonly ITeamOfExplorersValidator _teamOfExplorersValidator;
        private const int MaxPagination = 9999;

        public TeamOfExplorersService(ITeamOfExplorersRepository repository,
            ITeamOfExplorersValidator teamOfExplorersValidator, IShuttleRepository shuttleRepository)
        {
            _repository = repository;
            _teamOfExplorersValidator = teamOfExplorersValidator;
            _shuttleRepository = shuttleRepository;
        }

        public async Task<List<TeamOfExplorers>> GetAllTeamsOfExplorers(string toSearch, List<Guid> guids,
            int pagination = 50, int skip = 0)
        {
            return await _repository.GetAllTeamsOfExplorers(new TeamOfExplorersFilter
            {
                ToSearch = toSearch,
                Guids = guids
            }, pagination, skip);
        }

        public async Task<bool> CreateTeamOfExplorers(TeamOfExplorers inputTeamOfExplorers)
        {
            _teamOfExplorersValidator.Validate(inputTeamOfExplorers);

            var shuttles = await _shuttleRepository.GetAllShuttles(
                new ShuttleFilter {Guids = new List<Guid>() {inputTeamOfExplorers.ShuttleId}}, MaxPagination);
            if (shuttles.Count != 1)
                throw new CrewApiException
                {
                    Message = "TeamOfExplorers's Shuttle id does not correspond to any shuttle !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };
            if (shuttles.First().TeamOfExplorers != null)
                throw new CrewApiException
                {
                    Message = "TeamOfExplorers's Shuttle id has already been assigned to another team !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            var nameAlikeTeamsOfExplorers = await _repository.GetAllTeamsOfExplorers(new TeamOfExplorersFilter
            {
                ToSearch = inputTeamOfExplorers.Name,
            }, MaxPagination);
            if (nameAlikeTeamsOfExplorers.Any())
                throw new CrewApiException
                {
                    Message = "TeamOfExplorers name is not unique !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            var idAlikeTeamsOfExplorers = await _repository.GetAllTeamsOfExplorers(new TeamOfExplorersFilter
            {
                Guids = new List<Guid>() {inputTeamOfExplorers.Id}
            }, MaxPagination);
            if (idAlikeTeamsOfExplorers.Any())
                throw new CrewApiException
                {
                    Message = "TeamOfExplorers already exists in the repository !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            return await _repository.CreateTeamOfExplorers(inputTeamOfExplorers);
        }

        public async Task<bool> DeleteTeamOfExplorers(string toSearch, List<Guid> guids)
        {
            var teamsToDelete = await _repository.GetAllTeamsOfExplorers(new TeamOfExplorersFilter
            {
                ToSearch = toSearch,
                Guids = guids,
                PerfectMatch = true
            }, MaxPagination);
            if (!teamsToDelete.Any())
                throw new CrewApiException
                {
                    Message = "TeamOfExplorers is not in the repository.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };
            var result = true;
            foreach (var team in teamsToDelete)
                result = result && await _repository.DeleteTeamOfExplorers(team.Id);
            return result;
        }

        public async Task<TeamOfExplorers> UpdateTeamOfExplorers(TeamOfExplorers inputTeamOfExplorers)
        {
            _teamOfExplorersValidator.Validate(inputTeamOfExplorers);
            var shuttles = await _shuttleRepository.GetAllShuttles(
                new ShuttleFilter {Guids = new List<Guid>() {inputTeamOfExplorers.ShuttleId}}, MaxPagination);
            if (shuttles.Count != 1)
                throw new CrewApiException
                {
                    Message = "TeamOfExplorers's Shuttle id does not correspond to any shuttle !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };
            if (shuttles.First().TeamOfExplorers != null)
                if (shuttles.First().TeamOfExplorers.Id != inputTeamOfExplorers.Id)
                    throw new CrewApiException
                    {
                        Message = "TeamOfExplorers's Shuttle id has already been assigned to another team !",
                        Severity = ExceptionSeverity.Error,
                        Type = ExceptionType.ServiceException
                    };


            var sameNameTeamsOfExplorers = await _repository.GetAllTeamsOfExplorers(new TeamOfExplorersFilter
            {
                ToSearch = inputTeamOfExplorers.Name,
                PerfectMatch = true
            }, MaxPagination);
            if (sameNameTeamsOfExplorers.Any(team => team.Id != inputTeamOfExplorers.Id))
                throw new CrewApiException
                {
                    Message = "Cannot update team name to one that already exists.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            var sameIdTeamsOfExplorers = await _repository.GetAllTeamsOfExplorers(new TeamOfExplorersFilter
            {
                Guids = new List<Guid>() {inputTeamOfExplorers.Id}
            }, MaxPagination);
            if (sameIdTeamsOfExplorers.Count == 1)
                return await _repository.UpdateTeamOfExplorers(inputTeamOfExplorers);

            throw new CrewApiException
            {
                Message = "Cannot update non-existent team.",
                Severity = ExceptionSeverity.Error,
                Type = ExceptionType.ServiceException
            };
        }
    }
}