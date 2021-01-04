using System;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectAstra.Web.CrewApi.Core.Enums;

namespace ProjectAstra.Web.CrewApi.Core.Models
{
    public abstract class Explorer : IEntity
    {
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(256)")] public string Name { get; set; }
        public Guid TeamOfExplorersId { get; set; }

        public ExplorerType ExplorerType { get; set; }
        public TeamOfExplorers TeamOfExplorers { get; set; }
    }
}