using Agenda.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agenda.Domain.Entities
{
    [Table("Task")]
    public class Task
    {
        [Key]
        public int Id { get; private set; }
        public virtual User User { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Subject { get; private set; }
        public string Description { get; private set; }        

        //required for Entity Framework
        private Task() { }

        public Task(User user, DateTime startDate, DateTime endDate, string subject, string description)
        {
            Validate(startDate, endDate, subject);

            User = user;
            StartDate = startDate;
            EndDate = endDate;
            Subject = subject;
            Description = description;
        }

        public void Update(DateTime startDate, DateTime endDate, string subject, string description)
        {
            ValidateUpdate(startDate, endDate, subject);

            StartDate = startDate;
            EndDate = endDate;
            Subject = subject;
            Description = description;
        }

        private void Validate(DateTime startDate, DateTime endDate, string subject)
        {
            if (startDate == DateTime.MinValue) { throw new Exceptions.ValidationException("Start Date is required."); }
            if (endDate == DateTime.MinValue) { throw new Exceptions.ValidationException("End Date is required."); }
            if (subject == null) { throw new Exceptions.ValidationException("Subject is required."); }
        }

        private void ValidateUpdate(DateTime startDate, DateTime endDate, string subject)
        {
            if (startDate == DateTime.MinValue) { throw new Exceptions.ValidationException("Start Date is required."); }
            if (endDate == DateTime.MinValue) { throw new Exceptions.ValidationException("End Date is required."); }
            if (subject == null) { throw new Exceptions.ValidationException("Subject is required."); }
        }

    }
}
