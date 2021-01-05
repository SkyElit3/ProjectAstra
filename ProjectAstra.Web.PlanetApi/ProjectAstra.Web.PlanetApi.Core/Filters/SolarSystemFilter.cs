using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectAstra.Web.PlanetApi.Core.Models;

namespace ProjectAstra.Web.PlanetApi.Core.Filters
{
    public class SolarSystemFilter
    {
        public string ToSearch { get; set; }

        public List<Guid> Ids { get; set; }

        public bool PerfectMatch { get; set; }


        public IQueryable<SolarSystem> Filter(IQueryable<SolarSystem> solarSystemQuery)
        {
            if (!string.IsNullOrEmpty(ToSearch))
            {
                if (!PerfectMatch)
                    ToSearch = new string("%" + ToSearch + "%");
                solarSystemQuery = solarSystemQuery.Where(system =>
                    EF.Functions.Like(system.Name, ToSearch));
            }

            if (Ids != null && Ids.Count != 0)
                solarSystemQuery = solarSystemQuery.Where(system => Ids.Contains(system.Id));
            return solarSystemQuery;
        }
    }
}