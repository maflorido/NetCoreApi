namespace Agenda.Domain.Exceptions
{
    public class NotfoundException : Exception
    {
        public NotfoundException() { }

        public NotfoundException(string message) : base(message) { }

        public NotfoundException(string message, Exception inner) : base(message, inner) { }
    }

    public class ValidationException : Exception
    {
        public ValidationException() { }

        public ValidationException(string message) : base(message) { }

        public ValidationException(string message, Exception inner) : base(message, inner) { }
    }
}
