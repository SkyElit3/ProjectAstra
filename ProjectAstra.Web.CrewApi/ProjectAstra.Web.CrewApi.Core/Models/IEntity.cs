using System;

namespace ProjectAstra.Web.CrewApi.Core.Models
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}