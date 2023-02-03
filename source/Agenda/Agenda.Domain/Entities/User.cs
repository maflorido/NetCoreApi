using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agenda.Domain.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public ICollection<Task> Tasks { get; private set; }        

        private User() { }

        public User(int id, string name, string login, string password)
        {
            Id = id;
            Name = name;
            Login = login;
            Password = password;
        }

        public void Update(string name, string login, string password)
        {
            Name = name;
            Login = login;
            Password = password;
        }

        public void ScheduleEvent(DateTime start, DateTime end, string subject, string description)
        {
            ValidateTaskTime(start, end);

            Tasks.Add(new Task(Id, start, end, subject, description));
        }

        public void ValidateTaskTime(DateTime start, DateTime end)
        {
            if (Tasks.Any(x => x.StartDate >= start || x.EndDate <= start || x.StartDate >= end || x.EndDate <= end))
                throw new Exceptions.ValidationException("There is an event in that time period.");
        }
    }
}
