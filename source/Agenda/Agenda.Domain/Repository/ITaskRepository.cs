namespace Agenda.Domain.Repository
{
    public interface ITaskRepository
    {
        Task<Entities.Task> Find(int id);
        Task<IEnumerable<Entities.Task>> List();
        Task New(Entities.Task task);
        Task Save(Entities.Task task);
        Task Delete(int id);
    }
}
