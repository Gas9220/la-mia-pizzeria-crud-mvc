using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_crud_mvc.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column("photo_url")]
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }

        public Pizza() { }

        public Pizza(int id, string name, string photoUrl, string description, float price) { 
            Id = id;
            Name = name;
            PhotoUrl = photoUrl;
            Description = description;
            Price = price;
        }
    }
}
