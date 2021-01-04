using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Filters
{
    public class HumanCaptainFilter
    {
        public string ToSearch { get; set; }

        public List<Guid> Ids { get; set; }

        public bool PerfectMatch { get; set; }
        
        public Guid TeamId { get; set; }
        
        public IQueryable<HumanCaptain> Filter(IQueryable<HumanCaptain> humanCaptainQuery)
        {
            if (!string.IsNullOrEmpty(ToSearch))
            {
                if (!PerfectMatch)
                    ToSearch = new string("%" + ToSearch + "%");
                humanCaptainQuery = humanCaptainQuery.Where(humanCaptain =>
                    EF.Functions.Like(humanCaptain.Name, ToSearch));
            }

            if (Ids != null && Ids.Count != 0)
                humanCaptainQuery = humanCaptainQuery.Where(humanCaptain => Ids.Contains(humanCaptain.Id ) || Ids.Contains(humanCaptain.TeamOfExplorersId));
            
            if (TeamId != Guid.Empty)
                humanCaptainQuery = humanCaptainQuery.Where(t => t.TeamOfExplorersId.Equals(TeamId));
            
            return humanCaptainQuery;
        }
    }
}