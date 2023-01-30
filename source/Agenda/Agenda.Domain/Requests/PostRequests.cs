namespace Agenda.Domain.Requests
{
    public class PostTaskRequest
    {
        public string User { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get;set; }
        public string Description { get; set; }
    }

    public class PutTaskRequest
    {
        public int Id { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }
}
