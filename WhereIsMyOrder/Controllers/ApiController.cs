using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhereIsMyOrder.Models;

namespace WhereIsMyOrder.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        public static List<Order> orders = new List<Order>();

        IOrderRepository OrderRepository { get; set; }

        public ApiController(IOrderRepository orderRepository)
        {
            OrderRepository = orderRepository;
        }

        [HttpGet]
        [Route("api/GetOrdersForUser/{userId}")]
        public async Task<ActionResult<Order[]>> GetOrdersForUser(string userId)
        {
            var results = OrderRepository.GetAll().Where(x => x.userId == userId).ToArray();
            return results;
        }

        [HttpPost]
        [Route("api/CreateOrderForUser/{userId}")]
        public async Task<Order> CreateOrderForUser(string userId, [FromBody] JsonElement body)
        {
            string json = JsonSerializer.Serialize(body);
            var root = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(json);
            var order = root.newOrder;
            Order newOrder = new Order
            {
                Company = order.company,
                Title = order.title,
                Link = order.link,
                Arrival = order.arrival,
                userId = userId
            };
            OrderRepository.Insert(newOrder);
            OrderRepository.SaveChanges();
            return newOrder;
        }

        [HttpDelete] 
        [Route("api/DeleteOrder/{orderId}")]
        public async Task<ActionResult> DeleteOrder(int orderId)
        {
            var order = OrderRepository.GetAll().Where(x => x.Id == orderId).FirstOrDefault();
            OrderRepository.Delete(order);
            OrderRepository.SaveChanges();
            return new OkResult();
        }
    }

    public class NewOrder
    {
        public int id { get; set; }
        public string company { get; set; }
        public string title { get; set; }
        public DateTime arrival { get; set; }
        public string link { get; set; }
    }

    public class Root
    {
        public NewOrder newOrder { get; set; }
    }


}
