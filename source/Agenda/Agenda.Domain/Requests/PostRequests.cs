namespace Agenda.Domain.Requests
{
    public class PostTaskRequest
    {
        public int User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Subject { get;set; }
        public string Description { get; set; }
    }

    public class PutTaskRequest
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }

    public class PostUserRequest
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class PutUserRequest : PostUserRequest
    {
        public int Id { get; set; }
    }
}
