using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAstra.Web.CrewApi.Core.Models
{
    public class Shuttle : IEntity
    {
        public Guid Id { get; set; }
        
        [Column(TypeName = "varchar(256)")] public string Name { get; set; }

        public int MaxCrewCapacity { get; set; }

        public TeamOfExplorers TeamOfExplorers { get; set; }
    }
}