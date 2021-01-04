using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Filters
{
    public class TeamOfExplorersFilter
    {
        public string ToSearch { get; set; }

        public List<Guid> Ids { get; set; }
        
        public Guid ShuttleGuid { get; set; }

        public bool PerfectMatch { get; set; }


        public IQueryable<TeamOfExplorers> Filter(IQueryable<TeamOfExplorers> teamOfExplorersQuery)
        {
            if (!string.IsNullOrEmpty(ToSearch))
            {
                if (!PerfectMatch)
                    ToSearch = new string("%" + ToSearch + "%");
                teamOfExplorersQuery = teamOfExplorersQuery.Where(t =>
                    EF.Functions.Like(t.Name, ToSearch));
            }
            if (Ids != null && Ids.Count != 0)
                teamOfExplorersQuery = teamOfExplorersQuery.Where(t => Ids.Contains(t.Id));
            if (ShuttleGuid != Guid.Empty)
                teamOfExplorersQuery = teamOfExplorersQuery.Where(t => t.ShuttleId.Equals(ShuttleGuid));
            
            return teamOfExplorersQuery;
        }
    }
}