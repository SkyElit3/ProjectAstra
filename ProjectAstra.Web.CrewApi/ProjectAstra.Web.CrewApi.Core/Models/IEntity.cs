using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAstra.Web.CrewApi.Core.Models
{
    public interface IEntity
    {
        public Guid Id { get; set; }

        [Column(TypeName = "varchar(256)")] public string Name { get; set; }
    }
}