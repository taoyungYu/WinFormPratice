using Newtonsoft.Json;
using OrderApi.Models;

namespace OrderApi.Repositories
{
    public class EventRepositories
    {
        private const string DATA_PATH = @"Data\events.json";

        public IEnumerable<Event>? GetAll()
        {
            var jsonString = File.ReadAllText(DATA_PATH);
            var events = JsonConvert.DeserializeObject<IEnumerable<Event>>(jsonString);
            return events;
        }

        public Event? Get(int eventId)
        {
            var jsonString = File.ReadAllText(DATA_PATH);
            var events = JsonConvert.DeserializeObject<IEnumerable<Event>>(jsonString);
            if (events == null)
            {
                return null;
            }
            var @event = events.Where(x => x.EventId == eventId).SingleOrDefault();
            return @event;
        }
    }
}
