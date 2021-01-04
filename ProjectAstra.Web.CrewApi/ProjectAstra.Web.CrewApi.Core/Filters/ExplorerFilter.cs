using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Filters
{
    public class ExplorerFilter
    {
        public string ToSearch { get; set; }

        public List<Guid> Ids { get; set; }

        public bool PerfectMatch { get; set; }

        public Guid TeamId { get; set; }

        public IQueryable<Explorer> Filter(IQueryable<Explorer> explorerQuery)
        {
            if (!string.IsNullOrEmpty(ToSearch))
            {
                if (!PerfectMatch)
                    ToSearch = new string("%" + ToSearch + "%");
                explorerQuery = explorerQuery.Where(humanCaptain =>
                    EF.Functions.Like(humanCaptain.Name, ToSearch));
            }

            if (Ids != null && Ids.Count != 0)
                explorerQuery = explorerQuery.Where(humanCaptain =>
                    Ids.Contains(humanCaptain.Id) || Ids.Contains(humanCaptain.TeamOfExplorersId));

            if (TeamId != Guid.Empty)
                explorerQuery = explorerQuery.Where(t => t.TeamOfExplorersId.Equals(TeamId));

            return explorerQuery;
        }
    }
}