using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Filters
{
    public class RobotFilter
    {
        public string ToSearch { get; set; }

        public List<Guid> Ids { get; set; }

        public bool PerfectMatch { get; set; }
        
        public Guid TeamId { get; set; }
        
        public IQueryable<Robot> Filter(IQueryable<Robot> robotQuery)
        {
            if (!string.IsNullOrEmpty(ToSearch))
            {
                if (!PerfectMatch)
                    ToSearch = new string("%" + ToSearch + "%");
                robotQuery = robotQuery.Where(robot =>
                    EF.Functions.Like(robot.Name, ToSearch));
            }

            if (Ids != null && Ids.Count != 0)
                robotQuery = robotQuery.Where(robot => Ids.Contains(robot.Id));
            
            if (TeamId != Guid.Empty)
                robotQuery = robotQuery.Where(t => t.TeamOfExplorersId.Equals(TeamId));
            
            return robotQuery;
        }
    }
}