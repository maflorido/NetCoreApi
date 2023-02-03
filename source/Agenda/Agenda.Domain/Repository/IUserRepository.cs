namespace Agenda.Domain.Repository
{
    public interface IUserRepository
    {
        Task<Entities.User> Find(int id);
        Task<IEnumerable<Entities.User>> List();
        Task New(Entities.User user);
        Task Save(Entities.User user);
        Task Delete(int id);
    }
}
