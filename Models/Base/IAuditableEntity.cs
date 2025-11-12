namespace GuestHouseBookingApplication_Server.Models.Base
{
    public interface IAuditableEntity
    {
        int? CreatedBy { get; set; }
        DateTime? CreatedDate { get; set; }
        int? ModifiedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
        int? DeletedBy { get; set; }
        DateTime? DeletedDate { get; set; }
        string ActiveStatus { get; set; }
    }
}
