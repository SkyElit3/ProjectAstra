using System;

namespace ProjectAstra.Web.CrewApi.Core.Models
{
    public class Shuttle : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MaxCrewCapacity { get; set; }
    }
    
}