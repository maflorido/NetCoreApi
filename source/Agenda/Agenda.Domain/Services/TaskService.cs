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
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;

        public TaskService(ITaskRepository taskRepository, IUserRepository userRepository) 
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }

        public async Task Delete(int id)
        {
            await _taskRepository.Delete(id);
        }

        public async Task<IEnumerable<Entities.Task>> Get()
        {
            return await _taskRepository.List();
        }

        public async Task New(PostTaskRequest request)
        {
            var user = await _userRepository.Find(request.User);
            if (user == null) throw new NotfoundException("User was not found.");
            
            user.ScheduleEvent(request.StartDate, request.EndDate, request.Subject, request.Description);
            await _userRepository.Save(user);
        }

        public async Task Save(PutTaskRequest request)
        {
            var task = await _taskRepository.Find(request.Id);
            if (task == null) throw new NotfoundException("Task record was not found.");

            task.User.ValidateTaskTime(request.StartDate, request.EndDate);
            task.Update(request.StartDate, request.EndDate, request.Subject, request.Description);
            
            await _taskRepository.Save(task);
        }
    }
}
