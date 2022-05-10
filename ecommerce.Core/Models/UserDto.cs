using System.Collections.Generic;

namespace ecommerce.Core.Models
{
    public class UserDto
    {
        // TODO ar arsebul datas saidan vqmnit?
        // tu id arvicit mashin rogor vigebt ra items vgulisxmobt???????
        public string Username { get; set; }
        public uint Experience { get; set; }
        public uint Wallet { get; set; }
        public uint Bank { get; set; }
        public IEnumerable<ItemDto> Items { get; set; }
    }
}