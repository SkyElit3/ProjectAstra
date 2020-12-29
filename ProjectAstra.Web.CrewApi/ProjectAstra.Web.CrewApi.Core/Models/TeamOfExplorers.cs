using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectAstra.Web.CrewApi.Core.Enums;

namespace ProjectAstra.Web.CrewApi.Core.Models
{
    public class TeamOfExplorers : IEntity
    {
        public Guid Id { get; set; }
        
        [Column(TypeName = "varchar(256)")] public string Name { get; set; }

        public StatusType Status { get; set; }

        public Guid ShuttleId { get; set; }

        public Shuttle Shuttle { get; set; }

        public List<Explorer> Explorers { get; set; }
    }
}