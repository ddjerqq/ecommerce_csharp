using System;
 using System.ComponentModel.DataAnnotations.Schema;
 
 namespace ecommerce.Core.Models
 {
     public class Item
     {
         public long Id       { get; set; }
         public ItemType Type { get; set; }
         public double Rarity { get; set; }
         public long OwnerId  { get; set; }
         
         public uint Price { get => (uint) Math.Round(1 / Rarity);  }
         public DateTime CreatedAt { get => GioId.GetCreationDate(Id); }
     }
 }