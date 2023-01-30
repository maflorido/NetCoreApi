using Agenda.Domain.Exceptions;
using Agenda.Domain.Repository;
using Agenda.Domain.Requests;

namespace Agenda.Domain.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<Entities.Task>> Get();
        Task New(PostTaskRequest request);
        Task Save(PutTaskRequest request);
        Task Delete(int id);
    }

    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository) 
        {
            _repository = repository;
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<IEnumerable<Entities.Task>> Get()
        {
            return await _repository.List();
        }

        public async Task New(PostTaskRequest request)
        {
            var task = new Entities.Task(request.User, request.Date, request.Subject, request.Description);
            await _repository.New(task);
        }

        public async Task Save(PutTaskRequest request)
        {
            var task = await _repository.Find(request.Id);
            if (task == null) throw new NotfoundException("Task record was not found.");

            task.Update(request.User, request.Date, request.Subject, request.Description);

            await _repository.Save(task);
        }
    }
}
