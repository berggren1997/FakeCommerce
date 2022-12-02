namespace FakeCommerce.Entities.Exceptions.BadRequestExceptions
{
    public class LoginBadRequestException : BadRequestException
    {
        public LoginBadRequestException() : base("Bad credentials from client, when logging in")
        { }
    }
}
