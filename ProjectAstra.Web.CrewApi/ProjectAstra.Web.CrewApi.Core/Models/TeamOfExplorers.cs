using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAstra.Web.CrewApi.Core.Models
{
    public class TeamOfExplorers : IEntity
    {
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(100)")] public string Name { get; set; }

        [Column(TypeName = "varchar(100)")] public string Status { get; set; }

        public Guid ShuttleId { get; set; }

        public Shuttle Shuttle { get; set; }

        public List<Explorer> Explorers { get; set; }
    }
}