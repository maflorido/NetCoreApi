using Agenda.Domain.Exceptions;
using Agenda.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Data.Repository
{
    public class TaskRepository: ITaskRepository
    {
        private readonly AgendaContext _context;

        public TaskRepository(AgendaContext context) 
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

        public async Task<Domain.Entities.Task> Find(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<IEnumerable<Domain.Entities.Task>> List()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task New(Domain.Entities.Task task)
        {
            await _context.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task Save(Domain.Entities.Task task)
        {
            await _context.SaveChangesAsync();
        }
    }
}
