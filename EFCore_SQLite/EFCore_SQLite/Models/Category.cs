using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFCore_SQLite.Models
{
    public class Category : BindableBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        //[Column(TypeName = "varchar(200)")]
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        //public string Name { get; set; }

        //
        public ICollection<Item> Items { get; set; }

        public Category()
        {

        }
        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public Category(string name)
        {
            Name = name;
        }
    }
}
