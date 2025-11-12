using GuestHouseBookingApplication_Server.Models;

namespace GuestHouseBookingApplication_Server.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<GuestHouse> GuestHouses { get; }
        IRepository<Room> Rooms { get; }
        IRepository<Bed> Beds { get; }
        IRepository<Booking> Bookings { get; }

        Task<int> CommitAsync();
    }
}
