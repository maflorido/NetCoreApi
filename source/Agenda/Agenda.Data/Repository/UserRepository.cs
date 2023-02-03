using Agenda.Domain.Exceptions;
using Agenda.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AgendaContext _context;

        public UserRepository(AgendaContext context) 
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            var task = await Find(id);
            if (task == null) throw new NotfoundException("Task not founded.");
            _context.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<Domain.Entities.User> Find(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<Domain.Entities.User>> List()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task New(Domain.Entities.User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Save(Domain.Entities.User user)
        {
            await _context.SaveChangesAsync();
        }
    }
}
