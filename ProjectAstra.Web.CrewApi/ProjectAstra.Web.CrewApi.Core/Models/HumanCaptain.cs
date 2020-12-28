using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAstra.Web.CrewApi.Core.Models
{
    public class HumanCaptain : Explorer
    {
        [Column(TypeName = "varchar(256)")] public string Grade { get; set; }

        [Column(TypeName = "varchar(256)")] public string Password { get; set; }
    }
}