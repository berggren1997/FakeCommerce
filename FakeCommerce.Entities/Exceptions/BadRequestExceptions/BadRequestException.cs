namespace FakeCommerce.Entities.Exceptions.BadRequestExceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
