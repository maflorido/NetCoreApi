using Agenda.Domain.Entities;
using Agenda.Domain.Exceptions;
using Agenda.Domain.Repository;
using Agenda.Domain.Requests;

namespace Agenda.Domain.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> Get();
        System.Threading.Tasks.Task New(PostUserRequest request);
        System.Threading.Tasks.Task Save(PutUserRequest request);
        System.Threading.Tasks.Task Delete(int id);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(ITaskRepository taskRepository, IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            await _userRepository.Delete(id);
        }

        public async Task<IEnumerable<Entities.User>> Get()
        {
            return await _userRepository.List();
        }

        public async System.Threading.Tasks.Task New(PostUserRequest request)
        {
            var user = new User(request.Name, request.Login, request.Password);
            await _userRepository.New(user);
        }

        public async System.Threading.Tasks.Task Save(PutUserRequest request)
        {
            var user = await _userRepository.Find(request.Id);
            if (user == null) throw new NotfoundException("User was not found.");
            user.Update(request.Name, request.Login, request.Password);
            
            await _userRepository.Save(user);
        }
    }
}
