namespace FakeCommerce.Entities.Exceptions.NotFoundExceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException() : base("No products found")
        { }

        public ProductNotFoundException(int id) : base($"Product with id {id} was not found")
        { }
    }
}
