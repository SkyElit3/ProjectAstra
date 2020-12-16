using System;

namespace ProjectAstra.Web.CrewApi.Core.Models
{
    public class TeamOfExplorers : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public Guid ShuttleId { get; set; }
        public Shuttle Shuttle { get; set; }
        
    }
}