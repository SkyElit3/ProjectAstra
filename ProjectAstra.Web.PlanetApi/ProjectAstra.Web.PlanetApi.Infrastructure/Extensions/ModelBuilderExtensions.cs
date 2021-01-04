using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProjectAstra.Web.PlanetApi.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SetIndexForEntities(this ModelBuilder inputBuilder, string inputProperty)
        {
            inputBuilder.Model.GetEntityTypes().Where(t => t.ClrType != null).ToList().ForEach(entityType =>
                inputBuilder.Entity(entityType.ClrType).HasIndex(inputProperty).IsUnique());
        }
    }
}