using System;
using ecommerce.Core.Models;

namespace ecommerce.Core.Factories
{
    public class ItemFactory
    {
        private static readonly Random _random = new Random();
        
        public static Item Create(ItemType type, long ownerId)
        {
            var item = new Item();
            item.Id = GioId.New();
            item.Type = type;
            item.Rarity = _random.NextDouble();
            item.OwnerId = ownerId;
            return item;
        }
    }
}