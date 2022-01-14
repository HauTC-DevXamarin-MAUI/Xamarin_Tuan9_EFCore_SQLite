using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFCore_SQLite.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public Item()
        {

        }
        public Item(int id, string name, string image, string desc)
        {
            Id = id;
            Name = name;
            Image = image;
            Description = desc;
        }
        public Item(string name, string image, string desc)
        {
            Name = name;
            Image = image;
            Description = desc;
        }
    }
}
