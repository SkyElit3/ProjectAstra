using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectAstra.Web.CrewApi.Core.Filters;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Interfaces
{
    public interface IRobotRepository
    {
        public Task<List<Robot>> GetAllRobots(RobotFilter filter, int pagination = 50, int skip = 0);

        public Task<bool> CreateRobot(Robot inputRobot);

        public Task<bool> DeleteRobot(Guid id);

        public Task<bool> UpdateRobot(Robot inputRobot);
    }
}