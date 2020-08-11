using System;

namespace WhereIsMyOrder.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Company { get; set; }

        public string Title { get; set; }

        public DateTime Arrival { get; set; }

        public string Link { get; set; }

        public string userId { get; set; }
    }
}
