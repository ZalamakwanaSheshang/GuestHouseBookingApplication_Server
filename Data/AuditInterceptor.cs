using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GuestHouseBookingApplication_Server.Data
{

    public class AuditInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result
        )
        {
            ApplyAuditFields(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        private void ApplyAuditFields(DbContext? context)
        {

            if (context == null) return;

            Console.WriteLine("➡️ [AUDIT] SaveChanges triggered for " + context.GetType().Name);

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value;
            int.TryParse(userIdClaim, out int userId);

            var utcNow = DateTime.UtcNow;

            foreach (var entry in context.ChangeTracker.Entries())
            {
                // Only act on tracked entities with those properties
                if (entry.Entity == null)
                    continue;

                var props = entry.Properties.Select(p => p.Metadata.Name).ToList();

                bool hasCreatedBy = props.Contains("CreatedBy");
                bool hasCreatedDate = props.Contains("CreatedDate");
                bool hasModifiedBy = props.Contains("ModifiedBy");
                bool hasModificationDate = props.Contains("ModificationDate");

                if (entry.State == EntityState.Added)
                {
                    if (hasCreatedBy)
                        entry.Property("CreatedBy").CurrentValue = userId;
                    if (hasCreatedDate)
                        entry.Property("CreatedDate").CurrentValue = utcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    if (hasModifiedBy)
                        entry.Property("ModifiedBy").CurrentValue = userId;
                    if (hasModificationDate)
                        entry.Property("ModificationDate").CurrentValue = utcNow;
                }

                if (entry.State == EntityState.Deleted)
                {
                    if (hasModifiedBy)
                        entry.Property("ModifiedBy").CurrentValue = userId;
                    if (hasModificationDate)
                        entry.Property("ModificationDate").CurrentValue = utcNow;
                }

                Console.WriteLine($"Audit running for user {userId}");
            }
            Console.WriteLine("➡️ [AUDIT] Entries tracked: " + context.ChangeTracker.Entries().Count());
        }
    }
}
