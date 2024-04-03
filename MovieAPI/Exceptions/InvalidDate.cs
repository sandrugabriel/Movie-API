namespace MovieAPI.Exceptions
{
    public class InvalidDate: Exception
    {
        public InvalidDate(string? message) : base(message) { }
    }
}
