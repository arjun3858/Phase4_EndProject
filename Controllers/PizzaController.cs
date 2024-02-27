using Microsoft.AspNetCore.Mvc;
using Phase4EndProject.Models;

namespace Phase4EndProject.Controllers
{
    public class PizzaController : Controller
    {
      
        static public List<Pizza> pizzalist = new List<Pizza>() {

          new Pizza { PizzaId = 100,Type = "Normal Pizza", Price =70},
          new Pizza { PizzaId = 101,Type = "Chicken Pizza",Price=80},
          new Pizza { PizzaId = 102,Type = "Spice lip Pizza ",Price=230},
          new Pizza { PizzaId = 103,Type = "Mottan Pizaa ",Price=150},
          new Pizza { PizzaId = 104,Type = "Pepporni Pizza",Price=110},
          new Pizza { PizzaId = 105,Type = "Mushroom Pizza",Price=120},
          new Pizza { PizzaId = 106,Type = "Panner Pizza",Price=100}
    };
        static public List<OrderInfo> orderdetails = new List<OrderInfo>();
        public IActionResult Index()
        {
            return View(pizzalist);
        }
        public IActionResult Cart(int id)
        {
            var found = (pizzalist.Find(p => p.PizzaId == id)); // This is line 28

            TempData["id"] = id;

            return View(found);
        }

        [HttpPost]
        public IActionResult Cart(IFormCollection f)
        {
            Random r = new Random();
            int id = Convert.ToInt32(TempData["id"]);
            OrderInfo o = new OrderInfo();
            var found = (pizzalist.Find(p => p.PizzaId == id));
            o.OrderId = r.Next(100, 999);
            o.PizzaId = id;
            o.Price = found.Price;
            o.Type = found.Type;
            o.Quantity = Convert.ToInt32(Request.Form["qty"]);
            o.TotalPrice = o.Price * o.Quantity;

            orderdetails.Add(o);

            return RedirectToAction("Checkout");

        }
        public IActionResult Checkout()
        {
            return View(orderdetails);

        }
    }
}
