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
    public class HumanCaptainService : IHumanCaptainService
    {
        private readonly IHumanCaptainRepository _repository;
        private readonly ITeamOfExplorersRepository _teamOfExplorersRepository;
        private readonly IExplorerRepository _explorerRepository;
        private readonly IHumanCaptainValidator _humanCaptainValidator;
        private readonly IShuttleRepository _shuttleRepository;

        public HumanCaptainService(IHumanCaptainRepository repository, IHumanCaptainValidator humanCaptainValidator,
            ITeamOfExplorersRepository teamOfExplorersRepository, IShuttleRepository shuttleRepository, IExplorerRepository explorerRepository)
        {
            _repository = repository;
            _humanCaptainValidator = humanCaptainValidator;
            _teamOfExplorersRepository = teamOfExplorersRepository;
            _shuttleRepository = shuttleRepository;
            _explorerRepository = explorerRepository;
        }

        public async Task<List<HumanCaptain>> GetAllHumanCaptains(string toSearch, List<Guid> guids,
            int pagination = 50, int skip = 0)
        {
            return await _repository.GetAllHumanCaptains(
                new HumanCaptainFilter {ToSearch = toSearch, Ids = guids}, pagination, skip);
        }

        public async Task<bool> CreateHumanCaptain(HumanCaptain inputHumanCaptain)
        {
            _humanCaptainValidator.Validate(inputHumanCaptain);
            await ValidateHumanCaptainCreation(inputHumanCaptain);
            return await _repository.CreateHumanCaptain(inputHumanCaptain);
        }

        public async Task<bool> DeleteHumanCaptain(string toSearch, List<Guid> guids)
        {
            var humanCaptainsToDelete = await _repository.GetAllHumanCaptains(
                new HumanCaptainFilter {ToSearch = toSearch, Ids = guids, PerfectMatch = true});

            if (!humanCaptainsToDelete.Any())
                throw new CrewApiException
                {
                    ExceptionMessage = "HumanCaptain is not in the repository.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            var result = true;
            foreach (var humanCaptain in humanCaptainsToDelete)
                result = result && await _repository.DeleteHumanCaptain(humanCaptain.Id);
            return result;
        }

        public async Task<bool> UpdateHumanCaptain(HumanCaptain inputHumanCaptain)
        {
            _humanCaptainValidator.Validate(inputHumanCaptain);

            var sameIdHumanCaptains = await _repository.GetAllHumanCaptains(
                new HumanCaptainFilter {Ids = new List<Guid> {inputHumanCaptain.Id}});

            if (!sameIdHumanCaptains.Any())
                throw new CrewApiException
                {
                    ExceptionMessage = $"Cannot update non-existant humanCaptain {inputHumanCaptain.Id}.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            if (inputHumanCaptain.Name != sameIdHumanCaptains.First().Name)
            {
                var sameNameHumanCaptains = await _repository.GetAllHumanCaptains(
                    new HumanCaptainFilter {ToSearch = inputHumanCaptain.Name, PerfectMatch = true});

                if (sameNameHumanCaptains.Any(h => h.Id != inputHumanCaptain.Id))
                    throw new CrewApiException
                    {
                        ExceptionMessage = $"Cannot update Human Captain name {inputHumanCaptain.Name} to one that already exists.",
                        Severity = ExceptionSeverity.Error,
                        Type = ExceptionType.ServiceException
                    };
            }

            if (inputHumanCaptain.TeamOfExplorersId != sameIdHumanCaptains.First().TeamOfExplorersId)
            {
                var sameIdTeamOfExplorers = await _teamOfExplorersRepository.GetAllTeamsOfExplorers(
                    new TeamOfExplorersFilter {Ids = new List<Guid> {inputHumanCaptain.TeamOfExplorersId}});
                if(!sameIdTeamOfExplorers.Any())
                    throw new CrewApiException
                    {
                        ExceptionMessage = $"Team with id {inputHumanCaptain.TeamOfExplorersId} does not exist.",
                        Severity = ExceptionSeverity.Error,
                        Type = ExceptionType.ServiceException
                    };
                var sameTeamIdHumanCaptains = await _repository.GetAllHumanCaptains(
                    new HumanCaptainFilter {TeamId = inputHumanCaptain.TeamOfExplorersId});
                if(sameTeamIdHumanCaptains.Any())
                    throw new CrewApiException
                    {
                        ExceptionMessage = $"Team with id {inputHumanCaptain.TeamOfExplorersId} has another captain.",
                        Severity = ExceptionSeverity.Error,
                        Type = ExceptionType.ServiceException
                    };
            }

            var humanCaptain = sameIdHumanCaptains.First();
            humanCaptain.UpdateByReflection(inputHumanCaptain);
            return await _repository.UpdateHumanCaptain(humanCaptain);
        }

        private async Task ValidateHumanCaptainCreation(HumanCaptain inputHumanCaptain)
        {
            var sameIdHumanCaptains = await _repository.GetAllHumanCaptains(
                new HumanCaptainFilter {Ids = new List<Guid>() {inputHumanCaptain.Id}});

            if (sameIdHumanCaptains.Any())
                throw new CrewApiException
                {
                    ExceptionMessage = $"HumanCaptain with same id {inputHumanCaptain.Id} already exists in the repository !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            var sameIdTeamsOfExplorers = await _teamOfExplorersRepository.GetAllTeamsOfExplorers(
                new TeamOfExplorersFilter {Ids = new List<Guid> {inputHumanCaptain.TeamOfExplorersId}});

            if (sameIdTeamsOfExplorers.Count != 1)
                throw new CrewApiException
                {
                    ExceptionMessage = $"Human Captain's TeamOfExplorers id {inputHumanCaptain.TeamOfExplorersId} does not correspond to any team !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            var sameTeamIdExplorers = await _explorerRepository.GetAllExplorers(
                new ExplorerFilter {TeamId = inputHumanCaptain.TeamOfExplorersId});

            var sameTeamIdShuttles = await _shuttleRepository.GetAllShuttles(
                new ShuttleFilter {Ids = new List<Guid> {sameIdTeamsOfExplorers.First().ShuttleId}});
            
            if(sameTeamIdShuttles.Any())
                if(sameTeamIdExplorers > sameTeamIdShuttles.First().MaxCrewCapacity)
                    throw new CrewApiException
                    {
                        ExceptionMessage = $"Adding Human captain with id {inputHumanCaptain.TeamOfExplorersId} exceeds shuttle's max crew capacity !",
                        Severity = ExceptionSeverity.Error,
                        Type = ExceptionType.ServiceException
                    };

            var sameTeamIdHumanCaptains = await _repository.GetAllHumanCaptains(
                new HumanCaptainFilter {Ids = new List<Guid> {inputHumanCaptain.TeamOfExplorersId}});

            if (sameTeamIdHumanCaptains.Any())
                throw new CrewApiException
                {
                    ExceptionMessage =
                        $"TeamOfExplorers with id {inputHumanCaptain.TeamOfExplorersId} can have only one human captain !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            var nameAlikeHumanCaptains = await _repository.GetAllHumanCaptains(
                new HumanCaptainFilter {ToSearch = inputHumanCaptain.Name, PerfectMatch = true});

            if (nameAlikeHumanCaptains.Any())
                throw new CrewApiException
                {
                    ExceptionMessage = $"HumanCaptain name {inputHumanCaptain.Name} is not unique !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };
        }
    }
}