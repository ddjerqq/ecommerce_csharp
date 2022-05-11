namespace ecommerce.Core.Models
{
    public class ItemCreateDto
    {
        public ItemType Type { get; set; }
        public long OwnerId { get; set; }
    }
}