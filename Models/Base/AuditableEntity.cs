namespace GuestHouseBookingApplication_Server.Models.Base
{
    public abstract class AuditableEntity : IAuditableEntity
    {
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string ActiveStatus { get; set; } = "Active";
    }
}
