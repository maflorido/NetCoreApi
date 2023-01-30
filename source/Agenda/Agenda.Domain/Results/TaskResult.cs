namespace Agenda.Domain.Results
{
    public class TaskResult
    {
        public int Id { get; private set; }
        public string User { get; private set; }
        public string Date { get; private set; }
        public string Subject { get; private set; }
        public string Description { get; private set; }

        public TaskResult(Entities.Task dbRecord)
        {
            Id = dbRecord.Id;
            User = dbRecord.User;
            Date = dbRecord.Date.ToShortTimeString();
            Subject = dbRecord.Subject;
            Description = dbRecord.Description;
        }

    }
}
