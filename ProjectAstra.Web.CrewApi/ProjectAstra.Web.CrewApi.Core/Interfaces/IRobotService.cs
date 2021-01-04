#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Interfaces
{
    public interface IRobotService
    {
        public Task<List<Robot>> GetAllRobots(string toSearch, List<Guid> guids, int pagination = 50, int skip = 0);

        public Task<bool> CreateRobot(Robot inputRobot);

        public Task<bool> DeleteRobot(string toSearch, List<Guid> guids);

        public Task<bool> UpdateRobot(Robot inputRobot);
    }
}