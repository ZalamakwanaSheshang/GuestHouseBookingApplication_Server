using GuestHouseBookingApplication_Server.Data;
using GuestHouseBookingApplication_Server.Models;

namespace GuestHouseBookingApplication_Server.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _context;

        // User Table
        private IRepository<User>? _users;

        // Guest House Table
        private IRepository<GuestHouse>? _guestHouses;
        public IRepository<GuestHouse> GuestHouses => _guestHouses ??= new GenericRepository<GuestHouse>(_context);


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<User> Users => _users ??= new GenericRepository<User>(_context);

        public async Task<int> CommitAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
