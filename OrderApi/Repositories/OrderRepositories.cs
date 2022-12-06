using Newtonsoft.Json;
using OrderApi.Models;

namespace OrderApi.Repositories
{
    public class OrderRepositories
    {
        private const string DATA_PATH = @"Data\orders.json";

        public Order Add(Order order)
        {
            var jsonString = File.ReadAllText(DATA_PATH);
            var orders = JsonConvert.DeserializeObject<IList<Order>>(jsonString);
            orders ??= new List<Order>();

            order.OrderId = GetNewId();
            order.DateTime = DateTime.Now;
            orders.Add(order);
            jsonString = JsonConvert.SerializeObject(orders);
            File.WriteAllText(DATA_PATH, jsonString);

            return order;
        }

        public IList<Order>? GetAll()
        {
            var jsonString = File.ReadAllText(DATA_PATH);
            var orders = JsonConvert.DeserializeObject<IList<Order>>(jsonString);
            return orders;
        }

        public int GetNewId()
        {
            var jsonString = File.ReadAllText(DATA_PATH);
            var orders = JsonConvert.DeserializeObject<IList<Order>>(jsonString);
            if (orders == null || orders.Count == 0)
            {
                return 1;
            }
            int maxId = orders.Max(x => x.OrderId);
            int newId = maxId + 1;
            return newId;
        }
    }
}
