namespace FakeCommerce.Entities.Exceptions.NotFoundExceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException() : base("User not found")
        { }
    }
}
