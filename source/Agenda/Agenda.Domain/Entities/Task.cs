using Agenda.Domain.Exceptions;

namespace Agenda.Domain.Entities
{
    [Table("Task")]
    public class Task
    {
        [Key]
        public int Id { get; private set; }
        public string User { get; private set; }
        public DateTime Date { get; private set; }
        public string Subject { get; private set; }
        public string Description { get; private set; }

        //required for Entity Framework
        private Task() { }

        public Task(string user, DateTime date, string subject, string description)
        {
            Validate(user, date, subject);

            User = user;
            Date = date;
            Subject = subject;
            Description = description;
        }

        public void Update(string user, DateTime date, string subject, string description)
        {
            Validate(user, date, subject);

            User = user;
            Date = date;
            Subject = subject;
            Description = description;
        }

        private void Validate(string user, DateTime date, string subject)
        {
            if (string.IsNullOrEmpty(user)) { throw new ValidationException("User is required."); }
            if (date == DateTime.MinValue) { throw new ValidationException("Date is required."); }
            if (subject == null) { throw new ValidationException("Subject is required."); }
        }

        

    }
}
