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

        public List<Guid> Guids { get; set; }

        public bool PerfectMatch { get; set; }
        

        public IQueryable<Shuttle> Filter(IQueryable<Shuttle> shuttleQuery)
        {
            if (!string.IsNullOrEmpty(ToSearch))
            {
                if(!PerfectMatch)
                    ToSearch = new string("%" + ToSearch + "%");
                if (Guid.TryParse(ToSearch, out var guid))
                    shuttleQuery = shuttleQuery.Where(shuttle => shuttle.Id.Equals(guid));
                else
                    shuttleQuery = shuttleQuery.Where(shuttle =>
                        EF.Functions.Like(shuttle.Name, ToSearch));
            }

            if (Guids != null && Guids.Count != 0)
                shuttleQuery = shuttleQuery.Where(shuttle => Guids.Contains(shuttle.Id));
            return shuttleQuery;
        }
    }
}