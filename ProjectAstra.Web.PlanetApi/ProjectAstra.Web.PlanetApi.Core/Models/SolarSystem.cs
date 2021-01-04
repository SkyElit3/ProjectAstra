using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAstra.Web.PlanetApi.Core.Models
{
    public class SolarSystem : IEntity
    {
        public Guid Id { get; set; }
        
        [Column(TypeName = "varchar(256)")] public string Name { get; set; }

        public int OrbitalSpeed { get; set; }
        
        public List<Planet> Planets { get; set; }
    }
}