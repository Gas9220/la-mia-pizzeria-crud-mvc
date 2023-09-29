namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }

        public Pizza(int id, string name, string photoUrl, string description, float price) { 
            Id = id;
            Name = name;
            PhotoUrl = photoUrl;
            Description = description;
            Price = price;
        }
    }
}
