using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectAstra.Web.CrewApi.Core.Filters;
using ProjectAstra.Web.CrewApi.Core.Interfaces;
using ProjectAstra.Web.CrewApi.Core.Models;
using ProjectAstra.Web.CrewApi.Infrastructure.Data;

namespace ProjectAstra.Web.CrewApi.Infrastructure.Repositories
{
    public class RobotRepository : IRobotRepository
    {
        private readonly DataContext _dataContext;

        public RobotRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Robot>> GetAllRobots(
            RobotFilter filter, int pagination = 50, int skip = 0)
        {
            return await filter.Filter(_dataContext.Robots
                    .AsQueryable())
                .Skip(skip)
                .Take(pagination)
                .ToListAsync();
        }

        public async Task<bool> CreateRobot(Robot inputRobot)
        {
            await _dataContext.Robots.AddAsync(inputRobot);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRobot(Guid id)
        {
            _dataContext.Robots.Remove(
                await _dataContext.Robots.FirstOrDefaultAsync(robot => robot.Id.Equals(id)));
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRobot(Robot inputRobot)
        {
            _dataContext.Robots.Update(inputRobot);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}