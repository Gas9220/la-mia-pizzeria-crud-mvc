namespace la_mia_pizzeria_static.Models
{
    public static class PizzaStore
    {
        public static List<Pizza> GetAllPizzas()
        {
            List<Pizza> pizzaList = new List<Pizza>() {
             new Pizza(1, "Margherita", "/img/margherita.png", "Pomodoro, mozzarella", 6),
            new Pizza(2, "Marinara", "/img/marinara.png", "Pomodoro, origano, aglio", 5),
            new Pizza(3, "Diavola", "/img/diavola.png", "Pomodoro, mozzarella, salame piccante", 8)
            };

            return pizzaList;
        }
    }
}
