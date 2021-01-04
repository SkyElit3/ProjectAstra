using System;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectAstra.Web.PlanetApi.Core.Enums;

namespace ProjectAstra.Web.PlanetApi.Core.Models
{
    public class Planet : IEntity
    {
        public Guid Id { get; set; }

        [Column(TypeName = "varchar(256)")] public string Name { get; set; }

        public int OrbitalSpeed { get; set; }
        
        [Column(TypeName = "varchar(256)")] public string Image { get; set; }
        
        [Column(TypeName = "varchar(256)")] public string Description { get; set; }
        
        public PlanetStatus Status { get; set; }
        
        public int NumberOfRobots { get; set; }
        
        public Guid SolarSystemId { get; set; }
        
        public SolarSystem SolarSystem { get; set; }
        
    }
}