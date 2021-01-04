using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Filters
{
    public class ShuttleFilter
    {
        public string ToSearch { get; set; }

        public List<Guid> Ids { get; set; }

        public bool PerfectMatch { get; set; }


        public IQueryable<Shuttle> Filter(IQueryable<Shuttle> shuttleQuery)
        {
            if (!string.IsNullOrEmpty(ToSearch))
            {
                if (!PerfectMatch)
                    ToSearch = new string("%" + ToSearch + "%");
                shuttleQuery = shuttleQuery.Where(shuttle =>
                    EF.Functions.Like(shuttle.Name, ToSearch));
            }

            if (Ids != null && Ids.Count != 0)
                shuttleQuery = shuttleQuery.Where(shuttle => Ids.Contains(shuttle.Id));
            return shuttleQuery;
        }
    }
}