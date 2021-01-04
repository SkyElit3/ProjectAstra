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
    public class RobotService : IRobotService
    {
        private readonly IRobotRepository _repository;
        private readonly ITeamOfExplorersRepository _teamOfExplorersRepository;
        private readonly IExplorerRepository _explorerRepository;
        private readonly IRobotValidator _robotValidator;
        private readonly IShuttleRepository _shuttleRepository;

        public RobotService(IRobotRepository repository, IRobotValidator robotValidator,
            ITeamOfExplorersRepository teamOfExplorersRepository, IShuttleRepository shuttleRepository, IExplorerRepository explorerRepository)
        {
            _repository = repository;
            _robotValidator = robotValidator;
            _teamOfExplorersRepository = teamOfExplorersRepository;
            _shuttleRepository = shuttleRepository;
            _explorerRepository = explorerRepository;
        }

        public async Task<List<Robot>> GetAllRobots(string toSearch, List<Guid> guids,
            int pagination = 50, int skip = 0)
        {
            return await _repository.GetAllRobots(
                new RobotFilter {ToSearch = toSearch, Ids = guids}, pagination, skip);
        }

        public async Task<bool> CreateRobot(Robot inputRobot)
        {
            _robotValidator.Validate(inputRobot);
            await ValidateRobotCreation(inputRobot);
            return await _repository.CreateRobot(inputRobot);
        }

        public async Task<bool> DeleteRobot(string toSearch, List<Guid> guids)
        {
            var robotsToDelete = await _repository.GetAllRobots(
                new RobotFilter {ToSearch = toSearch, Ids = guids, PerfectMatch = true});

            if (!robotsToDelete.Any())
                throw new CrewApiException
                {
                    ExceptionMessage = "Robot is not in the repository.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            var result = true;
            foreach (var robot in robotsToDelete)
                result = result && await _repository.DeleteRobot(robot.Id);
            return result;
        }

        public async Task<bool> UpdateRobot(Robot inputRobot)
        {
            _robotValidator.Validate(inputRobot);

            var sameIdRobots = await _repository.GetAllRobots(
                new RobotFilter {Ids = new List<Guid> {inputRobot.Id}});

            if (!sameIdRobots.Any())
                throw new CrewApiException
                {
                    ExceptionMessage = $"Cannot update non-existant robot {inputRobot.Id}.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            if (inputRobot.Name != sameIdRobots.First().Name)
            {
                var sameNameRobots = await _repository.GetAllRobots(
                    new RobotFilter {ToSearch = inputRobot.Name, PerfectMatch = true});

                if (sameNameRobots.Any(h => h.Id != inputRobot.Id))
                    throw new CrewApiException
                    {
                        ExceptionMessage = $"Cannot update Robot name {inputRobot.Name} to one that already exists.",
                        Severity = ExceptionSeverity.Error,
                        Type = ExceptionType.ServiceException
                    };
            }

            if (inputRobot.TeamOfExplorersId != sameIdRobots.First().TeamOfExplorersId)
            {
                var sameIdTeamOfExplorers = await _teamOfExplorersRepository.GetAllTeamsOfExplorers(
                    new TeamOfExplorersFilter {Ids = new List<Guid> {inputRobot.TeamOfExplorersId}});
                if(!sameIdTeamOfExplorers.Any())
                    throw new CrewApiException
                    {
                        ExceptionMessage = $"Team with id {inputRobot.TeamOfExplorersId} does not exist.",
                        Severity = ExceptionSeverity.Error,
                        Type = ExceptionType.ServiceException
                    };
            }

            var robot = sameIdRobots.First();
            robot.UpdateByReflection(inputRobot);
            return await _repository.UpdateRobot(robot);
        }

        private async Task ValidateRobotCreation(Robot inputRobot)
        {
            var sameIdRobots = await _repository.GetAllRobots(
                new RobotFilter {Ids = new List<Guid>() {inputRobot.Id}});

            if (sameIdRobots.Any())
                throw new CrewApiException
                {
                    ExceptionMessage = $"Robot with same id {inputRobot.Id} already exists in the repository !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            var sameIdTeamsOfExplorers = await _teamOfExplorersRepository.GetAllTeamsOfExplorers(
                new TeamOfExplorersFilter {Ids = new List<Guid> {inputRobot.TeamOfExplorersId}});

            if (sameIdTeamsOfExplorers.Count != 1)
                throw new CrewApiException
                {
                    ExceptionMessage = $"Robot's TeamOfExplorers id {inputRobot.TeamOfExplorersId} does not correspond to any team !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };

            var sameTeamIdExplorers = await _explorerRepository.GetAllExplorers(
                new ExplorerFilter {TeamId = inputRobot.TeamOfExplorersId});

            var sameTeamIdShuttles = await _shuttleRepository.GetAllShuttles(
                new ShuttleFilter {Ids = new List<Guid> {sameIdTeamsOfExplorers.First().ShuttleId}});
            
            if(sameTeamIdShuttles.Any())
                if(sameTeamIdExplorers > sameTeamIdShuttles.First().MaxCrewCapacity)
                    throw new CrewApiException
                    {
                        ExceptionMessage = $"Adding Robot with id {inputRobot.TeamOfExplorersId} exceeds shuttle's {sameTeamIdShuttles.First().Id} max crew capacity !",
                        Severity = ExceptionSeverity.Error,
                        Type = ExceptionType.ServiceException
                    };

            var nameAlikeRobots = await _repository.GetAllRobots(
                new RobotFilter {ToSearch = inputRobot.Name, PerfectMatch = true});

            if (nameAlikeRobots.Any())
                throw new CrewApiException
                {
                    ExceptionMessage = $"Robot name {inputRobot.Name} is not unique !",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ServiceException
                };
        }
    }
}