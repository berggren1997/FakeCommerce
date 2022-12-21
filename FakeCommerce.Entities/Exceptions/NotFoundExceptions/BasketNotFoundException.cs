namespace FakeCommerce.Entities.Exceptions.NotFoundExceptions
{
    public class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException() : base("No basket found.")
        { }
    }
}
