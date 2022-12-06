using Newtonsoft.Json;
using OrderApi.Models;

namespace OrderApi.Repositories
{
    public class UserRepositories
    {
        private const string DATA_PATH = @"Data\users.json";

        public IEnumerable<Event>? GetAll()
        {
            var jsonString = File.ReadAllText(DATA_PATH);
            var events = JsonConvert.DeserializeObject<IEnumerable<Event>>(jsonString);
            return events;
        }

        public bool Exist(int userId)
        {
            var jsonString = File.ReadAllText(DATA_PATH);
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(jsonString);
            if (users == null)
            {
                return false;
            }
            var isExist = users.Where(x => x.UserId == userId).Any();
            return isExist;
        }
    }
}
