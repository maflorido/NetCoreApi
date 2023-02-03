namespace Agenda.Domain.Results
{
    public class TaskResult
    {
        public int Id { get; private set; }
        public int User { get; private set; }
        public string StartDate { get; private set; }
        public string EndDate { get; private set; }
        public string Subject { get; private set; }
        public string Description { get; private set; }

        public TaskResult(Entities.Task dbRecord)
        {
            Id = dbRecord.Id;
            User = dbRecord.User;
            StartDate = dbRecord.StartDate.ToShortTimeString();
            EndDate = dbRecord.StartDate.ToShortTimeString();
            Subject = dbRecord.Subject;
            Description = dbRecord.Description;
        }

    }
}
