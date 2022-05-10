using System.Collections.Generic;
using ecommerce.Core.Models;


namespace ecommerce.Core.Models
{
    public class User
    {
        public long   Id         { get; set; }
        public string Username   { get; set; }
        public uint   Experience { get; set; }
        public uint   Wallet     { get; set; }
        public uint   Bank       { get; set; }
        public List<Item> Items { get; set; }
    }
}
