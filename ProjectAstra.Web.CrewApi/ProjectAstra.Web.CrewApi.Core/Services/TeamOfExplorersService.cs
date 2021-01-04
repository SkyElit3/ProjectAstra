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
    public class TeamOfExplorersService : ITeamOfExplorersService
    {
        private readonly ITeamOfExplorersRepository _repository;
        private readonly IShuttleRepository _shuttleRepository;
        private readonly ITeamOfExplorersValidator _teamOfExplorersValidator;
        private readonly IExplorerRepository _explorerRepository;

        public TeamOfExplorersService(ITeamOfExplorersRepository repository,
            ITeamOfExplorersValidator teamOfExplorersValidator, IShuttleRepository shuttleRepository, IExplorerRepository explorerRepository)
        {
            _repository = repository;
            _teamOfExplorersValidator = teamOfExplorersValidator;
            _shuttleRepository = shuttleRepository;
            _explorerRepository = explorerRepository;
        }

        public async Task<List<TeamOfExplorers>> GetAllTeamsOfExplorers(string toSearch, List<Guid> guids,
            int pagination = 50, int skip = 0)
        {
            return await _repository.GetAllTeamsOfExplorers(new TeamOfExplorersFilter
                {ToSearch = toSearch, Ids = guids}, pagination, skip);
        }

        public async Task<bool> CreateTeamOfExplorers(TeamOfExplorers inputTeamOfExplorers)
        {
            _teamOfExplorersValidator.Validate(inputTeamOfExplorers);

            var sameIdShuttles = await _shuttleRepository.GetAllShuttles(
                new ShuttleFilter {Ids = new List<Guid> {inputTeamOfExplorers.ShuttleId}});

            if (sameIdShuttles.Count != 1)
                throw new CrewApiException
                {
                    Message = $"TeamOfExplorers's Shuttle id {inputTeamOfExplorers.ShuttleId} does not correspond to any shuttle !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            var sameNameTeamsOfExplorers = await _repository.GetAllTeamsOfExplorers(
                new TeamOfExplorersFilter {ToSearch = inputTeamOfExplorers.Name, PerfectMatch = true});

            if (sameNameTeamsOfExplorers.Any())
                throw new CrewApiException
                {
                    Message = $"TeamOfExplorers name {inputTeamOfExplorers.Name} is not unique !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            var sameIdTeamsOfExplorers = await _repository.GetAllTeamsOfExplorers(
                new TeamOfExplorersFilter {Ids = new List<Guid>() {inputTeamOfExplorers.Id}});

            if (sameIdTeamsOfExplorers.Any())
                throw new CrewApiException
                {
                    Message = $"TeamOfExplorers id {inputTeamOfExplorers.Id} already exists in the repository !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };
            
            var sameShuttleIdTeamsOfExplorers = await _repository.GetAllTeamsOfExplorers(
                new TeamOfExplorersFilter {ShuttleGuid = sameIdShuttles.First().Id});

            if (sameShuttleIdTeamsOfExplorers.Any())
                throw new CrewApiException
                {
                    Message = $"TeamOfExplorers's Shuttle id {inputTeamOfExplorers.ShuttleId} has already been assigned to another team !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            return await _repository.CreateTeamOfExplorers(inputTeamOfExplorers);
        }

        public async Task<bool> DeleteTeamOfExplorers(string toSearch, List<Guid> guids)
        {
            var teamsToDelete = await _repository.GetAllTeamsOfExplorers(
                new TeamOfExplorersFilter {ToSearch = toSearch, Ids = guids, PerfectMatch = true});

            if (!teamsToDelete.Any())
                throw new CrewApiException
                {
                    Message = "TeamOfExplorers is not in the repository.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };
            var result = true;
            foreach (var team in teamsToDelete)
            {
                var sameTeamIdExplorers = await _explorerRepository.GetAllExplorers(
                    new ExplorerFilter {TeamId = team.Id});
                if(sameTeamIdExplorers != 0)
                    throw new CrewApiException
                    {
                        Message = $"Explorers still contain the team id {team.Id}. Delete them before removing the explorers team.",
                        Severity = ExceptionSeverity.Error,
                        Type = ExceptionType.ServiceException
                    };
                result = result && await _repository.DeleteTeamOfExplorers(team.Id);
            }
            
            return result;
        }

        public async Task<bool> UpdateTeamOfExplorers(TeamOfExplorers inputTeamOfExplorers)
        {
            _teamOfExplorersValidator.Validate(inputTeamOfExplorers);
            
            var sameIdTeamOfExplorers = await _repository.GetAllTeamsOfExplorers(
                new TeamOfExplorersFilter {Ids = new List<Guid>{inputTeamOfExplorers.Id}});

            if (!sameIdTeamOfExplorers.Any())
                throw new CrewApiException
                {
                    Message = "Team of explorers to update has not been found.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };
            if (inputTeamOfExplorers.Name != sameIdTeamOfExplorers.First().Name)
            {
                var sameNameTeamOfExplorers = await _repository.GetAllTeamsOfExplorers(
                    new TeamOfExplorersFilter {ToSearch = inputTeamOfExplorers.Name, PerfectMatch = true});
                if(sameNameTeamOfExplorers.Any())
                    throw new CrewApiException
                    {
                        Message = $"Cannot update team of explorers name {inputTeamOfExplorers.Name} to one that already exists.",
                        Severity = ExceptionSeverity.Error,
                        Type = ExceptionType.ServiceException
                    };
            }

            if (inputTeamOfExplorers.ShuttleId != sameIdTeamOfExplorers.First().ShuttleId)
            {
                var shuttlesWithId = await _shuttleRepository.GetAllShuttles(new ShuttleFilter
                    {Ids = new List<Guid> {inputTeamOfExplorers.ShuttleId}});
                if(!shuttlesWithId.Any())
                    throw new CrewApiException
                    {
                        Message = $"TeamOfExplorers's Shuttle id {inputTeamOfExplorers.ShuttleId} does not correspond to any shuttle !",
                        Severity = ExceptionSeverity.Error,
                        Type = ExceptionType.ServiceException
                    };
                
                var sameShuttleIdTeams = await _repository.GetAllTeamsOfExplorers(
                    new TeamOfExplorersFilter {ShuttleGuid = inputTeamOfExplorers.ShuttleId});
                if(sameShuttleIdTeams.Any())
                    throw new CrewApiException
                    {
                        Message = $"TeamOfExplorers's Shuttle id {inputTeamOfExplorers.ShuttleId} has already been assigned to another team !",
                        Severity = ExceptionSeverity.Error,
                        Type = ExceptionType.ServiceException
                    };
            }
            
            var team = sameIdTeamOfExplorers.First();
            team.UpdateByReflection(inputTeamOfExplorers);
            return await _repository.UpdateTeamOfExplorers(team);
        }
    }
}