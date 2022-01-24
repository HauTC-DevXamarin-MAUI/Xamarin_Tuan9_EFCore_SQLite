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
        [Required, MaxLength(20)]
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        // Khóa ngoại
        public int IdCategory { get; set; }
        //Khai báo khóa ngoại
        public virtual Category Category { get; set; }

        public Item()
        {

        }
        public Item(int id, string name, string image, string desc, int idCate)
        {
            Id = id;
            Name = name;
            Image = image;
            Description = desc;
            IdCategory = idCate;
        }
        public Item(string name, string image, string desc, int idCate)
        {
            Name = name;
            Image = image;
            Description = desc;
            IdCategory = idCate;
        }
    }
}
