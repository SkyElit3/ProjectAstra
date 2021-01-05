using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectAstra.Web.PlanetApi.Core.Models;

namespace ProjectAstra.Web.PlanetApi.Core.Filters
{
    public class PlanetFilter
    {
        public string ToSearch { get; set; }

        public List<Guid> Ids { get; set; }

        public bool PerfectMatch { get; set; }
        
        public Guid SystemId { get; set; }

        public IQueryable<Planet> Filter(IQueryable<Planet> planetQuery)
        {
            if (!string.IsNullOrEmpty(ToSearch))
            {
                if (!PerfectMatch)
                    ToSearch = new string("%" + ToSearch + "%");
                planetQuery = planetQuery.Where(planet =>
                    EF.Functions.Like(planet.Name, ToSearch));
            }

            if (Ids != null && Ids.Count != 0)
                planetQuery = planetQuery.Where(planet => Ids.Contains(planet.Id));
            
            if (SystemId != Guid.Empty)
                planetQuery = planetQuery.Where(t => t.SolarSystemId.Equals(SystemId));
            
            return planetQuery;
        }
    }
}