using System;

namespace ProjectAstra.Web.CrewApi.Core.Models
{
    public abstract class Explorer : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public Guid TeamOfExplorersId { get; set; }
        public TeamOfExplorers TeamOfExplorers { get; set; }
    }
}