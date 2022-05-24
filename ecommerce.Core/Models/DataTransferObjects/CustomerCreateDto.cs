namespace ecommerce.Core.Models.DataTransferObjects
{
    public class CustomerCreateDto
    {
        public string FirstName       { get; set; }
        public string LastName        { get; set; }
        public string Username        { get; set; }
        public string ShippingAddress { get; set; }
    }
}